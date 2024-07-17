using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.GrupoParametro.Queries
{
    public class GetGrupoParametro : IRequest<Response<IEnumerable<GrupoParametroDTO>>>
    {
    }

    public class GetGrupoParametroHandler : IRequestHandler<GetGrupoParametro, Response<IEnumerable<GrupoParametroDTO>>>
    {
        private readonly IParametroRepository _parametroRepository;
        public GetGrupoParametroHandler(IParametroRepository parametroRepository)
        {
            _parametroRepository = parametroRepository;
        }

        public Task<Response<IEnumerable<GrupoParametroDTO>>> Handle(GetGrupoParametro request, CancellationToken cancellationToken)
        {
            var grupos = _parametroRepository.GetGruposParametros();

            return Task.FromResult(new Response<IEnumerable<GrupoParametroDTO>>(grupos));
        }
    }
}
