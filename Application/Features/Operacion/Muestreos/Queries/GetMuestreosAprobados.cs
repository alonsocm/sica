using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Queries
{
    public class GetMuestreosAprobados : IRequest<Response<List<MuestreoDto>>>
    {
    }

    public class GetMuestreosAprobadosHandler : IRequestHandler<GetMuestreosAprobados, Response<List<MuestreoDto>>>
    {
        private readonly IMuestreoRepository _repository;

        public GetMuestreosAprobadosHandler(IMuestreoRepository repository)
        {
            _repository=repository;
        }

        public async Task<Response<List<MuestreoDto>>> Handle(GetMuestreosAprobados request, CancellationToken cancellationToken)
        {
            var estatus = new List<long>
            {
                (long)Application.Enums.EstatusMuestreo_1.OriginalesAprobados
            };

            var muestreos = await _repository.GetResumenMuestreosAsync(estatus);

            return new Response<List<MuestreoDto>>(muestreos.ToList());
        }
    }
}
