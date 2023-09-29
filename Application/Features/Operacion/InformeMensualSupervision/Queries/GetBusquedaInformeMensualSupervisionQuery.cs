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

        public async Task<Response<List<InformeMensualSupervisionBusquedaDto>>> Handle(GetBusquedaInformeMensualSupervisionQuery request, CancellationToken cancellationToken)
        {
            List<InformeMensualSupervisionBusquedaDto> datos = _informe.GetBusquedaInformeMensual(request.Busqueda).Result.ToList();

            return new Response<List<InformeMensualSupervisionBusquedaDto>>((datos == null) ? new List<InformeMensualSupervisionBusquedaDto>() : datos);
        }
    }
}
