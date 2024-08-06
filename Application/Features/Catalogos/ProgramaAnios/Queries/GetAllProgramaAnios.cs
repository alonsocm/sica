using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Catalogos.ProgramaAnios.Queries
{
    public class GetAllProgramaAnios : IRequest<Response<List<ProgramaAnio>>>
    { }
    public class GetAllProgramaAniosHandler : IRequestHandler<GetAllProgramaAnios, Response<List<ProgramaAnio>>>
    {
        private readonly IProgramaAnioRepository _programAnio;

        public GetAllProgramaAniosHandler(IMuestreoRepository repository, IProgramaAnioRepository programaAnio)
        { _programAnio = programaAnio; }

        public async Task<Response<List<ProgramaAnio>>> Handle(GetAllProgramaAnios request, CancellationToken cancellationToken)
        {
            var anios = await _programAnio.ObtenerTodosElementosAsync();
            return new Response<List<ProgramaAnio>>(anios.ToList());
        }
    }
}
