using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Actualizar
{
    public class ActualizarEstatusMuestreos : IRequest<Response<bool>>
    {
        public int EstatusId { get; set; }
        public List<Filter> Filters { get; set; }
    }

    public class ActualizarEstatusMuestreosHandler : IRequestHandler<ActualizarEstatusMuestreos, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public ActualizarEstatusMuestreosHandler(IMuestreoRepository muestreoRepository, IMapper mapper)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(ActualizarEstatusMuestreos request, CancellationToken cancellationToken)
        {
            var estatus = new List<long>
            {
                (int)Enums.EstatusMuestreo.Cargado,
                (int)Enums.EstatusMuestreo.EvidenciasCargadas
            };

            //Obtenemos los registros, con los estatus requeridos.
            var data = await _muestreoRepository.GetResumenMuestreosAsync(estatus);

            if (request.Filters.Any())
            {
                var expressions = MuestreoExpression.GetExpressionList(request.Filters);

                //Aplicamos cada filtro al total de datos
                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            foreach (var muestreoId in data.Select(s => s.MuestreoId))
            {
                var muestreo = await _muestreoRepository.ObtenerElementoPorIdAsync(muestreoId);
                muestreo.EstatusId = request.EstatusId;

                // Si se envia al estatus 29 "Acumulados de resultados" se actualiza tambien la bandera de ValidacionEvidencias a true
                muestreo.ValidacionEvidencias = request.EstatusId == (int)Enums.EstatusMuestreo.AcumulacionResultados;
                _muestreoRepository.Actualizar(muestreo);
            }

            return new Response<bool>(true);
        }
    }
}


