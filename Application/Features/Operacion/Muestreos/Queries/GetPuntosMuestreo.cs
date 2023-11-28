using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Queries
{
    public class GetPuntosMuestreo : IRequest<Response<IEnumerable<PuntosMuestreoDto>>>
    {
        public string claveMuestreo { get; set; }
    }

    public class GetPuntosMuestreoHandler : IRequestHandler<GetPuntosMuestreo, Response<IEnumerable<PuntosMuestreoDto>>>
    {
        private readonly IMuestreoRepository _puntosMuestreoRepository;

        public GetPuntosMuestreoHandler(IMuestreoRepository puntosMuestreoRepository)
        {
            _puntosMuestreoRepository = puntosMuestreoRepository;
        }

        public async Task<Response<IEnumerable<PuntosMuestreoDto>>> Handle(GetPuntosMuestreo request, CancellationToken cancellationToken)
        {
            var evidencias = await _puntosMuestreoRepository.GetPuntoPR_PMAsync(request.claveMuestreo);

            return new Response<IEnumerable<PuntosMuestreoDto>>(evidencias);
        }
    }
}
