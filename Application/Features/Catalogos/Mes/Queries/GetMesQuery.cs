using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.Mes.Queries
{
    public class GetMesQuery : IRequest<Response<List<Domain.Entities.Mes>>>
    {
    }

    public class GetMesHandler : IRequestHandler<GetMesQuery, Response<List<Domain.Entities.Mes>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Mes> _repositoryAsync;

        public GetMesHandler(IRepositoryAsync<Domain.Entities.Mes> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        public async Task<Response<List<Domain.Entities.Mes>>> Handle(GetMesQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<Domain.Entities.Mes>>(await _repositoryAsync.ListAsync());
        }
    }
}
