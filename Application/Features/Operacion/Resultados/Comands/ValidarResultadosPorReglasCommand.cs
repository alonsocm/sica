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
                    //var resultadosMuestreo = await _resultadosRepository.ObtenerElementosPorCriterioAsync(x => x.MuestreoId == muestreo.Id);
                    var resultadosMuestreo = _resultadosRepository.ObtenerElementoConInclusiones(x => x.MuestreoId == muestreo.Id, y => y.Parametro);

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

                    /*Ahora vamos con las reglas de relación. Aquí ejecutamos el método que contiene todas las reglas*/
                    //AplicarReglasDeRelacion(resultadosMuestreo.ToList());
                    
                }

                resultadosValidacion = await _resultadosRepository.ObtenerResultadosValidacion(muestreos.Select(x => x.Id).ToList());
            }

            return new Response<List<ResultadoValidacionReglasDto>>(resultadosValidacion);
        }

        public void AplicarReglasDeRelacion(List<ResultadoMuestreo> resultadosMuestreo)
        {
            /*Se iran ejecutando una a una las reglas de relación*/
            List<string> errores = new();

            /*COLI_FEC > COLI_TOT */
            var valorCOLI_FEC = ObtenerResultadoParametro(resultadosMuestreo, "COLI_FEC");
            var valorCOLI_TOT = ObtenerResultadoParametro(resultadosMuestreo, "COLI_TOT");

            if (Convert.ToDecimal(valorCOLI_FEC) > Convert.ToDecimal(valorCOLI_TOT))
            {
                errores.Add("RR-1");
            }

            /*E_COLI > COLI_FEC*/
            var valorE_COLI = ObtenerResultadoParametro(resultadosMuestreo, "E_COLI");

            if (Convert.ToDecimal(valorE_COLI) > Convert.ToDecimal(valorCOLI_FEC))
            {
                errores.Add("RR-2");
            }

            /*DBO_SOL > DBO_TOT*/
            var valorDBO_SOL = ObtenerResultadoParametro(resultadosMuestreo, "DBO_SOL");
            var valorDBO_TOT = ObtenerResultadoParametro(resultadosMuestreo, "DBO_TOT");

            if (Convert.ToDecimal(valorDBO_SOL) > Convert.ToDecimal(valorDBO_TOT))
            {
                errores.Add("RR-3");
            }
            
            /*COT_SOL > COT*/
            var valorCOT_SOL = ObtenerResultadoParametro(resultadosMuestreo, "COT_SOL");
            var valorCOT = ObtenerResultadoParametro(resultadosMuestreo, "COT");

            if (Convert.ToDecimal(valorCOT_SOL) > Convert.ToDecimal(valorCOT))
            {
                errores.Add("RR-4");
            }

            /*AOXF > AOXT*/
            var valorAOXF = ObtenerResultadoParametro(resultadosMuestreo, "AOXF");
            var valorAOXT = ObtenerResultadoParametro(resultadosMuestreo, "AOXT");

            if (Convert.ToDecimal(valorAOXF) > Convert.ToDecimal(valorAOXT))
            {
                errores.Add("RR-5");
            }

            /*AOXP > AOXT*/
            var valorAOXP = ObtenerResultadoParametro(resultadosMuestreo, "AOXP");

            if (Convert.ToDecimal(valorAOXP) > Convert.ToDecimal(valorAOXT))
            {
                errores.Add("RR-6");
            }

            /*DBO_TOT > DQO_TOT*/
            var valorDQO_TOT = ObtenerResultadoParametro(resultadosMuestreo, "DQO_TOT");

            if (Convert.ToDecimal(valorDBO_TOT) > Convert.ToDecimal(valorDQO_TOT))
            {
                errores.Add("RR-7");
            }

            /*DBO_TOT > DQO_TOT*/
            var valorDQO_SOL = ObtenerResultadoParametro(resultadosMuestreo, "DQO_SOL");

            if (Convert.ToDecimal(valorDQO_SOL) > Convert.ToDecimal(valorDQO_TOT))
            {
                errores.Add("RR-8");
            }

            /*PO4_TOT / P_TOT  < 2.6 ó > 3.6*/
            var valorPO4_TOT = ObtenerResultadoParametro(resultadosMuestreo, "PO4_TOT");
            var valorP_TOT = ObtenerResultadoParametro(resultadosMuestreo, "P_TOT");

            if ((Convert.ToDecimal(valorPO4_TOT) / Convert.ToDecimal(valorP_TOT)) < 2.6M || (Convert.ToDecimal(valorPO4_TOT) / Convert.ToDecimal(valorP_TOT)) < 3.6M)
            {
                errores.Add("RR-11");
            }

            /*N_TOT <> N_NO2 + N_NO3 + (N_NH3 + NORG)*/
            var valorN_TOT = ObtenerResultadoParametro(resultadosMuestreo, "N_TOT");
            var valorN_NO2 = ObtenerResultadoParametro(resultadosMuestreo, "N_NO2");
            var valorN_NO3 = ObtenerResultadoParametro(resultadosMuestreo, "N_NO3");
            var valorN_NH3 = ObtenerResultadoParametro(resultadosMuestreo, "N_NH3");
            var valorNORG = ObtenerResultadoParametro(resultadosMuestreo, "NORG");/*TODO:Validar el párametro NORG, se revisó en la BD y no existe*/

            if (Convert.ToDecimal(valorN_TOT) != (Convert.ToDecimal(valorN_NO2) + Convert.ToDecimal(valorN_NO3) + Convert.ToDecimal(valorN_NH3) + Convert.ToDecimal(valorNORG)))
            {
                errores.Add("RR-13");
            }

            /*P_TOT <> P_ORG + P_INORG*/
            var valorP_ORG = ObtenerResultadoParametro(resultadosMuestreo, "P_ORG");
            var valorP_INORG = ObtenerResultadoParametro(resultadosMuestreo, "P_INORG");

            if (Convert.ToDecimal(valorP_TOT) != (Convert.ToDecimal(valorP_ORG) + Convert.ToDecimal(valorP_INORG)))
            {
                errores.Add("RR-15");
            }

            /*N_NH3 > N_TOTK*/
            var valorN_TOTK = ObtenerResultadoParametro(resultadosMuestreo, "N_TOTK");

            if (Convert.ToDecimal(valorN_NH3) > Convert.ToDecimal(valorN_TOTK))
            {
                errores.Add("RR-16");
            }

            /*NTK <> (N_NH3 + N_ORG)*/
            var valorNTK = ObtenerResultadoParametro(resultadosMuestreo, "NTK");
            var valorN_ORG = ObtenerResultadoParametro(resultadosMuestreo, "N_ORG");

            if (Convert.ToDecimal(valorNTK) != (Convert.ToDecimal(valorN_NH3) + Convert.ToDecimal(valorN_ORG)))
            {
                errores.Add("RR-17");
            }

            /*ORTO_PO4 / 3.06 > P_TOT*/
            var valorORTO_PO4 = ObtenerResultadoParametro(resultadosMuestreo, "ORTO_PO4");

            if ((Convert.ToDecimal(valorORTO_PO4) / 3.06M) > Convert.ToDecimal(valorP_TOT))
            {
                errores.Add("RR-18");
            }

            /*TOX_D_48_UT <> 100/TOX_D_48_EC50*/
            var valorTOX_D_48_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_D_48_UT");
            var valorTOX_D_48_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_D_48_EC50");

            if (Convert.ToDecimal(valorTOX_D_48_UT) != (100M / Convert.ToDecimal(valorTOX_D_48_EC50)))
            {
                errores.Add("RR-22");
            }

            /*TOX_D_48_FON_UT  <> 100/ TOX_D_48_FON_EC50*/
            var valorTOX_D_48_FON_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_D_48_FON_UT");
            var valorTOX_D_48_FON_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_D_48_FON_EC50");

            if (Convert.ToDecimal(valorTOX_D_48_FON_UT) != (100M / Convert.ToDecimal(valorTOX_D_48_FON_EC50)))
            {
                errores.Add("RR-23");
            }

            /*TOX_D_48_SUP_UT  <> 100/ TOX_D_48_SUP_EC50*/
            var valorTOX_D_48_SUP_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_D_48_SUP_UT");
            var valorTOX_D_48_SUP_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_D_48_SUP_EC50");

            if (Convert.ToDecimal(valorTOX_D_48_SUP_UT) != (100M / Convert.ToDecimal(valorTOX_D_48_SUP_EC50)))
            {
                errores.Add("RR-24");
            }

            /*TOX_V_15_UT <> 100/TOX_V15_EC50*/
            var valorTOX_V_15_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_V_15_UT");
            var valorTOX_V15_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_V15_EC50");

            if (Convert.ToDecimal(valorTOX_V_15_UT) != (100M / Convert.ToDecimal(valorTOX_V15_EC50)))
            {
                errores.Add("RR-25");
            }

            /*TOX_V_30_UT  <> 100/TOX_V30_EC50*/
            var valorTOX_V_30_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_V_30_UT");
            var valorTOX_V30_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_V30_EC50");

            if (Convert.ToDecimal(valorTOX_V_30_UT) != (100M / Convert.ToDecimal(valorTOX_V30_EC50)))
            {
                errores.Add("RR-26");
            }

            /*TOX_V_5_UT <> 100/TOX_V5_EC50*/
            var valorTOX_V_5_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_V_5_UT");
            var valorTOX_V5_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_V5_EC50");

            if (Convert.ToDecimal(valorTOX_V_5_UT) != (100M / Convert.ToDecimal(valorTOX_V5_EC50)))
            {
                errores.Add("RR-27");
            }

            /*TOX_FIS_FON_15_UT  <> 100/ TOX_FIS_FON_15_EC50*/
            var valorTOX_FIS_FON_15_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_FON_15_UT");
            var valorTOX_FIS_FON_15_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_FON_15_EC50");

            if (Convert.ToDecimal(valorTOX_FIS_FON_15_UT) != (100M / Convert.ToDecimal(valorTOX_FIS_FON_15_EC50)))
            {
                errores.Add("RR-28");
            }

            /*TOX_FIS_FON_30_UT  <> 100/ TOX_FIS_FON_30_EC50*/
            var valorTOX_FIS_FON_30_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_FON_30_UT");
            var valorTOX_FIS_FON_30_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_FON_30_EC50");

            if (Convert.ToDecimal(valorTOX_FIS_FON_30_UT) != (100M / Convert.ToDecimal(valorTOX_FIS_FON_30_EC50)))
            {
                errores.Add("RR-29");
            }

            /*TOX_FIS_FON_5_UT  <> 100/ TOX_FIS_FON_5_EC50*/
            var valorTOX_FIS_FON_5_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_FON_5_UT");
            var valorTOX_FIS_FON_5_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_FON_5_EC50");

            if (Convert.ToDecimal(valorTOX_FIS_FON_5_UT) != (100M / Convert.ToDecimal(valorTOX_FIS_FON_5_EC50)))
            {
                errores.Add("RR-30");
            }

            /*TOX_FIS_SUP_15_UT <> 100/ TOX_FIS_SUP_15_EC50*/
            var valorTOX_FIS_SUP_15_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_SUP_15_UT");
            var valorTOX_FIS_SUP_15_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_SUP_15_EC50");

            if (Convert.ToDecimal(valorTOX_FIS_SUP_15_UT) != (100M / Convert.ToDecimal(valorTOX_FIS_SUP_15_EC50)))
            {
                errores.Add("RR-31");
            }

            /*TOX_FIS_SUP_30_UT  <> 100/ TOX_FIS_SUP_30_EC50*/
            var valorTOX_FIS_SUP_30_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_SUP_30_UT");
            var valorTOX_FIS_SUP_30_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_SUP_30_EC50");

            if (Convert.ToDecimal(valorTOX_FIS_SUP_30_UT) != (100M / Convert.ToDecimal(valorTOX_FIS_SUP_30_EC50)))
            {
                errores.Add("RR-32");
            }

            /*TOX_FIS_SUP_5_UT <> 100/ TOX_FIS_SUP_5_EC50*/
            var valorTOX_FIS_SUP_5_UT = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_SUP_5_UT");
            var valorTOX_FIS_SUP_5_EC50 = ObtenerResultadoParametro(resultadosMuestreo, "TOX_FIS_SUP_5_EC50");

            if (Convert.ToDecimal(valorTOX_FIS_SUP_5_UT) != (100M / Convert.ToDecimal(valorTOX_FIS_SUP_5_EC50)))
            {
                errores.Add("RR-33");
            }

            /*SI OD_% >50% ENTONCES POT_REDOX_CAMPO < 0*/
            var valorOD = ObtenerResultadoParametro(resultadosMuestreo, "OD_%");
            var valorPOT_REDOX_CAMPO = ObtenerResultadoParametro(resultadosMuestreo, "POT_REDOX_CAMPO");

            if (Convert.ToDecimal(valorOD) > 50)
            {
                if (Convert.ToDecimal(valorPOT_REDOX_CAMPO) < 0)
                {
                    errores.Add("RR-34");
                }
            }

            /*SI OD_%_FON >50% ENTONCES POT_REDOX_CAMPO_FON < 0*/
            var valorOD_FON = ObtenerResultadoParametro(resultadosMuestreo, "OD_%_FON");
            var valorPOT_REDOX_CAMPO_FON = ObtenerResultadoParametro(resultadosMuestreo, "POT_REDOX_CAMPO_FON");

            if (Convert.ToDecimal(valorOD_FON) > 50)
            {
                if (Convert.ToDecimal(valorPOT_REDOX_CAMPO_FON) < 0)
                {
                    errores.Add("RR-35");
                }
            }

            /*SI OD_%_MED >50% ENTONCES POT_REDOX_CAMPO_MED < 0*/
            var valorOD_MED = ObtenerResultadoParametro(resultadosMuestreo, "OD_%_MED");
            var valorPOT_REDOX_CAMPO_MED = ObtenerResultadoParametro(resultadosMuestreo, "POT_REDOX_CAMPO_MED");

            if (Convert.ToDecimal(valorOD_MED) > 50)
            {
                if (Convert.ToDecimal(valorPOT_REDOX_CAMPO_MED) < 0)
                {
                    errores.Add("RR-36");
                }
            }

            /*SI OD_%_SUP >50% POT_REDOX_CAMPO_SUP < 0*/
            var valorOD_SUP = ObtenerResultadoParametro(resultadosMuestreo, "OD_%SUP");
            var valorPOT_REDOX_CAMPO_SUP = ObtenerResultadoParametro(resultadosMuestreo, "POT_REDOX_CAMPO_SUP");

            if (Convert.ToDecimal(valorOD_SUP) > 50)
            {
                if (Convert.ToDecimal(valorPOT_REDOX_CAMPO_SUP) < 0)
                {
                    errores.Add("RR-37");
                }
            }

            /*TRANSPARENCIA > PROFUNDIDAD*/
            var valorTRANSPARENCIA = ObtenerResultadoParametro(resultadosMuestreo, "TRANSPARENCIA");
            var valorPROFUNDIDAD = ObtenerResultadoParametro(resultadosMuestreo, "PROFUNDIDAD");

            if (Convert.ToDecimal(valorTRANSPARENCIA) > Convert.ToDecimal(valorPROFUNDIDAD))
            {
                errores.Add("RR-42");
            }

            /*TEMP_AGUA_MED > TEMP_AGUA_SUP*/
            var valorTEMP_AGUA_MED = ObtenerResultadoParametro(resultadosMuestreo, "TEMP_AGUA_MED");
            var valorTEMP_AGUA_SUP = ObtenerResultadoParametro(resultadosMuestreo, "TEMP_AGUA_SUP");

            if (Convert.ToDecimal(valorTEMP_AGUA_MED) > Convert.ToDecimal(valorTEMP_AGUA_SUP))
            {
                errores.Add("RR-43");
            }

            /*TEMP_AGUA_FON > TEMP_AGUA_MED*/
            var valorTEMP_AGUA_FON = ObtenerResultadoParametro(resultadosMuestreo, "TEMP_AGUA_FON");

            if (Convert.ToDecimal(valorTEMP_AGUA_FON) > Convert.ToDecimal(valorTEMP_AGUA_MED))
            {
                errores.Add("RR-44");
            }

            /*DBO_SOL > DQO_SOL*/
            if (Convert.ToDecimal(valorDBO_SOL) > Convert.ToDecimal(valorDQO_SOL))
            {
                errores.Add("RR-45");
            }

            /*COP > COT*/
            var valorCOP = ObtenerResultadoParametro(resultadosMuestreo, "COP");

            if (Convert.ToDecimal(valorCOP) > Convert.ToDecimal(valorCOT))
            {
                errores.Add("RR-46");
            }

            /*N_ORG > N_TOTK*/
            var valorN_TOKT = ObtenerResultadoParametro(resultadosMuestreo, "N_TOTK");

            if (Convert.ToDecimal(valorN_ORG) > Convert.ToDecimal(valorN_TOKT))
            {
                errores.Add("RR-47");
            }

            /*P_INORG > P_TOT*/
            if (Convert.ToDecimal(valorP_INORG) > Convert.ToDecimal(valorP_TOT))
            {
                errores.Add("RR-48");
            }

            /*E_COLI > COLI_TOT*/
            if (Convert.ToDecimal(valorE_COLI) > Convert.ToDecimal(valorCOLI_TOT))
            {
                errores.Add("RR-49");
            }

            /*DQO_TOT > COT*/
            if (Convert.ToDecimal(valorDQO_TOT) > Convert.ToDecimal(valorCOT))
            {
                errores.Add("RR-50");
            }

            /*ALC_FEN > ALC_TOT*/
            var valorALC_FEN = ObtenerResultadoParametro(resultadosMuestreo, "ALC_FEN");
            var valorALC_TOT = ObtenerResultadoParametro(resultadosMuestreo, "ALC_TOT");

            if (Convert.ToDecimal(valorALC_FEN) > Convert.ToDecimal(valorALC_TOT))
            {
                errores.Add("RR-51");
            }

            /*SI OD_% = "<10" Y OD_mg/L <> "<1"*/
            var valorOD_mgL = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L");

            if (valorOD == "<10" && valorOD_mgL != "<1")
            {
                errores.Add("RR-61");
            }

            /*OD_%_SUP = "<10" Y OD_mg/L_SUP <> "<1"*/
            var valorOD_mgL_SUP = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L_SUP");

            if (valorOD_SUP == "<10" && valorOD_mgL_SUP != "<1")
            {
                errores.Add("RR-62");
            }

            /*OD_%_MED = "<10" Y OD_mg/L_MED <> "<1"*/
            var valorOD_mgL_MED = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L_MED");

            if (valorOD_MED == "<10" && valorOD_mgL_MED != "<1")
            {
                errores.Add("RR-63");
            }

            /*OD_%_FON = "<10" Y OD_mg/L_FON <> "<1"*/
            var valorOD_mgL_FON = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L_FON");

            if (valorOD_FON == "<10" && valorOD_mgL_FON != "<1")
            {
                errores.Add("RR-64");
            }

            /*OD_%_S1 = "<10" Y OD_mg/L_S1 <> "<1"*/
            var valorOD_S1 = ObtenerResultadoParametro(resultadosMuestreo, "OD_%_S1");
            var valorOD_mgL_S1 = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L_S1");

            if (valorOD_S1 == "<10" && valorOD_mgL_S1 != "<1")
            {
                errores.Add("RR-65");
            }

            /*OD_%_S2 = "<10" Y OD_mg/L_S2 <> "<1"*/
            var valorOD_S2 = ObtenerResultadoParametro(resultadosMuestreo, "OD_%_S2");
            var valorOD_mgL_S2 = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L_S2");

            if (valorOD_S2 == "<10" && valorOD_mgL_S2 != "<1")
            {
                errores.Add("RR-66");
            }

            /*OD_%_S3 = "<10" Y OD_mg/L_S3 <> "<1"*/
            var valorOD_S3 = ObtenerResultadoParametro(resultadosMuestreo, "OD_%_S3");
            var valorOD_mgL_S3 = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L_S3");

            if (valorOD_S3 == "<10" && valorOD_mgL_S3 != "<1")
            {
                errores.Add("RR-67");
            }

            /*OD_%_S4 = "<10" Y OD_mg/L_S4 <> "<1"*/
            var valorOD_S4 = ObtenerResultadoParametro(resultadosMuestreo, "OD_%_S4");
            var valorOD_mgL_S4 = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L_S4");

            if (valorOD_S4 == "<10" && valorOD_mgL_S4 != "<1")
            {
                errores.Add("RR-68");
            }

            /*OD_%_S5 = "<10" Y OD_mg/L_S5 <> "<1"*/
            var valorOD_S5 = ObtenerResultadoParametro(resultadosMuestreo, "OD_%_S5");
            var valorOD_mgL_S5 = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L_S5");

            if (valorOD_S5 == "<10" && valorOD_mgL_S5 != "<1")
            {
                errores.Add("RR-69");
            }

            /*OD_%_S6 = "<10" Y OD_mg/L_S6 <> "<1"*/
            var valorOD_S6 = ObtenerResultadoParametro(resultadosMuestreo, "OD_%_S6");
            var valorOD_mgL_S6 = ObtenerResultadoParametro(resultadosMuestreo, "OD_mg/L_S6");

            if (valorOD_S6 == "<10" && valorOD_mgL_S6 != "<1")
            {
                errores.Add("RR-70");
            }
        }

        public static string ObtenerResultadoParametro(List<ResultadoMuestreo> resultadosMuestreo, string claveParametro)
        {
            return resultadosMuestreo.Where(x => x.Parametro.ClaveParametro.Equals(claveParametro)).FirstOrDefault().Resultado;
        }
    }
}
