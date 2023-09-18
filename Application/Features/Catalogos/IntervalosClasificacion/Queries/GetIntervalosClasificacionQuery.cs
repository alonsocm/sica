using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.IntervalosClasificacion.Queries
{
    public class GetIntervalosClasificacionQuery : IRequest<Response<List<Domain.Entities.IntervalosClasificacion>>>
    {
    }

    public class GetIntervalosClasificacionHandler : IRequestHandler<GetIntervalosClasificacionQuery, Response<List<Domain.Entities.IntervalosClasificacion>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.IntervalosClasificacion> _repositoryAsync;

        public GetIntervalosClasificacionHandler(IRepositoryAsync<Domain.Entities.IntervalosClasificacion> repositoryAsync)
        {
            this._repositoryAsync = repositoryAsync;

        }
        public async Task<Response<List<Domain.Entities.IntervalosClasificacion>>> Handle(GetIntervalosClasificacionQuery request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.ListAsync();
            return new Response<List<Domain.Entities.IntervalosClasificacion>>(datos);

        }
    }


}
