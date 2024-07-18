using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.GrupoParametro.Queries
{
    public class GetSubGrupoAnalitico : IRequest<Response<IEnumerable<SubGrupoAnaliticoDTO>>>
    {
    }

    public class GetSubGrupoAnaliticoHandler : IRequestHandler<GetSubGrupoAnalitico, Response<IEnumerable<SubGrupoAnaliticoDTO>>>
    {
        private readonly IParametroRepository _parametroRepository;
        public GetSubGrupoAnaliticoHandler(IParametroRepository parametroRepository)
        {
            _parametroRepository = parametroRepository;
        }

        public Task<Response<IEnumerable<SubGrupoAnaliticoDTO>>> Handle(GetSubGrupoAnalitico request, CancellationToken cancellationToken)
        {
            var subGrupos = _parametroRepository.GetSubGrupoAnalitico();

            return Task.FromResult(new Response<IEnumerable<SubGrupoAnaliticoDTO>>(subGrupos));
        }
    }
}
