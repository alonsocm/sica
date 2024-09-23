using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using EstatusResultado = Application.Enums.EstatusResultado;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class EnviarIncidenciasCommand : IRequest<Response<bool>>
    {
        public IEnumerable<long> ResultadosId { get; set; } = new List<long>();
    }

    public class EnviarIncidenciasCommandHandler : IRequestHandler<EnviarIncidenciasCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadosRepository;

        public EnviarIncidenciasCommandHandler(IMuestreoRepository muestreoRepository, IResultado resultadosRepository)
        {
            _muestreoRepository = muestreoRepository;
            _resultadosRepository = resultadosRepository;
        }

        public async Task<Response<bool>> Handle(EnviarIncidenciasCommand request, CancellationToken cancellationToken)
        {
            if (request.ResultadosId.Any())
            {
                var resultados = await _resultadosRepository.ObtenerElementosPorCriterioAsync(x => request.ResultadosId.Contains(x.Id));

                foreach (var resultado in resultados)
                {
                    resultado.EstatusResultadoId = (int)EstatusResultado.IncidenciasResultados;
                    _resultadosRepository.Actualizar(resultado);
                }
            }

            return new Response<bool>(true);
        }
    }
}
