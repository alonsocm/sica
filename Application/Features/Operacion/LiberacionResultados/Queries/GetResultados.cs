using Application.DTOs.LiberacionResultados;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.LiberacionResultados.Queries
{
    public class GetResultados : IRequest<PagedResponse<List<ResultadoLiberacionDTO>>>
    {
        public List<Filter> Filters { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }

    public class GetResultadosHandler : IRequestHandler<GetResultados, PagedResponse<List<ResultadoLiberacionDTO>>>
    {
        private readonly IResultado _resultadoRepository;

        public GetResultadosHandler(IResultado resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<PagedResponse<List<ResultadoLiberacionDTO>>> Handle(GetResultados request, CancellationToken cancellationToken)
        {
            var data = await _resultadoRepository.GetResultadosLiberacion(request.Filters, request.Page, request.PageSize);

            return data;
        }
    }
}
