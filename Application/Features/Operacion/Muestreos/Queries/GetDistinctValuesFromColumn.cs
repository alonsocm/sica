using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Muestreos.Queries
{
    public class GetDistinctValuesFromColumn : IRequest<Response<IEnumerable<object>>>
    {
        public string Column { get; set; }
        public List<Filter> Filters { get; set; }
    }

    public class GetDistinctValuesFromColumnHandler : IRequestHandler<GetDistinctValuesFromColumn, Response<IEnumerable<object>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetDistinctValuesFromColumnHandler(IMuestreoRepository repositoryAsync)
        {
            _repositoryAsync=repositoryAsync;
        }

        public async Task<Response<IEnumerable<object>>> Handle(GetDistinctValuesFromColumn request, CancellationToken cancellationToken)
        {
            var data = await _repositoryAsync.GetResumenMuestreosAsync(new List<int> { (int)Enums.EstatusMuestreo.CargaResultados, (int)Enums.EstatusMuestreo.EvidenciasCargadas });
            data = data.AsQueryable();

            if (request.Filters.Any())
            {
                var expressions = MuestreoExpression.GetExpressionList(request.Filters);

                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            var response = _repositoryAsync.GetDistinctValuesFromColumn(request.Column, data);

            return new Response<IEnumerable<object>>(response);
        }
    }
}
