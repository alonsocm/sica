using Application.DTOs.InformeMensualSupervisionCampo;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.InformeMensualSupervision.Commands
{
    public class CreateInformeMensualSupervision : IRequest<Response<bool>>
    {
        public InformeMensualDto Informe { get; set; }
    }

    public class InformeMensualSupervisionCommandHandler : IRequestHandler<CreateInformeMensualSupervision, Response<bool>>
    {
        private readonly IInformeMensualSupervisionRepository _informeMensualSupervisionRepository;
        private readonly IArchivoService _archivoService;
        public InformeMensualSupervisionCommandHandler(IInformeMensualSupervisionRepository informeMensualSupervisionRepository, IArchivoService archivoService)
        {
            _informeMensualSupervisionRepository = informeMensualSupervisionRepository;
            _archivoService = archivoService;
        }

        public async Task<Response<bool>> Handle(CreateInformeMensualSupervision request, CancellationToken cancellationToken)
        {
            var informe = new Domain.Entities.InformeMensualSupervision
            {
                Memorando = request.Informe.Oficio,
                Lugar = request.Informe.Lugar,
                Fecha = Convert.ToDateTime(request.Informe.Fecha),
                DirectorioFirmaId = request.Informe.ResponsableId,
                Iniciales = request.Informe.PersonasInvolucradas,
                Anio = request.Informe.Anio,
                MesId = request.Informe.Mes,
                FechaRegistro = DateTime.Now,
                UsuarioRegistroId = request.Informe.Usuario,
                ArchivoInformeMensualSupervision = new List<ArchivoInformeMensualSupervision>
                {
                    new ArchivoInformeMensualSupervision
                    {
                        NombreArchivo = request.Informe.Archivo.FileName,
                        Archivo = await _archivoService.ConvertIFormFileToByteArray(request.Informe.Archivo),
                        UsuarioCargaId = request.Informe.Usuario,
                        TipoArchivoInformeMensualSupervisionId = 1,
                        FechaCarga = DateTime.Now
                    }
                },

                CopiaInformeMensualSupervision = request.Informe.Copias.Select(x => new CopiaInformeMensualSupervision
                {
                    Nombre = x.Nombre,
                    Puesto = x.Puesto,
                }).ToList(),
            };

            _informeMensualSupervisionRepository.Insertar(informe);
            //_archivoService.GuardarInformeSupervision(informe.Id.ToString(), request.Informe.Archivo);

            return new Response<bool>(true);
        }
    }
}
