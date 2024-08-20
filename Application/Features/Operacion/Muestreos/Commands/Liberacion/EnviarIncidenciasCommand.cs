using Application.DTOs;
using Application.Enums;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class EnviarIncidenciasCommand : IRequest<Response<bool>>
    {
        public List<long> Muestreos { get; set; } = new List<long>();
        public List<Filter> Filters { get; set; } = new List<Filter>();
    }

    public class EnviarIncidenciasCommandHandler : IRequestHandler<EnviarIncidenciasCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public EnviarIncidenciasCommandHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(EnviarIncidenciasCommand request, CancellationToken cancellationToken)
        {
            if (request.Muestreos.Any())
            {
                var muestreosIds = request.Muestreos.Distinct();
                EnviarIncidencias(muestreosIds);
            }
            else
            {
                var data = await _muestreoRepository.GetResultadosMuestreoEstatusMuestreoAsync((int)EstatusMuestreo.ResumenValidaciónReglas);
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

        private void EnviarIncidencias(IEnumerable<long> muestreosIds)
        {
            var muestreos = _muestreoRepository.ObtenerElementoConInclusiones(c => muestreosIds.Contains(c.Id), p => p.ProgramaMuestreo, i => i.ResultadoMuestreo);

            foreach (var muestreo in muestreos)
            {
                if (muestreo.ResultadoMuestreo.Any(a => a.ValidacionFinal == null || a.ValidacionFinal == true))
                {
                    throw new ValidationException($"Para enviar a incidencias el muestreo {muestreo.ProgramaMuestreo.NombreCorrectoArchivo}, no debe existir un \"OK\" en el campo Validación Final");
                }
                else
                {
                    muestreo.EstatusId = (int)EstatusMuestreo_1.EnviadoIncidencia;
                    _muestreoRepository.Actualizar(muestreo);
                }
            }
        }
    }
}
