using Application.DTOs.Common;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionOCDL.Queries
{
    public class GetDistinctResultadosValidados : QueryHelperDTO, IRequest<Response<IEnumerable<object>>>
    {
    }
    public class GetDistinctResultadosValidadosHandler : IRequestHandler<GetDistinctResultadosValidados, Response<IEnumerable<object>>>
    {
        private readonly IResultado _resultadoRepository;

        public GetDistinctResultadosValidadosHandler(IResultado resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<Response<IEnumerable<object>>> Handle(GetDistinctResultadosValidados request, CancellationToken cancellationToken)
        {
            var data = await _resultadoRepository.GetDistinctResultadosValidadosAsync(request.Filters, request.Selector);
            return new Response<IEnumerable<object>>(data);
        }
    }
}
