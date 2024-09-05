using Application.DTOs;
using Application.Enums;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Resultados.Queries
{
    public class GetResultadosByEstatusMuestreo : IRequest<PagedResponse<IEnumerable<AcumuladosResultadoDto>>>
    {
        public EstatusMuestreo Estatus { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }

    public class GetResultadosMuestreoEstatusMuestreoQueryHandler : IRequestHandler<GetResultadosByEstatusMuestreo, PagedResponse<IEnumerable<AcumuladosResultadoDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetResultadosMuestreoEstatusMuestreoQueryHandler(IMuestreoRepository repository)
        {
            _repositoryAsync = repository;
        }

        public async Task<PagedResponse<IEnumerable<AcumuladosResultadoDto>>> Handle(GetResultadosByEstatusMuestreo request, CancellationToken cancellationToken)
        {
            var data = await _repositoryAsync.GetResultadosMuestreoByStatusAsync(request.Estatus);

            if (request.Filter.Any())
            {
                var expressions = QueryExpression<AcumuladosResultadoDto>.GetExpressionList(request.Filter);
                List<AcumuladosResultadoDto> lstMuestreo = new();

                foreach (var filter in expressions)
                {
                    if (request.Filter.Count == 2 && request.Filter[0].Conditional == "equals" && request.Filter[1].Conditional == "equals")
                    {
                        var dataFinal = data;
                        dataFinal = dataFinal.AsQueryable().Where(filter);
                        lstMuestreo.AddRange(dataFinal);
                        data = lstMuestreo;
                    }
                    else
                    {
                        data = data.AsQueryable().Where(filter);
                    }
                }
            }
            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    data = data.AsQueryable().OrderBy(QueryExpression<AcumuladosResultadoDto>.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    data = data.AsQueryable().OrderByDescending(QueryExpression<AcumuladosResultadoDto>.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            if (data == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }

            return PagedResponse<AcumuladosResultadoDto>.CreatePagedReponse(data, request.Page, request.PageSize);
        }
    }
}
