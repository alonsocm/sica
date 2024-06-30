using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Resultados.Queries
{
    public class GetResultadosporMuestreoPaginadosQuery : IRequest<PagedResponse<IEnumerable<AcumuladosResultadoDto>>>
    {
        public int estatusId { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }

    public class GetResultadosporMuestreoQueryHandler : IRequestHandler<GetResultadosporMuestreoPaginadosQuery, PagedResponse<IEnumerable<AcumuladosResultadoDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetResultadosporMuestreoQueryHandler(IMuestreoRepository muestreoRepository)
        {
            _repositoryAsync = muestreoRepository;

        }

        public async Task<PagedResponse<IEnumerable<AcumuladosResultadoDto>>> Handle(GetResultadosporMuestreoPaginadosQuery request, CancellationToken cancellationToken)
        {
            var data = await _repositoryAsync.GetResultadosporMuestreoAsync(request.estatusId);
            data = data.AsQueryable();

            if (request.Filter.Any())
            {
                var expressions = MuestreoExpression.GetExpressionList(request.Filter);
                List<AcumuladosResultadoDto> lstMuestreo = new List<AcumuladosResultadoDto>();

                foreach (var filter in expressions)
                {
                    if (request.Filter.Count == 2 && request.Filter[0].Conditional == "equals" && request.Filter[1].Conditional == "equals")
                    {
                        var dataFinal = data;
                        dataFinal = (IEnumerable<AcumuladosResultadoDto>)dataFinal.AsQueryable().Where(filter);
                        lstMuestreo.AddRange(dataFinal);

                    }
                    else
                    {
                        data = (IEnumerable<AcumuladosResultadoDto>)data.AsQueryable().Where(filter);
                    }
                }
                data = (lstMuestreo.Count > 0) ? lstMuestreo.AsQueryable() : data;
            }

            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    data = (IEnumerable<AcumuladosResultadoDto>)data.AsQueryable().OrderBy(MuestreoExpression.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    data = (IEnumerable<AcumuladosResultadoDto>)data.AsQueryable().OrderByDescending(MuestreoExpression.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            if (data == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a GetResultadosporMuestreoAsync");
            }

            return PagedResponse<AcumuladosResultadoDto>.CreatePagedReponse(data, request.page, request.pageSize);
        }
    }
}
