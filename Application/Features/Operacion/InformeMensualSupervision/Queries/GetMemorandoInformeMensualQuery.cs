using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.InformeMensualSupervision.Queries
{
    public class GetMemorandoInformeMensualQuery : IRequest<Response<List<string>>>
    {
    }

    public class GetMemorandoInformeMensualHandler : IRequestHandler<GetMemorandoInformeMensualQuery, Response<List<string>>>
    {
        private readonly IInformeMensualSupervisionRepository _informe;
        public GetMemorandoInformeMensualHandler(IInformeMensualSupervisionRepository informe)
        {
            _informe = informe;
        }

        public async Task<Response<List<string>>> Handle(GetMemorandoInformeMensualQuery request, CancellationToken cancellationToken)
        { return new Response<List<string>>(_informe.GetMemorandoInformeMensual().Result.ToList()); }
    }
}
