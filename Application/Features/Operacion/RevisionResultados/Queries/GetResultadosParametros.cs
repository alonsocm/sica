using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
namespace Application.Features.Operacion.RevisionResultados.Queries
{
    public class GetResultadosParametrosPaginados : Pagination, IRequest<PagedResponse<IEnumerable<RegistroOriginalDto>>>
    {
        public int UserId { get; set; }
        public int CuerpoAgua { get; set; }
        public List<Filter> Filter { get; set; }
    }
    public class GetResultadosParametrosQueryHandler : IRequestHandler<GetResultadosParametrosPaginados, PagedResponse<IEnumerable<RegistroOriginalDto>>>
    {
        private readonly IResumenResRepository _repositoryAsync;

        public GetResultadosParametrosQueryHandler(IResumenResRepository repository)
        {
            _repositoryAsync = repository;
        }

        public async Task<PagedResponse<IEnumerable<RegistroOriginalDto>>> Handle(GetResultadosParametrosPaginados request, CancellationToken cancellationToken)
        {
            IEnumerable<RegistroOriginalDto> data = await _repositoryAsync.GetResumenResultadosTemp(request.UserId, null)??throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");

            if (request.Filter.Any())
            {
                var expressions = RegistroOriginalExpression.GetExpressionList(request.Filter);

                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            PagedResponse<IEnumerable<RegistroOriginalDto>> response = PagedResponse<RegistroOriginalDto>.CreatePagedReponse(data, request.Page, request.PageSize);
            return response;
        }
    }
}
