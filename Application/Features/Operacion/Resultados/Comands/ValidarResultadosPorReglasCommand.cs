using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class ValidarResultadosPorReglasCommand : IRequest<Response<bool>>
    {
        public List<int> Anios { get; set; } = new List<int>();
        public List<int> NumeroEntrega { get; set; } = new List<int>();
    }

    public class ValidarResultadosPorReglasCommandHandler : IRequestHandler<ValidarResultadosPorReglasCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadosRepository;
        private readonly IReglasMinimoMaximoRepository _reglasMinimoMaximoRepository;
        private readonly IReglaService _regla;

        public ValidarResultadosPorReglasCommandHandler(IMuestreoRepository muestreoRepository, IResultado resultadosRepository, IReglasMinimoMaximoRepository reglasMinimoMaximoRepository, IReglaService regla)
        {
            _muestreoRepository = muestreoRepository;
            _resultadosRepository=resultadosRepository;
            _reglasMinimoMaximoRepository=reglasMinimoMaximoRepository;
            _regla=regla;
        }

        public async Task<Response<bool>> Handle(ValidarResultadosPorReglasCommand request, CancellationToken cancellationToken)
        {
            /*Las reglas se ejecutan (todas) por muestreo, entonces vamos por los muestreos que cumplan con los filtros proporcionados por el usuario*/
            var muestreos = await _muestreoRepository.ObtenerElementosPorCriterioAsync(x => request.Anios.Contains((int)x.AnioOperacion) &&
                                                                                         request.NumeroEntrega.Contains((int)x.NumeroEntrega));

            /*validamos que exista por lo menos un muestreo*/
            if (muestreos.Any())
            {
                /*Vamos por todas las reglas de minimo y máximo (Recordar que todas las reglas se aplican a nivel muestreo) */
                var reglasMinimoMaximo = await _reglasMinimoMaximoRepository.ObtenerTodosElementosAsync();
                var resultadosNoValidos = new List<string>();

                /*Vamos a recorrer cada muestreo para aplicarle las reglas*/
                foreach (var muestreo in muestreos)
                {
                    /*Traemos todos los resultados correspondientes al muestreo que estamos revisando*/
                    var resultadosMuestreo = await _resultadosRepository.ObtenerElementosPorCriterioAsync(x => x.MuestreoId == muestreo.Id);

                    /*Recorremos la lista de reglas (Todo muestreo debe cumplir con las reglas definidas)*/
                    foreach (var regla in reglasMinimoMaximo)
                    {
                        /*Vamos por el resultadoParametro, asociado a la regla en la que estamos*/
                        var resultadoParametro = resultadosMuestreo.FirstOrDefault(f => f.ParametroId == regla.ParametroId);

                        if (resultadoParametro != null)
                        {
                            /*Primero evaluamos las reglas de las cuales si se conoce el minimo y máximo. Esto se sabe por la bandera 'Aplica' */
                            if (regla.Aplica)
                            {
                                /*Ahora, sabemos que aplica, pero debemos validar que el resultado es un valor númerico para poder hacer la comparación de minimoMaximo*/
                                var esNumerico = Decimal.TryParse(resultadoParametro.Resultado, out decimal valor);

                                if (esNumerico)
                                {
                                    try
                                    {
                                        var incumpleRegla = _regla.InCumpleReglaMinimoMaximo(regla.MinimoMaximo, resultadoParametro.Resultado);
                                    }
                                    catch (Exception ex)
                                    {
                                        resultadosNoValidos.Add($"La regla {regla.MinimoMaximo} no pudo ser aplicada al resultado: {resultadoParametro.Resultado}, {ex.Message}");
                                    }
                                }
                                else
                                {
                                    /*Debemos ir al catálogo de reglas de reporte. Para validar en que casos el valor puede no ser un número*/
                                }

                            }
                            else
                            {
                                /*Aquí iremos a buscar el minimoMaximo por parámetro y por laboratorio*/
                            }
                        }
                    }
                }
            }


            throw new NotImplementedException();
        }
    }
}
