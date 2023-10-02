using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.InformeMensualSupervision.Queries
{
    public class GetLugaresInformeMensualQuery : IRequest<Response<List<string>>>
    {
    }

    public class GetLugaresInformeMensualHandler : IRequestHandler<GetLugaresInformeMensualQuery, Response<List<string>>>
    {
        private readonly IInformeMensualSupervisionRepository _informe;
        public GetLugaresInformeMensualHandler(IInformeMensualSupervisionRepository informe)
        {
            _informe = informe;
        }

        public async Task<Response<List<string>>> Handle(GetLugaresInformeMensualQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<string>>(_informe.GetLugaresInformeMensual().Result.ToList());
        }
    }
}
