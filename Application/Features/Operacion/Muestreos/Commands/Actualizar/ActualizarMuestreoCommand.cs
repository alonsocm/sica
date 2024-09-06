using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Actualizar
{
    public class ActualizarMuestreoCommand : IRequest<Response<bool>>
    {
        public IEnumerable<long> Muestreos { get; set; }
    }

    public class ActualizarMuestreoHandler : IRequestHandler<ActualizarMuestreoCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public ActualizarMuestreoHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(ActualizarMuestreoCommand request, CancellationToken cancellationToken)
        {
            //Consultamos los muestreos por id y contengan resultados
            var muestreos = await _muestreoRepository.ObtenerElementosPorCriterioAsync(x => request.Muestreos.Contains((int)x.Id) && x.ResultadoMuestreo.Count > 0);

            if (!muestreos.Any())
            {
                throw new ArgumentException("No se pueden actualizar los muestreos seleccionados, ya que no contienen resultados");
            }
            else
            {
                foreach (var muestreo in muestreos)
                {
                    muestreo.EstatusId = (int)Enums.EstatusMuestreo.MóduloReglas;
                    //muestreo.AutorizacionIncompleto = muestreos.AutorizacionIncompleto;
                    //muestreo.AutorizacionFechaEntrega = muestreos.AutorizacionFechaEntrega;                    
                }

                await _muestreoRepository.ActualizarAsync(muestreos);
                return new Response<bool>(true);
            }
        }
    }
}
