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
        private readonly IReglasLaboratorioLDMRepository _ldmLpcLaboratorio;
        private readonly IParametroRepository _parametroRepository;

        public ValidarResultadosPorReglasCommandHandler(
            IMuestreoRepository muestreoRepository, 
            IResultado resultadosRepository,
            IReglasMinimoMaximoRepository reglasMinimoMaximoRepository,
            IReglaService regla,
            IReglasReporteRepository reglasReporteRepository,
            IFormaReporteEspecificaRepository formaReporteEspecificaRepository,
            IReglasLaboratorioLDMRepository ldmLpcLaboratorio,
            IParametroRepository parametroRepository)
        {
            _muestreoRepository = muestreoRepository;
            _resultadosRepository=resultadosRepository;
            _reglasMinimoMaximoRepository=reglasMinimoMaximoRepository;
            _regla=regla;
            _reglasReporteRepository=reglasReporteRepository;
            _formaReporteEspecificaRepository=formaReporteEspecificaRepository;
            _ldmLpcLaboratorio=ldmLpcLaboratorio;
            _parametroRepository=parametroRepository;
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
                var resultadosNoValidos = new List<string>();

                /*Vamos a recorrer cada muestreo para aplicarle las reglas*/
                foreach (var muestreo in muestreos)
                {
                    var resultadosMuestreo = await _resultadosRepository.ObtenerElementosPorCriterioAsync(x => x.MuestreoId == muestreo.Id);
                    AplicarReglasDeRelacion(resultadosMuestreo);
                }

                resultadosValidacion = await _resultadosRepository.ObtenerResultadosValidacion(muestreos.Select(x => x.Id).ToList());
            }

            return new Response<List<ResultadoValidacionReglasDto>>(resultadosValidacion);
        }
        public void AplicarReglasDeRelacion(IEnumerable<ResultadoMuestreo> resultadosMuestreo)
        {
            /*Se iran ejecutando una a una las reglas de relación*/
            List<string> errores = new();

            /*COLI_FEC > COLI_TOT */
            ReglaMayorQue("COLI_FEC", "COLI_TOT", "RR-1 - COLI_FEC > COLI_TOT", resultadosMuestreo, errores);

            /*E_COLI > COLI_FEC*/
            ReglaMayorQue("E_COLI", "COLI_FEC", "RR-2 - E_COLI > COLI_FEC", resultadosMuestreo, errores);

            /*DBO_SOL > DBO_TOT*/
            ReglaMayorQue("DBO_SOL", "DBO_TOT", "RR-3 - DBO_SOL > DBO_TOT", resultadosMuestreo, errores);

            /*COT_SOL > COT*/
            ReglaMayorQue("COT_SOL", "COT", "RR-4 - COT_SOL > COT", resultadosMuestreo, errores);

            /*AOXF > AOXT*/
            ReglaMayorQue("AOXF", "AOXT", "RR-5 - AOXF > AOXT", resultadosMuestreo, errores);

            /*AOXP > AOXT*/
            ReglaMayorQue("AOXP", "AOXT", "RR-6 - AOXP > AOXT", resultadosMuestreo, errores);

            /*DBO_TOT > DQO_TOT*/
            ReglaMayorQue("DBO_TOT", "DQO_TOT", "RR-7 - DBO_TOT > DQO_TOT", resultadosMuestreo, errores);

            /*DQO_SOL > DQO_TOT*/
            ReglaMayorQue("DQO_SOL", "DQO_TOT", "RR-8 - DQO_SOL > DQO_TOT", resultadosMuestreo, errores);

            /*N_NH3 > N_TOTK*/
            ReglaMayorQue("N_NH3", "N_TOTK", "RR-16 - N_NH3 > N_TOTK", resultadosMuestreo, errores);

            /*TRANSPARENCIA > PROFUNDIDAD*/
            ReglaMayorQue("TRANSPARENCIA", "PROFUNDIDAD", "RR-42 - TRANSPARENCIA > PROFUNDIDAD", resultadosMuestreo, errores);

            /*TEMP_AGUA_MED > TEMP_AGUA_SUP*/
            ReglaMayorQue("TEMP_AGUA_MED", "TEMP_AGUA_SUP", "RR-43 - TEMP_AGUA_MED > TEMP_AGUA_SUP", resultadosMuestreo, errores);

            /*TEMP_AGUA_FON > TEMP_AGUA_MED*/
            ReglaMayorQue("TEMP_AGUA_FON", "TEMP_AGUA_MED", "RR-44 - TEMP_AGUA_FON > TEMP_AGUA_MED", resultadosMuestreo, errores);

            /*DBO_SOL > DQO_SOL*/
            ReglaMayorQue("DBO_SOL", "DQO_SOL", "RR-45 - DBO_SOL > DQO_SOL", resultadosMuestreo, errores);

            /*COP > COT*/
            ReglaMayorQue("COP", "COT", "RR-46 - COP > COT", resultadosMuestreo, errores);

            /*N_ORG > N_TOTK*/
            ReglaMayorQue("N_ORG", "N_TOTK", "RR-47 - N_ORG > N_TOTK", resultadosMuestreo, errores);

            /*P_INORG > P_TOT*/
            ReglaMayorQue("P_INORG", "P_TOT", "RR-48 - P_INORG > P_TOT", resultadosMuestreo, errores);

            /*E_COLI > COLI_TOT*/
            ReglaMayorQue("E_COLI", "COLI_TOT", "RR-49 - E_COLI > COLI_TOT", resultadosMuestreo, errores);

            /*DQO_TOT > COT*/
            ReglaMayorQue("DQO_TOT", "COT", "RR-50 - DQO_TOT > COT", resultadosMuestreo, errores);

            /*ALC_FEN > ALC_TOT*/
            ReglaMayorQue("ALC_FEN", "ALC_TOT", "RR-51 - ALC_FEN > ALC_TOT", resultadosMuestreo, errores);

            /*SI OD_% = "<10" Y OD_mg/L <> "<1"*/
            ReglaOD("OD_%", "OD_mg/L", "RR-61 - SI OD_% = \"<10\" Y OD_mg/L <> \"<1\"", resultadosMuestreo, errores);

            /*SI OD_% = "<10" Y OD_mg/L <> "<1"*/
            ReglaOD("OD_%", "OD_mg/L", "RR-61 - SI OD_% = \"<10\" Y OD_mg/L <> \"<1\"", resultadosMuestreo, errores);

            /*OD_%_SUP = "<10" Y OD_mg/L_SUP <> "<1"*/
            ReglaOD("OD_%_SUP", "OD_mg/L_SUP", "RR-62 - OD_%_SUP = \"<10\" Y OD_mg/L_SUP <> \"<1\"", resultadosMuestreo, errores);

            /*OD_%_MED = "<10" Y OD_mg/L_MED <> "<1"*/
            ReglaOD("OD_%_MED", "OD_mg/L_MED", "RR-63 - OD_%_MED = \"<10\" Y OD_mg/L_MED <> \"<1\"", resultadosMuestreo, errores);

            /*OD_%_FON = "<10" Y OD_mg/L_FON <> "<1"*/
            ReglaOD("OD_%_FON", "OD_mg/L_FON", "RR-64 - OD_%_FON = \"<10\" Y OD_mg/L_FON <> \"<1\"", resultadosMuestreo, errores);

            /*OD_%_S1 = "<10" Y OD_mg/L_S1 <> "<1"*/
            ReglaOD("OD_%_S1", "OD_mg/L_S1", "RR-65 - OD_%_S1 = \"<10\" Y OD_mg/L_S1 <> \"<1\"", resultadosMuestreo, errores);

            /*OD_%_S2 = "<10" Y OD_mg/L_S2 <> "<1"*/
            ReglaOD("OD_%_S2", "OD_mg/L_S2", "RR-66 - OD_%_S2 = \"<10\" Y OD_mg/L_S2 <> \"<1\"", resultadosMuestreo, errores);

            /*OD_%_S3 = "<10" Y OD_mg/L_S3 <> "<1"*/
            ReglaOD("OD_%_S3", "OD_mg/L_S3", "RR-67 - OD_%_S3 = \"<10\" Y OD_mg/L_S3 <> \"<1\"", resultadosMuestreo, errores);

            /*OD_%_S4 = "<10" Y OD_mg/L_S4 <> "<1"*/
            ReglaOD("OD_%_S4", "OD_mg/L_S4", "RR-68 - OD_%_S4 = \"<10\" Y OD_mg/L_S4 <> \"<1\"", resultadosMuestreo, errores);

            /*OD_%_S5 = "<10" Y OD_mg/L_S5 <> "<1"*/
            ReglaOD("OD_%_S5", "OD_mg/L_S5", "RR-69 - OD_%_S5 = \"<10\" Y OD_mg/L_S5 <> \"<1\"", resultadosMuestreo, errores);

            /*OD_%_S6 = "<10" Y OD_mg/L_S6 <> "<1"*/
            ReglaOD("OD_%_S6", "OD_mg/L_S6", "RR-70 - OD_%_S6 = \"<10\" Y OD_mg/L_S6 <> \"<1\"", resultadosMuestreo, errores);

            /*TOX_D_48_UT <> 100/TOX_D_48_EC50*/
            ReglaTOX("TOX_D_48_UT", "TOX_D_48_EC50", "RR-22 - TOX_D_48_UT <> 100/TOX_D_48_EC50", resultadosMuestreo, errores);

            /*TOX_D_48_FON_UT <> 100/ TOX_D_48_FON_EC50*/
            ReglaTOX("TOX_D_48_FON_UT", "TOX_D_48_FON_EC50", "RR-23 - TOX_D_48_FON_UT  <> 100/ TOX_D_48_FON_EC50", resultadosMuestreo, errores);

            /*TOX_D_48_SUP_UT <> 100/ TOX_D_48_SUP_EC50*/
            ReglaTOX("TOX_D_48_SUP_UT", "TOX_D_48_SUP_EC50", "RR-24 - TOX_D_48_SUP_UT <> 100/ TOX_D_48_SUP_EC50", resultadosMuestreo, errores);

            /*TOX_V_15_UT <> 100/TOX_V15_EC50*/
            ReglaTOX("TOX_V_15_UT", "TOX_V15_EC50", "RR-25 - TOX_V_15_UT <> 100/TOX_V15_EC50", resultadosMuestreo, errores);

            /*TOX_V_30_UT <> 100/TOX_V30_EC50*/
            ReglaTOX("TOX_V_30_UT", "TOX_V30_EC50", "RR-26 - TOX_V_30_UT <> 100/TOX_V30_EC50", resultadosMuestreo, errores);

            /*TOX_V_5_UT <> 100/TOX_V5_EC50*/
            ReglaTOX("TOX_V_5_UT", "TOX_V5_EC50", "RR-27 - TOX_V_5_UT <> 100/TOX_V5_EC50", resultadosMuestreo, errores);

            /*TOX_FIS_FON_15_UT <> 100/TOX_FIS_FON_15_EC50*/
            ReglaTOX("TOX_FIS_FON_15_UT", "TOX_FIS_FON_15_EC50", "RR-28 - TOX_FIS_FON_15_UT <> 100/TOX_FIS_FON_15_EC50", resultadosMuestreo, errores);

            /*TOX_FIS_FON_30_UT <> 100/ TOX_FIS_FON_30_EC50*/
            ReglaTOX("TOX_FIS_FON_30_UT", "TOX_FIS_FON_30_EC50", "RR-29 - TOX_FIS_FON_30_UT <> 100/ TOX_FIS_FON_30_EC50", resultadosMuestreo, errores);

            /*TOX_FIS_FON_5_UT <> 100/TOX_FIS_FON_5_EC50*/
            ReglaTOX("TOX_FIS_FON_5_UT", "TOX_FIS_FON_5_EC50", "RR-30 - TOX_FIS_FON_5_UT <> 100/TOX_FIS_FON_5_EC50", resultadosMuestreo, errores);

            /*TOX_FIS_SUP_15_UT <> 100/TOX_FIS_SUP_15_EC50*/
            ReglaTOX("TOX_FIS_SUP_15_UT", "TOX_FIS_SUP_15_EC50", "RR-31 - TOX_FIS_SUP_15_UT <> 100/TOX_FIS_SUP_15_EC50", resultadosMuestreo, errores);

            /*TOX_FIS_SUP_30_UT <> 100/TOX_FIS_SUP_30_EC50*/
            ReglaTOX("TOX_FIS_SUP_30_UT", "TOX_FIS_SUP_30_EC50", "RR-32 - TOX_FIS_SUP_30_UT <> 100/TOX_FIS_SUP_30_EC50", resultadosMuestreo, errores);

            /*TOX_FIS_SUP_5_UT <> 100/TOX_FIS_SUP_5_EC50*/
            ReglaTOX("TOX_FIS_SUP_5_UT", "TOX_FIS_SUP_5_EC50", "RR-33 - TOX_FIS_SUP_5_UT <> 100/TOX_FIS_SUP_5_EC50", resultadosMuestreo, errores);

        }

        public LimiteDeteccion ObtenerValoresMinMax(ResultadoDto resultado)
        {
            LimiteDeteccion limites = new();
            var regla = _reglasMinimoMaximoRepository.ObtenerElementosPorCriterio(x => x.ParametroId == resultado.IdParametro);

            if (regla != null)
            {
                if (regla.First().Aplica)
                {
                    limites.Maximo = regla.Where(x => x.ClasificacionReglaId == 2).First().MinimoMaximo;
                    limites.Minimo = regla.Where(x => x.ClasificacionReglaId == 3).First().MinimoMaximo;
                }
                else
                {
                    var reglaLdmLpc = _ldmLpcLaboratorio.ObtenerElementosPorCriterio(x => x.ParametroId == resultado.IdParametro && x.LaboratorioId == resultado.IdLaboratorio);

                    if (reglaLdmLpc != null)
                    {
                        limites.Minimo = (bool)reglaLdmLpc.FirstOrDefault().EsLdm ? reglaLdmLpc.FirstOrDefault().Ldm : reglaLdmLpc.FirstOrDefault().Lpc;
                        limites.Maximo = (Convert.ToInt64(limites.Minimo) * 100).ToString();
                    }
                }
            }

            return limites;
        }

        public bool CumpleFormaReporteEspecifica(string valor, long parametroId)
        {
            var existe = _formaReporteEspecificaRepository.ExisteElemento(x => x.ParametroId == parametroId && x.Descripcion == valor).Result;
            return existe;
        } 

        public bool CumpleReglaReporte(string valor, long parametroId)
        {
            bool cumpleReglaReporte = false;
            /*Validamos la forma de reporte para cuando un valor no es númerico*/
            var formasReporte = new List<string>() { "<0", "NA", "NE", "IM", "<LD", "<CMC", "ND", "<LPC" };

            /*Buscamos si el valor del resultado corresponde con alguno de la lista de opciones*/
            var formaReporte = formasReporte.FirstOrDefault(x => x == valor);

            if (formaReporte is null)
                return cumpleReglaReporte;

            /*Si el valor corresponde con alguna forma de reporte, ahora necesitamos saber si es valida. Ocupamos la tabla, ReglasReporte*/
            var reglaReporte = _reglasReporteRepository.ObtenerElementosPorCriterioAsync(x => x.ParametroId == parametroId).Result.FirstOrDefault();

            if (reglaReporte is null)
                return cumpleReglaReporte;

            switch (formaReporte)
            {
                case "<0":
                    cumpleReglaReporte = reglaReporte.EsValidoMenorCero;
                    break;
                case "NA":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoNa;
                    break;
                case "NE":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoNe;
                    break;
                case "IM":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoIm;
                    break;
                case "<LD":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoMenorLd;
                    break;
                case "<CMC":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoMenorCmc;
                    break;
                case "ND":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoNd;
                    break;
                case "<LPC":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoMenorLpc;
                    break;
            }

            return cumpleReglaReporte;
        }

        public class LimiteDeteccion
        {
            public string Minimo { get; set; }
            public string Maximo { get; set; }
        }


        public ResultadoDto? ObtenerResultadoParametro(IEnumerable<ResultadoMuestreo> resultadosMuestreo, string claveParametro)
        {
            var parametroId = _parametroRepository.ObtenerElementosPorCriterio(x => x.ClaveParametro == claveParametro);
            var resultadoParametro = resultadosMuestreo.Where(x => x.ParametroId.Equals(parametroId)).FirstOrDefault();

            if (resultadoParametro == null)
                return null;

            return new ResultadoDto()
            {
                IdParametro = resultadoParametro.Id,
                Valor = resultadoParametro.Resultado,
                IdLaboratorio = 1
            };
        }

        public class ResultadoDto
        {
            public long IdParametro { get; set; }
            public string Valor { get; set; }
            public long IdLaboratorio { get; set; }
        }

        public List<string> ReglaMayorQue(string parametro1, string parametro2, string regla, IEnumerable<ResultadoMuestreo> resultadosMuestreo, List<string> errores)
        {
            var resultadoParametro1 = ObtenerResultadoParametro(resultadosMuestreo, parametro1);
            var resultadoParametro2 = ObtenerResultadoParametro(resultadosMuestreo, parametro2);

            if (resultadoParametro1 == null || resultadoParametro2 == null)
            {
                errores.Add($"Grupo incompleto - {regla}");
            }
            else
            {
                var esNumeroParametro1 = decimal.TryParse(resultadoParametro1.Valor, out decimal valorParametro1);
                var esNumeroParametro2 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro2);

                if (esNumeroParametro1 && esNumeroParametro2)
                {
                    var minMaxParametro1 = ObtenerValoresMinMax(resultadoParametro1);
                    var minMaxParametro2 = ObtenerValoresMinMax(resultadoParametro2);

                    if (minMaxParametro1 != null && minMaxParametro2 != null)
                    {
                        var cumpleLimitesParametro1 = _regla.CumpleLimitesDeteccion(minMaxParametro1, valorParametro1);
                        if (!cumpleLimitesParametro1)
                            errores.Add("Error: Límites de detección");

                        var cumpleLimitesParametro2 = _regla.CumpleLimitesDeteccion(minMaxParametro2, valorParametro2);
                        if (!cumpleLimitesParametro2)
                            errores.Add("Error: Límites de detección");
                    }

                    if (valorParametro1 > valorParametro2)
                        errores.Add($"{regla}");
                }
                else
                {
                    if (!CumpleFormaReporteEspecifica(resultadoParametro1.Valor, resultadoParametro1.IdParametro))
                    {
                        if (!CumpleReglaReporte(resultadoParametro1.Valor, resultadoParametro1.IdParametro))
                        {
                            errores.Add($"No se reconoce la forma de reporte: {resultadoParametro1.Valor}");
                        }
                    }

                    if (!CumpleFormaReporteEspecifica(resultadoParametro2.Valor, resultadoParametro2.IdParametro))
                    {
                        if (!CumpleReglaReporte(resultadoParametro2.Valor, resultadoParametro2.IdParametro))
                        {
                            errores.Add($"No se reconoce la forma de reporte: {resultadoParametro2.Valor}");
                        }
                    }
                }
            }

            return errores;
        }

        public List<string> ReglaOD(string parametro1, string parametro2, string regla, IEnumerable<ResultadoMuestreo> resultadosMuestreo, List<string> errores)
        {
            var resultadoParametro1 = ObtenerResultadoParametro(resultadosMuestreo, parametro1);
            var resultadoParametro2 = ObtenerResultadoParametro(resultadosMuestreo, parametro2);

            if (resultadoParametro1 == null || resultadoParametro2 == null)
            {
                errores.Add($"Grupo incompleto - {regla}");
            }
            else
            {
                var esNumeroParametro1 = decimal.TryParse(resultadoParametro1.Valor, out decimal valorParametro1);
                var esNumeroParametro2 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro2);

                if (esNumeroParametro1 && esNumeroParametro2)
                {
                    var minMaxParametro1 = ObtenerValoresMinMax(resultadoParametro1);
                    var minMaxParametro2 = ObtenerValoresMinMax(resultadoParametro2);

                    if (minMaxParametro1 != null && minMaxParametro2 != null)
                    {
                        var cumpleLimitesParametro1 = _regla.CumpleLimitesDeteccion(minMaxParametro1, valorParametro1);
                        if (!cumpleLimitesParametro1)
                            errores.Add("Error: Límites de detección");

                        var cumpleLimitesParametro2 = _regla.CumpleLimitesDeteccion(minMaxParametro2, valorParametro2);
                        if (!cumpleLimitesParametro2)
                            errores.Add("Error: Límites de detección");
                    }
                }
                else
                {
                    if (!CumpleFormaReporteEspecifica(resultadoParametro1.Valor, resultadoParametro1.IdParametro))
                    {
                        if (!CumpleReglaReporte(resultadoParametro1.Valor, resultadoParametro1.IdParametro))
                        {
                            errores.Add($"No se reconoce la forma de reporte: {resultadoParametro1.Valor}");
                        }
                    }

                    if (!CumpleFormaReporteEspecifica(resultadoParametro2.Valor, resultadoParametro2.IdParametro))
                    {
                        if (!CumpleReglaReporte(resultadoParametro2.Valor, resultadoParametro2.IdParametro))
                        {
                            errores.Add($"No se reconoce la forma de reporte: {resultadoParametro2.Valor}");
                        }
                    }

                    if (resultadoParametro1.Valor == "<10" && resultadoParametro2.Valor != "<1")
                        errores.Add($"{regla}");
                }
            }

            return errores;
        }

        public List<string> ReglaTOX(string parametro1, string parametro2, string regla, IEnumerable<ResultadoMuestreo> resultadosMuestreo, List<string> errores)
        {
            var resultadoParametro1 = ObtenerResultadoParametro(resultadosMuestreo, parametro1);
            var resultadoParametro2 = ObtenerResultadoParametro(resultadosMuestreo, parametro2);

            if (resultadoParametro1 == null || resultadoParametro2 == null)
            {
                errores.Add($"Grupo incompleto - {regla}");
            }
            else
            {
                var esNumeroParametro1 = decimal.TryParse(resultadoParametro1.Valor, out decimal valorParametro1);
                var esNumeroParametro2 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro2);

                if (esNumeroParametro1 && esNumeroParametro2)
                {
                    var minMaxParametro1 = ObtenerValoresMinMax(resultadoParametro1);
                    var minMaxParametro2 = ObtenerValoresMinMax(resultadoParametro2);

                    if (minMaxParametro1 != null && minMaxParametro2 != null)
                    {
                        var cumpleLimitesParametro1 = _regla.CumpleLimitesDeteccion(minMaxParametro1, valorParametro1);
                        if (!cumpleLimitesParametro1)
                            errores.Add("Error: Límites de detección");

                        var cumpleLimitesParametro2 = _regla.CumpleLimitesDeteccion(minMaxParametro2, valorParametro2);
                        if (!cumpleLimitesParametro2)
                            errores.Add("Error: Límites de detección");
                    }

                    if (valorParametro1 != (100 / valorParametro2))
                        errores.Add($"{regla}");
                }
                else
                {
                    if (!CumpleFormaReporteEspecifica(resultadoParametro1.Valor, resultadoParametro1.IdParametro))
                    {
                        if (!CumpleReglaReporte(resultadoParametro1.Valor, resultadoParametro1.IdParametro))
                        {
                            errores.Add($"No se reconoce la forma de reporte: {resultadoParametro1.Valor}");
                        }
                    }

                    if (!CumpleFormaReporteEspecifica(resultadoParametro2.Valor, resultadoParametro2.IdParametro))
                    {
                        if (!CumpleReglaReporte(resultadoParametro2.Valor, resultadoParametro2.IdParametro))
                        {
                            errores.Add($"No se reconoce la forma de reporte: {resultadoParametro2.Valor}");
                        }
                    }
                }
            }

            return errores;
        }

    }
}
