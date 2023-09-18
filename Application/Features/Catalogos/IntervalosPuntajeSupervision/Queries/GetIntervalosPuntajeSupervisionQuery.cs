using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.IntervalosPuntajeSupervision.Queries
{
    public class GetIntervalosPuntajeSupervisionQuery : IRequest<Response<List<Domain.Entities.IntervalosPuntajeSupervision>>>
    {
    }

    public class GetIntervalosClasificacionHandler : IRequestHandler<GetIntervalosPuntajeSupervisionQuery, Response<List<Domain.Entities.IntervalosPuntajeSupervision>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.IntervalosPuntajeSupervision> _repositoryAsync;

        public GetIntervalosClasificacionHandler(IRepositoryAsync<Domain.Entities.IntervalosPuntajeSupervision> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        public async Task<Response<List<Domain.Entities.IntervalosPuntajeSupervision>>> Handle(GetIntervalosPuntajeSupervisionQuery request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.ListAsync();
            return new Response<List<Domain.Entities.IntervalosPuntajeSupervision>>(datos);
        }
    }
}
