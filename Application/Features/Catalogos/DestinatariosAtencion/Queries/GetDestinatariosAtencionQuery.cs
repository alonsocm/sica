using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.DestinatariosAtencion.Queries
{
    public class GetDestinatariosAtencionQuery : IRequest<Response<List<Domain.Entities.DestinatariosAtencion>>>
    {
    }
    public class GetDestinatariosAtencionHandler : IRequestHandler<GetDestinatariosAtencionQuery, Response<List<Domain.Entities.DestinatariosAtencion>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.DestinatariosAtencion> _repositoryAsync;

        public GetDestinatariosAtencionHandler(IRepositoryAsync<Domain.Entities.DestinatariosAtencion> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        public async Task<Response<List<Domain.Entities.DestinatariosAtencion>>> Handle(GetDestinatariosAtencionQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<Domain.Entities.DestinatariosAtencion>>(await _repositoryAsync.ListAsync());
        }
    }
}
