using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.SupervisionMuestreo.Commands
{
    
    public class EvidenciaSupervisonCommand : IRequest<Response<RespuestaSupervisionDto>>
    {
     
        public ArchivosSupervisionDto lstEvidencias { get; set; }
    }

    public class EvidenciaSupervisonCommandHandler : IRequestHandler<EvidenciaSupervisonCommand, Response<RespuestaSupervisionDto>>
    {   
        private readonly ISupervisionMuestreoRepository _repository;
       
        private readonly IMapper _mapper;
        private readonly IEvidenciaSupervisionMuestreoRepository _evidenciasupervisionrepository;
        public EvidenciaSupervisonCommandHandler(ISupervisionMuestreoRepository repository,IMapper mapper, IEvidenciaSupervisionMuestreoRepository evidenciasupervisionrepository)
        {
            _repository = repository;           
            _mapper = mapper;
            _evidenciasupervisionrepository = evidenciasupervisionrepository;
        }

        public async Task<Response<RespuestaSupervisionDto>> Handle(EvidenciaSupervisonCommand request, CancellationToken cancellationToken)
        {
            var datosSupervision = await _repository.ObtenerElementoPorIdAsync(request.lstEvidencias.SupervisionId);
            

            if (request.lstEvidencias.Archivos.Count > 0)
            {

                //List<EvidenciaSupervisionMuestreo> lstevidenciasFinal = _mapper.Map<List<EvidenciaSupervisionMuestreo>>(request.lstEvidencias);
                List<EvidenciaSupervisionMuestreo> lstevidenciasFinal = new List<EvidenciaSupervisionMuestreo>();
                


                request.lstEvidencias.Archivos.ToList().ForEach(evidencia =>
                {
                    var evidenciaDto = new EvidenciaSupervisionMuestreo()
                    {
                        SupervisionMuestreoId = request.lstEvidencias.SupervisionId,
                        NombreArchivo = evidencia.FileName,                        
                        TipoEvidenciaId = (evidencia.ContentType =="application/pdf") ? 10 : 11,
                    };
                    lstevidenciasFinal.Add(evidenciaDto);
                });




                List<EvidenciaSupervisionMuestreo> lstevidenciasActualizar = lstevidenciasFinal.Where(x => x.Id != 0).ToList();
                List<EvidenciaSupervisionMuestreo> lstevidenciasNuevas = lstevidenciasFinal.Where(x => x.Id == 0).ToList();
                if (lstevidenciasNuevas.Count > 0) { _evidenciasupervisionrepository.InsertarRango(lstevidenciasFinal); }
                if (lstevidenciasActualizar.Count > 0) { await _evidenciasupervisionrepository.ActualizarBulkAsync(lstevidenciasActualizar); }
            }

            var respuesta = new RespuestaSupervisionDto()
            {
                SupervisionMuestreoId = request.lstEvidencias.SupervisionId,
                Completo = (datosSupervision.Id != 0 && datosSupervision.EvidenciaSupervisionMuestreo.Count > 0 && (datosSupervision.ValoresSupervisionMuestreo.Count > 0 && datosSupervision.ValoresSupervisionMuestreo.Count==70)) ? true : false
            };

            return new Response<RespuestaSupervisionDto>(respuesta);
        }
    }




}
