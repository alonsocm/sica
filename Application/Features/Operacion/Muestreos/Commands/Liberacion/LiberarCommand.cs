using Application.DTOs;
using Application.Enums;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class LiberarCommand : IRequest<Response<bool>>
    {
        public List<long> Muestreos { get; set; } = new List<long>();
        public List<Filter> Filters { get; set; } = new List<Filter>();
    }

    public class LiberarCommandHandler : IRequestHandler<LiberarCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public LiberarCommandHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(LiberarCommand request, CancellationToken cancellationToken)
        {
            if (request.Muestreos.Any())
            {
                var muestreosIds = request.Muestreos.Distinct();
                LiberarMuestreos(muestreosIds);
            }
            else
            {
                var data = await _muestreoRepository.GetResultadosMuestreoEstatusMuestreoAsync((int)EstatusMuestreo.ValidadoPorReglas);
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
                LiberarMuestreos(muestreosIds);
            }

            return new Response<bool>(true);
        }

        private void LiberarMuestreos(IEnumerable<long> muestreosIds)
        {
            var muestreos = _muestreoRepository.ObtenerElementoConInclusiones(c => muestreosIds.Contains(c.Id), p => p.ProgramaMuestreo, i => i.ResultadoMuestreo);

            foreach (var muestreo in muestreos)
            {
                if (muestreo.ResultadoMuestreo.Any(a => a.ValidacionFinal == null || a.ValidacionFinal == false))
                {
                    throw new ValidationException($"Para liberar el muestreo {muestreo.ProgramaMuestreo.NombreCorrectoArchivo}, el campo validación final, debe tener OK, para todos los resultados");
                }
                else
                {
                    muestreo.EstatusId = (int)EstatusMuestreo.NoEnviado;
                    _muestreoRepository.Actualizar(muestreo);
                }
            }
        }
    }
}
