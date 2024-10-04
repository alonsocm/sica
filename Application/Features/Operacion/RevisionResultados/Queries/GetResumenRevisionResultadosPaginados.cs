using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionResultados.Queries
{
    public class GetResumenRevisionResultadosPaginados : IRequest<PagedResponse<IEnumerable<ResultadoMuestreoDto>>>
    {
        public int EstatusId { get; set; }
        public int UserId { get; set; }
        public bool IsOCDL { get; set; } = false;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter>? Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }

    public class GetResumenRevisionResultadosPaginadosHandler : IRequestHandler<GetResumenRevisionResultadosPaginados, PagedResponse<IEnumerable<ResultadoMuestreoDto>>>
    {
        private readonly IResumenResRepository _repositoryAsync;

        public GetResumenRevisionResultadosPaginadosHandler(IResumenResRepository repository)
        {
            _repositoryAsync = repository;
        }

        public async Task<PagedResponse<IEnumerable<ResultadoMuestreoDto>>> Handle(GetResumenRevisionResultadosPaginados request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetResumenResultadosMuestreoAsync(request.EstatusId, request.UserId, request.IsOCDL);

            if (datos == null || !datos.Any())
            {
                throw new KeyNotFoundException("No se encontraron datos asociados a resultados revisados");
            }

            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    datos = datos.AsQueryable().OrderBy(QueryExpression<ResultadoMuestreoDto>.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    datos = datos.AsQueryable().OrderByDescending(QueryExpression<ResultadoMuestreoDto>.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            return PagedResponse<ResultadoMuestreoDto>.CreatePagedReponse(datos, request.Page, request.PageSize);
        }
    }
}
