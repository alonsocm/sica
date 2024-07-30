using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System.Linq;

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
                var expressions = QueryExpression<AcumuladosResultadoDto>.GetExpressionList(request.Filter);

                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
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
