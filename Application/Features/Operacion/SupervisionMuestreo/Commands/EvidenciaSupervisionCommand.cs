using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.SupervisionMuestreo.Commands
{

    public class EvidenciaSupervisionCommand : IRequest<Response<RespuestaSupervisionDto>>
    {
        public ArchivosSupervisionDto LstEvidencias { get; set; }
    }

    public class EvidenciaSupervisonCommandHandler : IRequestHandler<EvidenciaSupervisionCommand, Response<RespuestaSupervisionDto>>
    {
        private readonly ISupervisionMuestreoRepository _repository;
        private readonly IArchivoService _archivos;
        private readonly IMapper _mapper;
        private readonly IEvidenciaSupervisionMuestreoRepository _evidenciasupervisionrepository;
        public EvidenciaSupervisonCommandHandler(ISupervisionMuestreoRepository repository, IMapper mapper,
            IEvidenciaSupervisionMuestreoRepository evidenciasupervisionrepository, IArchivoService archivos)
        {
            _repository = repository;
            _mapper = mapper;
            _evidenciasupervisionrepository = evidenciasupervisionrepository;
            _archivos = archivos;
        }

        public async Task<Response<RespuestaSupervisionDto>> Handle(EvidenciaSupervisionCommand request, CancellationToken cancellationToken)
        {
            var datosSupervision = await _repository.ObtenerElementoPorIdAsync(request.LstEvidencias.SupervisionId);

            if (request.LstEvidencias.Archivos.Count > 0)
            {
                _archivos.GuardarEvidenciasSupervision(request.LstEvidencias);
                List<EvidenciaSupervisionMuestreo> lstevidenciasFinal = new();

                request.LstEvidencias.Archivos.ToList().ForEach(evidencia =>
                {
                    var evidenciaDto = new EvidenciaSupervisionMuestreo()
                    {
                        SupervisionMuestreoId = request.LstEvidencias.SupervisionId,
                        NombreArchivo = evidencia.FileName,
                        TipoEvidenciaId = (evidencia.ContentType =="application/pdf") ? Convert.ToInt64(Enums.TipoEvidencia.ArchivoSupervisión) : Convert.ToInt64(Enums.TipoEvidencia.EvidenciaSupervisión),
                    };
                    lstevidenciasFinal.Add(evidenciaDto);
                });

                List<EvidenciaSupervisionMuestreo> lstevidenciasActualizar = lstevidenciasFinal.Where(x => x.Id != 0).ToList();
                List<EvidenciaSupervisionMuestreo> lstevidenciasNuevas = lstevidenciasFinal.Where(x => x.Id == 0).ToList();

                if (lstevidenciasNuevas.Count > 0)
                    _evidenciasupervisionrepository.InsertarRango(lstevidenciasFinal);

                if (lstevidenciasActualizar.Count > 0)
                    await _evidenciasupervisionrepository.ActualizarBulkAsync(lstevidenciasActualizar);
            }

            var respuesta = new RespuestaSupervisionDto()
            {
                SupervisionMuestreoId = request.LstEvidencias.SupervisionId,
                Completo = (datosSupervision.Id != 0 && datosSupervision.EvidenciaSupervisionMuestreo.Count > 0 && (datosSupervision.ValoresSupervisionMuestreo.Count > 0 && datosSupervision.ValoresSupervisionMuestreo.Count==70)) ? true : false
            };

            return new Response<RespuestaSupervisionDto>(respuesta);
        }
    }




}
