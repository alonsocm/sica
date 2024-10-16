using Application.DTOs.Common;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Operacion.LiberacionResultados.Queries
{
    public class GetDistinctResultadosLiberacionProperty : QueryHelperDTO, IRequest<IEnumerable<object>>
    {
    }

    public class GetDistinctResultadosLiberacionPropertyHandler : IRequestHandler<GetDistinctResultadosLiberacionProperty, IEnumerable<object>>
    {
        private readonly IResultado _resultadoRepository;

        public GetDistinctResultadosLiberacionPropertyHandler(IResultado resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<IEnumerable<object>> Handle(GetDistinctResultadosLiberacionProperty request, CancellationToken cancellationToken)
        {
            return await _resultadoRepository.GetDistinctResultadosLiberacionPropertyAsync(request.Filters, request.Selector);
        }
    }
}
