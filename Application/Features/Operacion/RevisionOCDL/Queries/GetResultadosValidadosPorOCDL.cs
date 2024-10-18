using Application.DTOs.RevisionOCDL;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionOCDL.Queries
{
    public class GetResultadosValidadosPorOCDL : IRequest<PagedResponse<List<ResultadosValidadosPorOCDLDTO>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filters { get; set; }
    }
    public class GetResultadosValidadosPorOCDLHandler : IRequestHandler<GetResultadosValidadosPorOCDL, PagedResponse<List<ResultadosValidadosPorOCDLDTO>>>
    {
        private readonly IResultado _repository;
        public GetResultadosValidadosPorOCDLHandler(IResultado repository)
        {
            _repository = repository;
        }
        public async Task<PagedResponse<List<ResultadosValidadosPorOCDLDTO>>> Handle(GetResultadosValidadosPorOCDL request, CancellationToken cancellationToken)
        {
            var datos = await _repository.GetResultadosValidadosPorOCDLAsync(request.Filters, request.Page, request.PageSize);
            return datos;
        }
    }
}