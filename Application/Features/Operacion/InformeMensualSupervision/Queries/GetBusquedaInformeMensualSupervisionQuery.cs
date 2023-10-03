using Application.DTOs.InformeMensualSupervisionCampo;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.InformeMensualSupervision.Queries
{
    public class GetBusquedaInformeMensualSupervisionQuery : IRequest<Response<List<InformeMensualSupervisionBusquedaDto>>>
    {
        public InformeMensualSupervisionBusquedaDto Busqueda { get; set; }
    }

    public class GetBusquedaInformeMensualSupervisionHandler : IRequestHandler<GetBusquedaInformeMensualSupervisionQuery, Response<List<InformeMensualSupervisionBusquedaDto>>>
    {
        private readonly IInformeMensualSupervisionRepository _informe;
        public GetBusquedaInformeMensualSupervisionHandler(IInformeMensualSupervisionRepository informe)
        {
            _informe = informe;
        }

        public Task<Response<List<InformeMensualSupervisionBusquedaDto>>> Handle(GetBusquedaInformeMensualSupervisionQuery request, CancellationToken cancellationToken)
        {
            List<InformeMensualSupervisionBusquedaDto> informes = _informe.GetBusquedaInformeMensual(request.Busqueda);

            return Task.FromResult(new Response<List<InformeMensualSupervisionBusquedaDto>>(informes));
        }
    }
}
