using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.Resultados.Queries
{
    public class GetVwResultadosNoCumplenFechaEntregaQuery : IRequest<Response<IEnumerable<VwResultadosNoCumplenFechaEntrega>>>
    {
        public IEnumerable<int> Muestreos { get; set; } = new List<int>();
    }

    public class GetVwResultadosNoCumplenFechaEntregaQueryHandler : IRequestHandler<GetVwResultadosNoCumplenFechaEntregaQuery, Response<IEnumerable<VwResultadosNoCumplenFechaEntrega>>>
    {
        private readonly IVwResultadosNoCumplenFechaEntregaRepository _resultadosEvidenciaRepository;
        public GetVwResultadosNoCumplenFechaEntregaQueryHandler(IVwResultadosNoCumplenFechaEntregaRepository resultadosEvidenciaRepository)
        {
            _resultadosEvidenciaRepository = resultadosEvidenciaRepository;
        }

        public async Task<Response<IEnumerable<VwResultadosNoCumplenFechaEntrega>>> Handle(GetVwResultadosNoCumplenFechaEntregaQuery request, CancellationToken cancellationToken)
        {
            var registros = await _resultadosEvidenciaRepository.ObtenerElementosPorCriterioAsync(x => request.Muestreos.Contains((int)x.MuestreoId));
            return new Response<IEnumerable<VwResultadosNoCumplenFechaEntrega>>(registros);
        }
    }
}
