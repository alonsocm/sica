using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.Laboratorios.Queries
{
    public class GetLaboratoriosMuestradoresQuery : IRequest<Response<List<Domain.Entities.Laboratorios>>>
    {
    }

    public class GetLaboratoriosMuestradoresHandler : IRequestHandler<GetLaboratoriosMuestradoresQuery, Response<List<Domain.Entities.Laboratorios>>>
    {
        private readonly ILaboratorioRepository _repositoryAsync;


        public GetLaboratoriosMuestradoresHandler(ILaboratorioRepository repositoryAsync)
        {
            this._repositoryAsync = repositoryAsync;

        }
        public async Task<Response<List<Domain.Entities.Laboratorios>>> Handle(GetLaboratoriosMuestradoresQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<Domain.Entities.Laboratorios>>(_repositoryAsync.ObtenerLaboratoriosMuestradores());
        }
    }
}
