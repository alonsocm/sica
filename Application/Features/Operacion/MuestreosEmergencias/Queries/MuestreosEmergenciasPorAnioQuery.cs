using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.MuestreosEmergencias.Queries
{
    public class MuestreosEmergenciasPorAnioQuery : IRequest<Response<IEnumerable<ResultadoMuestreoEmergenciaDto>>>
    {
        public List<int> Anios { get; set; }
    }

    public class MuestreosEmergenciasPorAñoQueryHandler : IRequestHandler<MuestreosEmergenciasPorAnioQuery, Response<IEnumerable<ResultadoMuestreoEmergenciaDto>>>
    {
        private readonly IMuestreoEmergenciasRepository _muestreosEmergenciasRepository;
        public MuestreosEmergenciasPorAñoQueryHandler(IMuestreoEmergenciasRepository muestreoEmergenciasRepository)
        {
            _muestreosEmergenciasRepository = muestreoEmergenciasRepository;
        }

        public async Task<Response<IEnumerable<ResultadoMuestreoEmergenciaDto>>> Handle(MuestreosEmergenciasPorAnioQuery request, CancellationToken cancellationToken)
        {
            var resultadosMuestreoEmergencias = await _muestreosEmergenciasRepository.ObtenerResultadosEmergenciasPorAnio();

            return new Response<IEnumerable<ResultadoMuestreoEmergenciaDto>>(resultadosMuestreoEmergencias);
        }
    }
}
