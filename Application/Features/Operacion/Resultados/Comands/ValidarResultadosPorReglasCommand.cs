using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class ValidarResultadosPorReglasCommand : IRequest<Response<List<ResultadoValidacionReglasDto>>>
    {
        public List<int> Anios { get; set; } = new List<int>();
        public List<int> NumeroEntrega { get; set; } = new List<int>();
    }

    public class ValidarResultadosPorReglasCommandHandler : IRequestHandler<ValidarResultadosPorReglasCommand, Response<List<ResultadoValidacionReglasDto>>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadosRepository;
        private readonly IReglasMinimoMaximoRepository _reglasMinimoMaximoRepository;
        private readonly IReglaService _regla;
        private readonly IReglasReporteRepository _reglasReporteRepository;
        private readonly IFormaReporteEspecificaRepository _formaReporteEspecificaRepository;

        public ValidarResultadosPorReglasCommandHandler(
            IMuestreoRepository muestreoRepository, 
            IResultado resultadosRepository, 
            IReglasMinimoMaximoRepository reglasMinimoMaximoRepository, 
            IReglaService regla, 
            IReglasReporteRepository reglasReporteRepository, 
            IFormaReporteEspecificaRepository formaReporteEspecificaRepository)
        {
            _muestreoRepository = muestreoRepository;
            _resultadosRepository=resultadosRepository;
            _reglasMinimoMaximoRepository=reglasMinimoMaximoRepository;
            _regla=regla;
            _reglasReporteRepository=reglasReporteRepository;
            _formaReporteEspecificaRepository=formaReporteEspecificaRepository;
        }

        public async Task<Response<List<ResultadoValidacionReglasDto>>> Handle(ValidarResultadosPorReglasCommand request, CancellationToken cancellationToken)
        {
            /*Creamos la lista que regresaremos al cliente*/
            List<ResultadoValidacionReglasDto> resultadosValidacion = new List<ResultadoValidacionReglasDto>();
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
                                    if (resultadoParametro.Resultado == "0")
                                    {
                                        var reglaReporte = _reglasReporteRepository.ObtenerElementosPorCriterioAsync(x => x.ParametroId == resultadoParametro.ParametroId).Result.FirstOrDefault();

                                        if (reglaReporte != null && !reglaReporte.EsValidoResultadoCero)
                                        {
                                            resultadoParametro.ReglaReporteId = reglaReporte.Id;
                                            _resultadosRepository.Actualizar(resultadoParametro);
                                            resultadosNoValidos.Add($"El valor: {resultadoParametro.Resultado} no corresponde con ninguna forma de reporte valida para el parámetro con id: {resultadoParametro.ParametroId}");
                                        }
                                    }
                                    else
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
                                }
                                else
                                {
                                    /*Validamos la forma de reporte para cuando un valor no es númerico*/
                                    var formasReporte = new List<string>() { "<0", "NA", "NE", "IM", "<LD", "<CMC", "ND", "<LPC" };

                                    /*Buscamos si el valor del resultado corresponde con alguno de la lista de opciones*/
                                    var formaReporte = formasReporte.FirstOrDefault(x => x == resultadoParametro.Resultado);

                                    /*Si el valor corresponde con alguna forma de reporte, ahora necesitamos saber si es valida. Ocupamos la tabla, ReglasReporte*/
                                    if (formaReporte != null)
                                    {
                                        var reglaReporte = _reglasReporteRepository.ObtenerElementosPorCriterioAsync(x => x.ParametroId == resultadoParametro.ParametroId).Result.FirstOrDefault();

                                        if(reglaReporte != null)
                                        {
                                            bool esValido;

                                            switch (formaReporte)
                                            {
                                                case "<0":
                                                    esValido = reglaReporte.EsValidoMenorCero;
                                                    break;
                                                case "NA":
                                                    esValido = reglaReporte.EsValidoResultadoNa;
                                                    break;
                                                case "NE":
                                                    esValido = reglaReporte.EsValidoResultadoNe;
                                                    break;
                                                case "IM":
                                                    esValido = reglaReporte.EsValidoResultadoIm;
                                                    break;
                                                case "<LD":
                                                    esValido = reglaReporte.EsValidoResultadoMenorLd;
                                                    break;
                                                case "<CMC":
                                                    esValido = reglaReporte.EsValidoResultadoMenorCmc;
                                                    break;
                                                case "ND":
                                                    esValido = reglaReporte.EsValidoResultadoNd;
                                                    break;
                                                case "<LPC":
                                                    esValido = reglaReporte.EsValidoResultadoMenorLpc;
                                                    break;
                                                default:
                                                    resultadosNoValidos.Add($"El resultado: {resultadoParametro.Resultado} del parámetro {resultadoParametro.Parametro.ClaveParametro} no es válido");
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        /*Validamos si el parámetro cuenta con una forma especifica de reporte. Buscaremos por medio del IdParametro del resultado*/
                                        var aplicaFormaReporteEspecifica = await _formaReporteEspecificaRepository.ExisteElemento(x => x.ParametroId == resultadoParametro.ParametroId);

                                        if (!aplicaFormaReporteEspecifica)
                                        {
                                            /*En este punto el valor reportado por el laboratorio no coincide, es momento de reportarlo como error*/
                                            resultadoParametro.ReglaMinMaxId = regla.Id;
                                            _resultadosRepository.Actualizar(resultadoParametro);
                                            resultadosNoValidos.Add($"El valor: {resultadoParametro.Resultado} no corresponde con ninguna forma de reporte valida para el parámetro con id: {resultadoParametro.ParametroId}");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                /*Aquí iremos a buscar el minimoMaximo por parámetro y por laboratorio. Lo haremos por el idParametro y idLaboratorio*/
                            }
                        }
                    }
                }

                resultadosValidacion = await _resultadosRepository.ObtenerResultadosValidacion(muestreos.Select(x => x.Id).ToList());
            }

            return new Response<List<ResultadoValidacionReglasDto>>(resultadosValidacion.Take(100).ToList());
        }
    }
}
