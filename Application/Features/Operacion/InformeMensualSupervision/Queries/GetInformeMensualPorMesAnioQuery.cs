using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.ReporteSupervisionMuestreo.Queries
{
    public class GetInformeMensualPorMesAnioQuery : IRequest<Response<InformeMensualSupervisionDto>>
    {
        public string anioReporte { get; set; }
        public string? anioRegistro { get; set; } = null;
        public int? mes { get; set; } = null;
    }

    public class GetInformeMensualPorMesAnioHandler : IRequestHandler<GetInformeMensualPorMesAnioQuery, Response<InformeMensualSupervisionDto>>
    {
        private readonly IInformeMensualSupervisionRepository _repository;
        public GetInformeMensualPorMesAnioHandler(IInformeMensualSupervisionRepository repository)
        {
            _repository = repository;

        }

        public async Task<Response<InformeMensualSupervisionDto>> Handle(GetInformeMensualPorMesAnioQuery request, CancellationToken cancellationToken)
        {

            return new Response<InformeMensualSupervisionDto>(_repository.GetInformeMensualPorAnioMes(request.anioReporte, request.anioRegistro, request.mes).Result);
        }
    }

}
