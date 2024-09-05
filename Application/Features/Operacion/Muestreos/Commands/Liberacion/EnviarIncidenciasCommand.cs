using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using FluentValidation;
using MediatR;
using EstatusResultado = Application.Enums.EstatusResultado;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class EnviarIncidenciasCommand : IRequest<Response<bool>>
    {
        public List<long> ResultadosId { get; set; } = new List<long>();
        public List<Filter> Filters { get; set; } = new List<Filter>();
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
                var resultadosIds = request.ResultadosId.Distinct();
                EnviarIncidencias(resultadosIds);
            }
            else
            {
                var data = await _muestreoRepository.GetResultadosMuestreoByStatusAsync(Enums.EstatusMuestreo.ResumenValidaciónReglas);
                var expressions = QueryExpression<AcumuladosResultadoDto>.GetExpressionList(request.Filters);
                List<AcumuladosResultadoDto> lstMuestreo = new();

                foreach (var filter in expressions)
                {
                    if (request.Filters.Count == 2 && request.Filters[0].Conditional == "equals" && request.Filters[1].Conditional == "equals")
                    {
                        var dataFinal = data;
                        dataFinal = dataFinal.AsQueryable().Where(filter);
                        lstMuestreo.AddRange(dataFinal);
                        data = lstMuestreo;
                    }
                    else
                    {
                        data = data.AsQueryable().Where(filter);
                    }
                }

                var muestreosIds = data.DistinctBy(x => x.MuestreoId).Select(s => s.MuestreoId);
                EnviarIncidencias(muestreosIds);
            }

            return new Response<bool>(true);
        }

        private void EnviarIncidencias(IEnumerable<long> resultadosIds)
        {
            var resultados = _resultadosRepository.ObtenerElementosPorCriterioAsync(x => resultadosIds.Contains(x.Id)).Result;



            foreach (var resultado in resultados)
            {
                resultado.EstatusResultadoId = (int)EstatusResultado.IncidenciasResultados;
                _resultadosRepository.Actualizar(resultado);
            }
        }
    }
}
