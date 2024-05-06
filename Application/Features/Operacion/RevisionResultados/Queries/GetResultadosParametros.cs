using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
namespace Application.Features.Operacion.RevisionResultados.Queries
{
    public class GetResultadosParametros : Pagination, IRequest<PagedResponse<List<RegistroOriginalDto>>>
    {
        public int UserId { get; set; }
        public int CuerpoAgua { get; set; }
        public int Estatus { get; set; }
        public int Anio { get; set; }
        public List<Filter> Filter { get; set; }
    }
    public class GetResultadosParametrosQueryHandler : IRequestHandler<GetResultadosParametros, PagedResponse<List<RegistroOriginalDto>>>
    {
        private readonly IResumenResRepository _repositoryAsync;

        public GetResultadosParametrosQueryHandler(IResumenResRepository repository)
        {
            _repositoryAsync = repository;
        }

        public async Task<PagedResponse<List<RegistroOriginalDto>>> Handle(GetResultadosParametros request, CancellationToken cancellationToken)
        {
            IEnumerable<RegistroOriginalDto> data = await _repositoryAsync.GetResumenResultadosTemp(request.UserId, request.CuerpoAgua, request.Estatus, request.Anio)??throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");

            if (request.Filter.Any())
            {
                var expressions = RegistroOriginalExpression.GetExpressionList(request.Filter);

                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            PagedResponse<List<RegistroOriginalDto>> response = PagedResponse<RegistroOriginalDto>.CreatePagedReponse(data.ToList(), request.Page, request.PageSize);
            return response;
        }
    }
}
