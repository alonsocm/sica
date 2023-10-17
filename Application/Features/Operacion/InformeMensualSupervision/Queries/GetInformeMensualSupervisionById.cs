using Application.DTOs.InformeMensualSupervisionCampo;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace WebAPI.Controllers.v1.Operacion
{
    public class GetInformeMensualSupervisionById : IRequest<Response<InformeMensualDto>>
    {
        public long Informe { get; set; }
    }

    public class GetInformeMensualSupervisionByIdHandler : IRequestHandler<GetInformeMensualSupervisionById, Response<InformeMensualDto>>
    {
        private readonly IInformeMensualSupervisionRepository _repository;
        public GetInformeMensualSupervisionByIdHandler(IInformeMensualSupervisionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<InformeMensualDto>> Handle(GetInformeMensualSupervisionById request, CancellationToken cancellationToken)
        {
            var informe = _repository.ObtenerElementoConInclusiones(x => x.Id == request.Informe, i => i.CopiaInformeMensualSupervision).First();

            var informeDto = new InformeMensualDto()
            {
                Oficio = informe.Memorando,
                Lugar = informe.Lugar,
                Fecha = informe.Fecha.ToString("yyyy-MM-dd"),
                ResponsableId = informe.DirectorioFirmaId,
                Anio = informe.Anio,
                Mes = informe.MesId,
                PersonasInvolucradas = informe.Iniciales,
                Copias = informe.CopiaInformeMensualSupervision.Select(s => new Copia()
                {
                    Nombre = s.Nombre,
                    Puesto = s.Puesto,
                }).ToList(),
            };

            return new Response<InformeMensualDto>(informeDto);
        }
    }
}