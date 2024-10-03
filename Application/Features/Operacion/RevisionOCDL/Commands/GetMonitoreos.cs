using Application.DTOs.RevisionOCDL;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionOCDL.Commands
{
    public class GetMonitoreos : IRequest<PagedResponse<IEnumerable<MonitoreoOCDL>>>
    {
        public int UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filters { get; set; } = new List<Filter>();
    }

    public class GetMonitoreosHandler : IRequestHandler<GetMonitoreos, PagedResponse<IEnumerable<MonitoreoOCDL>>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public GetMonitoreosHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<PagedResponse<IEnumerable<MonitoreoOCDL>>> Handle(GetMonitoreos request, CancellationToken cancellationToken)
        {
            var datos = await _muestreoRepository.GetMonitoreosOCDL(request.UserId);

            if (request.Filters.Any())
            {
                var expressions = QueryExpression<MonitoreoOCDL>.GetExpressionList(request.Filters);

                foreach (var expression in expressions)
                {
                    datos = datos.Where(expression);
                }
            }

            return PagedResponse<MonitoreoOCDL>.CreatePagedReponse(datos, request.Page, request.PageSize);
        }
    }
}
