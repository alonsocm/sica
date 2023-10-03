using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.InformeMensualSupervision.Queries
{
    public class GetInformeMensualPorMesAnioQuery : IRequest<Response<InformeMensualSupervisionDto>>
    {
        public string AnioReporte { get; set; }
        public string? AnioRegistro { get; set; } = null;
        public int? Mes { get; set; } = null;
        public long? OcId { get; set; } = null;
    }

    public class GetInformeMensualPorMesAnioHandler : IRequestHandler<GetInformeMensualPorMesAnioQuery, Response<InformeMensualSupervisionDto>>
    {
        private readonly IInformeMensualSupervisionRepository _repository;
        public GetInformeMensualPorMesAnioHandler(IInformeMensualSupervisionRepository repository)
        {
            _repository = repository;
        }

        public Task<Response<InformeMensualSupervisionDto>> Handle(GetInformeMensualPorMesAnioQuery request, CancellationToken cancellationToken)
        {
            var informe = _repository.GetInformeMensualPorAnioMes(request.AnioReporte, request.AnioRegistro, request.Mes, request.OcId);

            return Task.FromResult(new Response<InformeMensualSupervisionDto>(informe));
        }
    }
}
