using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.UnidadMedida.Queries
{
    public class GetUnidadesMedida : IRequest<Response<IEnumerable<UnidadMedidaDTO>>>
    {
    }

    public class GetUnidadesMedidaHandler : IRequestHandler<GetUnidadesMedida, Response<IEnumerable<UnidadMedidaDTO>>>
    {
        private readonly IParametroRepository _parametroRepository;

        public GetUnidadesMedidaHandler(IParametroRepository parametroRepository)
        {
            _parametroRepository=parametroRepository;
        }

        public Task<Response<IEnumerable<UnidadMedidaDTO>>> Handle(GetUnidadesMedida request, CancellationToken cancellationToken)
        {
            var unidadesMedida = _parametroRepository.GetUnidadesMedida();

            return Task.FromResult(new Response<IEnumerable<UnidadMedidaDTO>>(unidadesMedida));
        }
    }
}
