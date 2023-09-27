using Application.DTOs.InformeMensualSupervisionCampo;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.ReporteSupervisionMuestreo.Commands
{
    public class InformeMensualSupervisionCommand : IRequest<Response<bool>>
    {
        public InformeMensualDto Informe { get; set; }
    }

    public class InformeMensualSupervisionCommandHandler : IRequestHandler<InformeMensualSupervisionCommand, Response<bool>>
    {
        private readonly IInformeMensualSupervisionRepository _informeMensualSupervisionRepository;
        private readonly IArchivoService _archivoService;
        public InformeMensualSupervisionCommandHandler(IInformeMensualSupervisionRepository informeMensualSupervisionRepository, IArchivoService archivoService)
        {
            _informeMensualSupervisionRepository = informeMensualSupervisionRepository;
            _archivoService = archivoService;
        }

        public async Task<Response<bool>> Handle(InformeMensualSupervisionCommand request, CancellationToken cancellationToken)
        {
            var informe = new InformeMensualSupervision
            {
                Memorando = request.Informe.Oficio,
                Lugar = request.Informe.Lugar,
                Fecha = Convert.ToDateTime(request.Informe.Fecha),
                DirectorioFirmaId = request.Informe.ResponsableId,
                Iniciales = request.Informe.PersonasInvolucradas,
                MesId = request.Informe.Mes,
                FechaRegistro = DateTime.Now,
                UsuarioRegistroId = request.Informe.Usuario,
                CopiaInformeMensualSupervision = request.Informe.Copias.Select(x => new CopiaInformeMensualSupervision
                {
                    Nombre = x.Nombre,
                    Puesto = x.Puesto,
                }).ToList(),
            };

            _informeMensualSupervisionRepository.Insertar(informe);
            _archivoService.GuardarInformeSupervision(informe.Id.ToString(), request.Informe.Archivo);

            return new Response<bool>(true);
        }
    }
}
