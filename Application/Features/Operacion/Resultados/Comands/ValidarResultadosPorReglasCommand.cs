using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class ValidarResultadosPorReglasCommand : IRequest<Response<List<ResultadoValidacionReglasDto>>>
    {
        public List<long> Muestreos { get; set; } = new List<long>();
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
        private IEnumerable<ReglasMinimoMaximo> reglasMinimoMaximos = new List<ReglasMinimoMaximo>();
        private IEnumerable<ReglasReporte> reglasReporte = new List<ReglasReporte>();
        private IEnumerable<FormaReporteEspecifica> formaReporteEspecifico = new List<FormaReporteEspecifica>();
        private IEnumerable<ReglasLaboratorioLdmLpc> ldmLpcLab = new List<ReglasLaboratorioLdmLpc>();

        public ValidarResultadosPorReglasCommandHandler(
            IMuestreoRepository muestreoRepository,
            IResultado resultadosRepository,
            IReglasMinimoMaximoRepository reglasMinimoMaximoRepository,
            IReglaService regla,
            IReglasReporteRepository reglasReporteRepository,
            IFormaReporteEspecificaRepository formaReporteEspecificaRepository,
            IReglasLaboratorioLDMRepository ldmLpcLaboratorio)
        {
            _muestreoRepository = muestreoRepository;
            _resultadosRepository=resultadosRepository;
            _reglasMinimoMaximoRepository=reglasMinimoMaximoRepository;
            _regla=regla;
            _reglasReporteRepository=reglasReporteRepository;
            _formaReporteEspecificaRepository=formaReporteEspecificaRepository;
            _ldmLpcLaboratorio=ldmLpcLaboratorio;
        }

        public async Task<Response<List<ResultadoValidacionReglasDto>>> Handle(ValidarResultadosPorReglasCommand request, CancellationToken cancellationToken)
        {
            List<ResultadoValidacionReglasDto> resultadosValidacion = new();

            var muestreos = await _muestreoRepository.ObtenerElementosPorCriterioAsync(x => request.Muestreos.Contains(x.Id));

            if (muestreos.Any())
            {
                reglasMinimoMaximos = _reglasMinimoMaximoRepository.ObtenerTodosElementosAsync().Result;
                reglasReporte = _reglasReporteRepository.ObtenerTodosElementosAsync().Result;
                formaReporteEspecifico = _formaReporteEspecificaRepository.ObtenerTodosElementosAsync().Result;
                ldmLpcLab = _ldmLpcLaboratorio.ObtenerTodosElementosAsync().Result;

                foreach (var muestreo in muestreos)
                {
                    var resultadosMuestreo = await _resultadosRepository.ObtenerResultadosParaReglas(muestreo.Id);
                    AplicarReglasDeRelacion(resultadosMuestreo);

                    resultadosMuestreo.Where(x => !x.Validado).ToList().ForEach(resultado =>
                    {
                        var esNumero = decimal.TryParse(resultado.Valor, out decimal valorResultado);

                        if (esNumero)
                            ValidarLimitesDeteccion(resultado, valorResultado);
                        else
                            ValidarFormaReglaReporte(resultado);
                    });

                    muestreo.EstatusId = (int)Enums.EstatusMuestreo.ResumenValidaciónReglas;

                    _resultadosRepository.ActualizarResultadosValidadosPorReglas(resultadosMuestreo.ToList());
                    _muestreoRepository.Actualizar(muestreo);
                }

                resultadosValidacion = await _resultadosRepository.ObtenerResultadosValidacion(muestreos.Select(x => x.Id).ToList());
            }

            return new Response<List<ResultadoValidacionReglasDto>>(resultadosValidacion);
        }

        public void AplicarReglasDeRelacion(IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo)
        {
            /*Se iran ejecutando una a una las reglas de relación*/
            List<string> errores = new();

            /*RR-1 COLI_FEC > COLI_TOT */
            ReglaMayorQue("COLI_FEC", "COLI_TOT", "SE RECHAZA-ABSOLUTA-COLI_FEC > COLI_TOT", resultadosMuestreo, errores);

            /*RR-2 E_COLI > COLI_FEC*/
            ReglaMayorQue("E_COLI", "COLI_FEC", "SE RECHAZA-ABSOLUTA-E_COLI > COLI_FEC", resultadosMuestreo, errores);

            /*RR-3 DBO_SOL > DBO_TOT*/
            ReglaMayorQue("DBO_SOL", "DBO_TOT", "SE RECHAZA-ABSOLUTA-DBO_SOL > DBO_TOT", resultadosMuestreo, errores);

            /*RR-4 COT_SOL > COT*/
            ReglaMayorQue("COT_SOL", "COT", "SE RECHAZA-ABSOLUTA-COT_SOL > COT", resultadosMuestreo, errores);

            /*RR-5 AOXF > AOXT*/
            ReglaMayorQue("AOXF", "AOXT", "SE RECHAZA-ABSOLUTA-CAOXF > AOXT", resultadosMuestreo, errores);

            /*RR-6 AOXP > AOXT*/
            ReglaMayorQue("AOXP", "AOXT", "SE RECHAZA-ABSOLUTA-CAOXP > AOXT", resultadosMuestreo, errores);

            /*RR-7 DBO_TOT > DQO_TOT*/
            ReglaMayorQue("DBO_TOT", "DQO_TOT", "ALERTA-INCUMPLE CON DBO_TOT > DQO_TOT", resultadosMuestreo, errores);

            /*RR-8 DQO_SOL > DQO_TOT*/
            ReglaMayorQue("DQO_SOL", "DQO_TOT", "SE RECHAZA-ABSOLUTA-DQO_SOL > DQO_TOT", resultadosMuestreo, errores);

            /*RR-16 N_NH3 > N_TOTK*/
            ReglaMayorQue("N_NH3", "N_TOTK", "SE RECHAZA-ABSOLUTA-N_NH3 > N_TOTK", resultadosMuestreo, errores);

            /*RR-42 TRANSPARENCIA > PROFUNDIDAD*/
            ReglaMayorQue("TRANSPARENCIA", "PROFUNDIDAD", "SE RECHAZA-ABSOLUTA-TRANSPARENCIA > PROFUNDIDAD", resultadosMuestreo, errores);

            /*RR-43 TEMP_AGUA_MED > TEMP_AGUA_SUP*/
            ReglaMayorQue("TEMP_AGUA_MED", "TEMP_AGUA_SUP", "ALERTA-INCUMPLE CON TEMP_AGUA_MED > TEMP_AGUA_SUP", resultadosMuestreo, errores);

            /*RR-44 TEMP_AGUA_FON > TEMP_AGUA_MED*/
            ReglaMayorQue("TEMP_AGUA_FON", "TEMP_AGUA_MED", "ALERTA-INCUMPLE CON TEMP_AGUA_FON > TEMP_AGUA_MED", resultadosMuestreo, errores);

            /*RR-45 DBO_SOL > DQO_SOL*/
            ReglaMayorQue("DBO_SOL", "DQO_SOL", "ALERTA-INCUMPLE CON DBO_SOL > DQO_SOL", resultadosMuestreo, errores);

            /*RR-46 COP > COT*/
            ReglaMayorQue("COP", "COT", "SE RECHAZA-ABSOLUTA-COP > COT", resultadosMuestreo, errores);

            /*RR-47 N_ORG > N_TOTK*/
            ReglaMayorQue("N_ORG", "N_TOTK", "SE RECHAZA-ABSOLUTA-N_ORG > N_TOTK", resultadosMuestreo, errores);

            /*RR-48 P_INORG > P_TOT*/
            ReglaMayorQue("P_INORG", "P_TOT", "SE RECHAZA-ABSOLUTA-P_INORG > P_TOT", resultadosMuestreo, errores);

            /*RR-49 E_COLI > COLI_TOT*/
            ReglaMayorQue("E_COLI", "COLI_TOT", "SE RECHAZA-ABSOLUTA-E_COLI > COLI_TOT", resultadosMuestreo, errores);

            /*RR-50 DQO_TOT > COT*/
            ReglaMayorQue("DQO_TOT", "COT", "ALERTA- INCUMPLE CON DQO_TOT > COT", resultadosMuestreo, errores);

            /*RR-51 ALC_FEN > ALC_TOT*/
            ReglaMayorQue("ALC_FEN", "ALC_TOT", "SE RECHAZA-ABSOLUTA-ALC_FEN > ALC_TOT", resultadosMuestreo, errores);

            /*RR-61 SI OD_% = "<10" Y OD_mg/L <> "<1"*/
            ReglaOD("OD_%", "OD_mg/L", "SE RECHAZA-ABSOLUTA-OD_% = \"<10\" Y OD_mg/L <> \"<1\"", resultadosMuestreo, errores);

            /*RR-62 OD_%_SUP = "<10" Y OD_mg/L_SUP <> "<1"*/
            ReglaOD("OD_%_SUP", "OD_mg/L_SUP", "SE RECHAZA-ABSOLUTA-OD_%_SUP = \"<10\" Y OD_mg/L_SUP <> \"<1\"", resultadosMuestreo, errores);

            /*RR-63 OD_%_MED = "<10" Y OD_mg/L_MED <> "<1"*/
            ReglaOD("OD_%_MED", "OD_mg/L_MED", "SE RECHAZA-ABSOLUTA-OD_%_MED = \"<10\" Y OD_mg/L_MED <> \"<1\"", resultadosMuestreo, errores);

            /*RR-64 OD_%_FON = "<10" Y OD_mg/L_FON <> "<1"*/
            ReglaOD("OD_%_FON", "OD_mg/L_FON", "SE RECHAZA-ABSOLUTA-OD_%_FON = \"<10\" Y OD_mg/L_FON <> \"<1\"", resultadosMuestreo, errores);

            /*RR-65 OD_%_S1 = "<10" Y OD_mg/L_S1 <> "<1"*/
            ReglaOD("OD_%_S1", "OD_mg/L_S1", "SE RECHAZA-ABSOLUTA-OD_%_S1 = \"<10\" Y OD_mg/L_S1 <> \"<1\"", resultadosMuestreo, errores);

            /*RR-66 OD_%_S2 = "<10" Y OD_mg/L_S2 <> "<1"*/
            ReglaOD("OD_%_S2", "OD_mg/L_S2", "SE RECHAZA-ABSOLUTA-OD_%_S2 = \"<10\" Y OD_mg/L_S2 <> \"<1\"", resultadosMuestreo, errores);

            /*RR-67 OD_%_S3 = "<10" Y OD_mg/L_S3 <> "<1"*/
            ReglaOD("OD_%_S3", "OD_mg/L_S3", "SE RECHAZA-ABSOLUTA-OD_%_S3 = \"<10\" Y OD_mg/L_S3 <> \"<1\"", resultadosMuestreo, errores);

            /*RR-68 OD_%_S4 = "<10" Y OD_mg/L_S4 <> "<1"*/
            ReglaOD("OD_%_S4", "OD_mg/L_S4", "SE RECHAZA-ABSOLUTA-OD_%_S4 = \"<10\" Y OD_mg/L_S4 <> \"<1\"", resultadosMuestreo, errores);

            /*RR-69 OD_%_S5 = "<10" Y OD_mg/L_S5 <> "<1"*/
            ReglaOD("OD_%_S5", "OD_mg/L_S5", "SE RECHAZA-ABSOLUTA-OD_%_S5 = \"<10\" Y OD_mg/L_S5 <> \"<1\"", resultadosMuestreo, errores);

            /*RR-70 OD_%_S6 = "<10" Y OD_mg/L_S6 <> "<1"*/
            ReglaOD("OD_%_S6", "OD_mg/L_S6", "SE RECHAZA-ABSOLUTA-OD_%_S6 = \"<10\" Y OD_mg/L_S6 <> \"<1\"", resultadosMuestreo, errores);

            /*RR-22 TOX_D_48_UT <> 100/TOX_D_48_EC50*/
            ReglaTOX("TOX_D_48_UT", "TOX_D_48_EC50", "SE RECHAZA-ABSOLUTA-TOX_D_48_UT <> 100/TOX_D_48_EC50", resultadosMuestreo, errores);

            /*RR-23 TOX_D_48_FON_UT <> 100/ TOX_D_48_FON_EC50*/
            ReglaTOX("TOX_D_48_FON_UT", "TOX_D_48_FON_EC50", "SE RECHAZA-ABSOLUTA-TOX_D_48_FON_UT  <> 100/ TOX_D_48_FON_EC50", resultadosMuestreo, errores);

            /*RR-24 TOX_D_48_SUP_UT <> 100/ TOX_D_48_SUP_EC50*/
            ReglaTOX("TOX_D_48_SUP_UT", "TOX_D_48_SUP_EC50", "SE RECHAZA-ABSOLUTA-TOX_D_48_SUP_UT <> 100/ TOX_D_48_SUP_EC50", resultadosMuestreo, errores);

            /*RR-25 TOX_V_15_UT <> 100/TOX_V15_EC50*/
            ReglaTOX("TOX_V_15_UT", "TOX_V15_EC50", "SE RECHAZA-ABSOLUTA-TOX_V_15_UT <> 100/TOX_V15_EC50", resultadosMuestreo, errores);

            /*RR-26 TOX_V_30_UT <> 100/TOX_V30_EC50*/
            ReglaTOX("TOX_V_30_UT", "TOX_V30_EC50", "SE RECHAZA-ABSOLUTA-TOX_V_30_UT <> 100/TOX_V30_EC50", resultadosMuestreo, errores);

            /*RR-27 TOX_V_5_UT <> 100/TOX_V5_EC50*/
            ReglaTOX("TOX_V_5_UT", "TOX_V5_EC50", "SE RECHAZA-ABSOLUTA-TOX_V_5_UT <> 100/TOX_V5_EC50", resultadosMuestreo, errores);

            /*RR-28 TOX_FIS_FON_15_UT <> 100/TOX_FIS_FON_15_EC50*/
            ReglaTOX("TOX_FIS_FON_15_UT", "TOX_FIS_FON_15_EC50", "SE RECHAZA-ABSOLUTA-TOX_FIS_FON_15_UT <> 100/TOX_FIS_FON_15_EC50", resultadosMuestreo, errores);

            /*RR-29 TOX_FIS_FON_30_UT <> 100/ TOX_FIS_FON_30_EC50*/
            ReglaTOX("TOX_FIS_FON_30_UT", "TOX_FIS_FON_30_EC50", "SE RECHAZA-ABSOLUTA-TOX_FIS_FON_30_UT <> 100/ TOX_FIS_FON_30_EC50", resultadosMuestreo, errores);

            /*RR-30 TOX_FIS_FON_5_UT <> 100/TOX_FIS_FON_5_EC50*/
            ReglaTOX("TOX_FIS_FON_5_UT", "TOX_FIS_FON_5_EC50", "SE RECHAZA-ABSOLUTA-TOX_FIS_FON_5_UT <> 100/TOX_FIS_FON_5_EC50", resultadosMuestreo, errores);

            /*RR-31 TOX_FIS_SUP_15_UT <> 100/TOX_FIS_SUP_15_EC50*/
            ReglaTOX("TOX_FIS_SUP_15_UT", "TOX_FIS_SUP_15_EC50", "SE RECHAZA-ABSOLUTA-TOX_FIS_SUP_15_UT <> 100/TOX_FIS_SUP_15_EC50", resultadosMuestreo, errores);

            /*RR-32 TOX_FIS_SUP_30_UT <> 100/TOX_FIS_SUP_30_EC50*/
            ReglaTOX("TOX_FIS_SUP_30_UT", "TOX_FIS_SUP_30_EC50", "SE RECHAZA-ABSOLUTA-TOX_FIS_SUP_30_UT <> 100/TOX_FIS_SUP_30_EC50", resultadosMuestreo, errores);

            /*RR-33 TOX_FIS_SUP_5_UT <> 100/TOX_FIS_SUP_5_EC50*/
            ReglaTOX("TOX_FIS_SUP_5_UT", "TOX_FIS_SUP_5_EC50", "SE RECHAZA-ABSOLUTA-TOX_FIS_SUP_5_UT <> 100/TOX_FIS_SUP_5_EC50", resultadosMuestreo, errores);

            /*RR-34 SI OD_% >50% ENTONCES POT_REDOX_CAMPO < 0*/
            ReglaODPOT("OD_%", "POT_REDOX_CAMPO", "ALERTA- INCUMPLE CON SI OD_% >50% ENTONCES POT_REDOX_CAMPO < 0", resultadosMuestreo, errores);

            /*RR-35 SI OD_%_FON >50% ENTONCES POT_REDOX_CAMPO_FON < 0*/
            ReglaODPOT("OD_%_FON", "POT_REDOX_CAMPO_FON", "ALERTA- INCUMPLE CON SI OD_%_FON >50% ENTONCES POT_REDOX_CAMPO_FON < 0", resultadosMuestreo, errores);

            /*RR-36 SI OD_%_MED >50% ENTONCES POT_REDOX_CAMPO_MED < 0*/
            ReglaODPOT("OD_%_MED", "POT_REDOX_CAMPO_MED", "ALERTA- INCUMPLE CON SI OD_%_MED >50% ENTONCES POT_REDOX_CAMPO_MED < 0", resultadosMuestreo, errores);

            /*RR-37 SI OD_%_SUP >50% POT_REDOX_CAMPO_SUP < 0*/
            ReglaODPOT("OD_%_SUP", "POT_REDOX_CAMPO_SUP", "ALERTA- INCUMPLE CON SI OD_%_SUP >50% POT_REDOX_CAMPO_SUP < 0", resultadosMuestreo, errores);

            /*RR-15 P_TOT <> P_ORG + P_INORG*/
            ReglaPTOT("P_TOT", "P_ORG", "P_INORG", "SE RECHAZA-ABSOLUTA-P_TOT <> P_ORG + P_INORG", resultadosMuestreo, errores);

            /*RR-17 NTK <> (N_NH3 + N_ORG)*/
            ReglaNTK("NTK", "N_NH3", "N_ORG", "SE RECHAZA-ABSOLUTA-NTK <> (N_NH3 + N_ORG)", resultadosMuestreo, errores);

            /*RR-18 ORTO_PO4 / 3.06 > P_TOT*/
            ReglaORTOPTOT("ORTO_PO4", "P_TOT", "SE RECHAZA-ABSOLUTA-ORTO_PO4 / 3.06 > P_TOT", resultadosMuestreo, errores);

            /*RR-11 PO4_TOT / P_TOT < 2.6 ó > 3.6*/
            ReglaPOTOT("PO4_TOT", "P_TOT", "ALERTA- INCUMPLE CON PO4_TOT / P_TOT < 2.6 ó > 3.6", resultadosMuestreo, errores);

            /*RR-13 N_TOT <> N_NO2 + N_NO3 + (N_NH3 + NORG)*/
            ReglaNTOT("N_TOT", "N_NO2", "N_NO3", "N_NH3", "NORG", "SE RECHAZA-ABSOLUTA-N_TOT <> N_NO2 + N_NO3 + (N_NH3 + NORG)", resultadosMuestreo, errores);
        }

        public LimiteDeteccionDto? ObtenerValoresMinMax(ResultadoParametroReglasDto resultado)
        {
            var reglas = reglasMinimoMaximos.Where(x => x.ParametroId == resultado.IdParametro);

            if (reglas.Any())
            {
                if (reglas.First().Aplica)
                {
                    LimiteDeteccionDto limites = new()
                    {
                        Leyenda = reglas.First().Leyenda
                    };

                    if (reglas.Where(x => x.ClasificacionReglaId == 2).Any())
                    {
                        limites.Maximo = reglas.Where(x => x.ClasificacionReglaId == 2).First().MinimoMaximo.Replace(">", string.Empty);
                    }

                    if (reglas.Where(x => x.ClasificacionReglaId == 3).Any())
                    {
                        limites.Minimo = reglas.Where(x => x.ClasificacionReglaId == 3).First().MinimoMaximo.Replace("<", string.Empty);
                    }

                    return limites;
                }
                else
                {
                    var reglasLdmLpc = ldmLpcLab.Where(x => x.ParametroId == resultado.IdParametro && x.LaboratorioId == resultado.IdLaboratorio);

                    if (reglasLdmLpc.Any())
                    {
                        var regla = reglasLdmLpc.FirstOrDefault();

                        if (regla != null)
                        {
                            LimiteDeteccionDto limites = new()
                            {
                                Minimo = (regla.EsLdm != null && (bool)regla.EsLdm) ? regla.Ldm : regla.Lpc
                            };
                            limites.Maximo = limites.Minimo??string.Empty;

                            return limites;
                        }
                    }
                }
            }

            return null;
        }

        public bool CumpleFormaReporteEspecifica(string valor, long parametroId, out string leyenda)
        {
            leyenda = string.Empty;
            var formaReporteEspecifica = formaReporteEspecifico.Where(x => x.ParametroId == parametroId && x.Descripcion == valor);

            if (formaReporteEspecifica.Any())
            {
                if (valor != formaReporteEspecifica.FirstOrDefault()?.Descripcion)
                {
                    leyenda = "NO CUMPLE CON LA FORMA DE REPORTE ESPECIFICO";
                }
            }

            return formaReporteEspecifica.Any();
        }

        public bool CumpleReglaReporte(string valor, long parametroId, out string leyenda)
        {
            leyenda = string.Empty;
            bool cumpleReglaReporte = false;
            /*Validamos la forma de reporte para cuando un valor no es númerico*/
            var formasReporte = new List<string>() { "<0", "NA", "NE", "IM", "<LD", "<CMC", "ND", "<LPC" };

            /*Buscamos si el valor del resultado corresponde con alguno de la lista de opciones*/
            var formaReporte = formasReporte.FirstOrDefault(x => x == valor);

            if (formaReporte is null)
                return cumpleReglaReporte;

            /*Si el valor corresponde con alguna forma de reporte, ahora necesitamos saber si es valida. Ocupamos la tabla, ReglasReporte*/
            var reglaReporte = reglasReporte.Where(x => x.ParametroId == parametroId).FirstOrDefault();

            if (reglaReporte is null)
                return cumpleReglaReporte;

            switch (formaReporte)
            {
                case "<0":
                    cumpleReglaReporte = reglaReporte.EsValidoMenorCero;
                    leyenda = cumpleReglaReporte ? string.Empty : "RECHAZA RESULTADO \"<0\"";
                    break;
                case "NA":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoNa;
                    leyenda = cumpleReglaReporte ? string.Empty : "RECHAZA RESULTADO \"NA\"";
                    break;
                case "NE":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoNe;
                    leyenda = cumpleReglaReporte ? string.Empty : "RECHAZA RESULTADO \"NE\"";
                    break;
                case "IM":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoIm;
                    leyenda = cumpleReglaReporte ? string.Empty : "RECHAZA RESULTADO \"IM\"";
                    break;
                case "<LD":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoMenorLd;
                    leyenda = cumpleReglaReporte ? string.Empty : "RECHAZA RESULTADO \"<LD\"";
                    break;
                case "<CMC":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoMenorCmc;
                    leyenda = cumpleReglaReporte ? string.Empty : "RECHAZA RESULTADO \"<CMC\"";
                    break;
                case "ND":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoNd;
                    leyenda = cumpleReglaReporte ? string.Empty : "RECHAZA RESULTADO \"ND\"";
                    break;
                case "<LPC":
                    cumpleReglaReporte = reglaReporte.EsValidoResultadoMenorLpc;
                    leyenda = cumpleReglaReporte ? string.Empty : "RECHAZA RESULTADO \"<LPC\"";
                    break;
            }

            return cumpleReglaReporte;
        }

        public static ResultadoParametroReglasDto? ObtenerResultadoParametro(IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, string claveParametro)
        {
            var resultadoParametro = resultadosMuestreo.Where(x => x.ClaveParametro.Equals(claveParametro)).FirstOrDefault();

            return resultadoParametro;
        }
        private void ValidarFormaReglaReporte(ResultadoParametroReglasDto resultadoParametro)
        {
            if (!CumpleFormaReporteEspecifica(resultadoParametro.Valor, resultadoParametro.IdParametro, out string leyendaRE) && leyendaRE != string.Empty)
            {
                resultadoParametro.ResultadoReglas = leyendaRE;
            }
            else
            {
                if (!CumpleReglaReporte(resultadoParametro.Valor, resultadoParametro.IdParametro, out string leyendaReporte) && leyendaRE != string.Empty)
                {
                    resultadoParametro.ResultadoReglas = leyendaReporte;
                }
            }
        }

        private void ValidarLimitesDeteccion(ResultadoParametroReglasDto resultadoParametro, decimal valorParametro)
        {
            var minMaxParametro = ObtenerValoresMinMax(resultadoParametro);

            if (minMaxParametro != null && (decimal.TryParse(minMaxParametro.Maximo, out _) || decimal.TryParse(minMaxParametro.Minimo, out _)))
            {
                try
                {
                    var cumpleLimitesParametro = _regla.CumpleLimitesDeteccion(minMaxParametro, valorParametro);

                    if (!cumpleLimitesParametro)
                        resultadoParametro.ResultadoReglas = minMaxParametro.Leyenda;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public List<string> ReglaMayorQue(string parametro1, string parametro2, string regla, IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, List<string> errores)
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

                if (esNumeroParametro1)
                    ValidarLimitesDeteccion(resultadoParametro1, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro1);

                if (esNumeroParametro2)
                    ValidarLimitesDeteccion(resultadoParametro2, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro2);

                if (esNumeroParametro1 && esNumeroParametro2)
                {
                    if (valorParametro1 > valorParametro2)
                    {
                        resultadoParametro1.ResultadoReglas += regla;
                        resultadoParametro2.ResultadoReglas += regla;
                    }
                }

                resultadoParametro1.Validado = true;
                resultadoParametro2.Validado = true;
            }

            return errores;
        }

        public List<string> ReglaOD(string parametro1, string parametro2, string regla, IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, List<string> errores)
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

                if (esNumeroParametro1)
                    ValidarLimitesDeteccion(resultadoParametro1, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro1);

                if (esNumeroParametro2)
                    ValidarLimitesDeteccion(resultadoParametro2, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro2);

                if (!esNumeroParametro1 && !esNumeroParametro2)
                {
                    if (resultadoParametro1.Valor == "<10" && resultadoParametro2.Valor != "<1")
                    {
                        resultadoParametro1.ResultadoReglas += regla;
                        resultadoParametro2.ResultadoReglas += regla;
                    }
                }

                resultadoParametro1.Validado = true;
                resultadoParametro2.Validado = true;
            }

            return errores;
        }

        public List<string> ReglaTOX(string parametro1, string parametro2, string regla, IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, List<string> errores)
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

                if (esNumeroParametro1)
                    ValidarLimitesDeteccion(resultadoParametro1, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro1);

                if (esNumeroParametro2)
                    ValidarLimitesDeteccion(resultadoParametro2, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro2);

                if (esNumeroParametro1 && esNumeroParametro2)
                {
                    if (valorParametro1 != (100 / valorParametro2))
                    {
                        resultadoParametro1.ResultadoReglas += regla;
                        resultadoParametro2.ResultadoReglas += regla;
                    }
                }

                resultadoParametro1.Validado = true;
                resultadoParametro2.Validado = true;
            }

            return errores;
        }

        public List<string> ReglaODPOT(string parametro1, string parametro2, string regla, IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, List<string> errores)
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

                if (esNumeroParametro1)
                    ValidarLimitesDeteccion(resultadoParametro1, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro1);

                if (esNumeroParametro2)
                    ValidarLimitesDeteccion(resultadoParametro2, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro2);

                if (esNumeroParametro1 && esNumeroParametro2)
                {
                    if (valorParametro1 > 50 && valorParametro2 < 0)
                    {
                        resultadoParametro1.ResultadoReglas += regla;
                        resultadoParametro2.ResultadoReglas += regla;
                    }
                }

                resultadoParametro1.Validado = true;
                resultadoParametro2.Validado = true;
            }

            return errores;
        }

        /*RR-15*/
        public List<string> ReglaPTOT(string parametro1, string parametro2, string parametro3, string regla, IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, List<string> errores)
        {
            var resultadoParametro1 = ObtenerResultadoParametro(resultadosMuestreo, parametro1);
            var resultadoParametro2 = ObtenerResultadoParametro(resultadosMuestreo, parametro2);
            var resultadoParametro3 = ObtenerResultadoParametro(resultadosMuestreo, parametro3);

            if (resultadoParametro1 == null || resultadoParametro2 == null || resultadoParametro3 == null)
            {
                errores.Add($"Grupo incompleto - {regla}");
            }
            else
            {
                var esNumeroParametro1 = decimal.TryParse(resultadoParametro1.Valor, out decimal valorParametro1);
                var esNumeroParametro2 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro2);
                var esNumeroParametro3 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro3);

                if (esNumeroParametro1)
                    ValidarLimitesDeteccion(resultadoParametro1, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro1);

                if (esNumeroParametro2)
                    ValidarLimitesDeteccion(resultadoParametro2, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro2);

                if (esNumeroParametro3)
                    ValidarLimitesDeteccion(resultadoParametro3, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro3);

                if (esNumeroParametro1 && esNumeroParametro2 && esNumeroParametro3)
                {
                    if (valorParametro1 != (valorParametro2 + valorParametro3))
                    {
                        resultadoParametro1.ResultadoReglas = regla;
                        resultadoParametro2.ResultadoReglas = regla;
                        resultadoParametro3.ResultadoReglas = regla;
                    }
                }

                resultadoParametro1.Validado = true;
                resultadoParametro2.Validado = true;
                resultadoParametro3.Validado = true;
            }

            return errores;
        }

        /*RR-17*/
        public List<string> ReglaNTK(string parametro1, string parametro2, string parametro3, string regla, IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, List<string> errores)
        {
            var resultadoParametro1 = ObtenerResultadoParametro(resultadosMuestreo, parametro1);
            var resultadoParametro2 = ObtenerResultadoParametro(resultadosMuestreo, parametro2);
            var resultadoParametro3 = ObtenerResultadoParametro(resultadosMuestreo, parametro3);

            if (resultadoParametro1 == null || resultadoParametro2 == null || resultadoParametro3 == null)
            {
                errores.Add($"Grupo incompleto - {regla}");
            }
            else
            {
                var esNumeroParametro1 = decimal.TryParse(resultadoParametro1.Valor, out decimal valorParametro1);
                var esNumeroParametro2 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro2);
                var esNumeroParametro3 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro3);

                if (esNumeroParametro1)
                    ValidarLimitesDeteccion(resultadoParametro1, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro1);

                if (esNumeroParametro2)
                    ValidarLimitesDeteccion(resultadoParametro2, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro2);

                if (esNumeroParametro3)
                    ValidarLimitesDeteccion(resultadoParametro3, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro3);

                if (esNumeroParametro1 && esNumeroParametro2 && esNumeroParametro3)
                {
                    if (valorParametro1 != (valorParametro2 + valorParametro3))
                    {
                        resultadoParametro1.ResultadoReglas = regla;
                        resultadoParametro2.ResultadoReglas = regla;
                        resultadoParametro3.ResultadoReglas = regla;
                    }
                }

                resultadoParametro1.Validado = true;
                resultadoParametro2.Validado = true;
                resultadoParametro3.Validado = true;
            }

            return errores;
        }

        /*RR-18*/
        public List<string> ReglaORTOPTOT(string parametro1, string parametro2, string regla, IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, List<string> errores)
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

                if (esNumeroParametro1)
                    ValidarLimitesDeteccion(resultadoParametro1, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro1);

                if (esNumeroParametro2)
                    ValidarLimitesDeteccion(resultadoParametro2, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro2);

                if (esNumeroParametro1 && esNumeroParametro2)
                {
                    if ((valorParametro1 / 3.06M) > valorParametro2)
                    {
                        resultadoParametro1.ResultadoReglas = regla;
                        resultadoParametro2.ResultadoReglas = regla;
                    }
                }

                resultadoParametro1.Validado = true;
                resultadoParametro2.Validado = true;
            }

            return errores;
        }

        /*RR-11*/
        public List<string> ReglaPOTOT(string parametro1, string parametro2, string regla, IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, List<string> errores)
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

                if (esNumeroParametro1)
                    ValidarLimitesDeteccion(resultadoParametro1, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro1);

                if (esNumeroParametro2)
                    ValidarLimitesDeteccion(resultadoParametro2, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro2);

                if (esNumeroParametro1 && esNumeroParametro2)
                {
                    if ((valorParametro1 / valorParametro2) < 2.6M || (valorParametro1 / valorParametro2) > 3.6M)
                    {
                        resultadoParametro1.ResultadoReglas = regla;
                        resultadoParametro2.ResultadoReglas = regla;
                    }
                }

                resultadoParametro1.Validado = true;
                resultadoParametro2.Validado = true;
            }

            return errores;
        }

        /*RR-13*/
        public List<string> ReglaNTOT(string parametro1, string parametro2, string parametro3, string parametro4, string parametro5, string regla, IEnumerable<ResultadoParametroReglasDto> resultadosMuestreo, List<string> errores)
        {
            var resultadoParametro1 = ObtenerResultadoParametro(resultadosMuestreo, parametro1);
            var resultadoParametro2 = ObtenerResultadoParametro(resultadosMuestreo, parametro2);
            var resultadoParametro3 = ObtenerResultadoParametro(resultadosMuestreo, parametro3);
            var resultadoParametro4 = ObtenerResultadoParametro(resultadosMuestreo, parametro4);
            var resultadoParametro5 = ObtenerResultadoParametro(resultadosMuestreo, parametro5);

            if (resultadoParametro1 == null || resultadoParametro2 == null || resultadoParametro3 == null || resultadoParametro4 == null || resultadoParametro5 == null)
            {
                errores.Add($"Grupo incompleto - {regla}");
            }
            else
            {
                var esNumeroParametro1 = decimal.TryParse(resultadoParametro1.Valor, out decimal valorParametro1);
                var esNumeroParametro2 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro2);
                var esNumeroParametro3 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro3);
                var esNumeroParametro4 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro4);
                var esNumeroParametro5 = decimal.TryParse(resultadoParametro2.Valor, out decimal valorParametro5);

                if (esNumeroParametro1)
                    ValidarLimitesDeteccion(resultadoParametro1, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro1);

                if (esNumeroParametro2)
                    ValidarLimitesDeteccion(resultadoParametro2, valorParametro1);
                else
                    ValidarFormaReglaReporte(resultadoParametro2);

                if (esNumeroParametro3)
                    ValidarLimitesDeteccion(resultadoParametro3, valorParametro3);
                else
                    ValidarFormaReglaReporte(resultadoParametro3);

                if (esNumeroParametro4)
                    ValidarLimitesDeteccion(resultadoParametro4, valorParametro4);
                else
                    ValidarFormaReglaReporte(resultadoParametro4);

                if (esNumeroParametro5)
                    ValidarLimitesDeteccion(resultadoParametro5, valorParametro5);
                else
                    ValidarFormaReglaReporte(resultadoParametro5);

                if (esNumeroParametro1 && esNumeroParametro2 && esNumeroParametro3 && esNumeroParametro4 && esNumeroParametro5)
                {
                    if (valorParametro1 != (valorParametro2 + valorParametro3 + valorParametro4 + valorParametro5))
                    {
                        resultadoParametro1.ResultadoReglas = regla;
                        resultadoParametro2.ResultadoReglas = regla;
                        resultadoParametro3.ResultadoReglas = regla;
                        resultadoParametro4.ResultadoReglas = regla;
                        resultadoParametro5.ResultadoReglas = regla;
                    }
                }

                resultadoParametro1.Validado = true;
                resultadoParametro2.Validado = true;
                resultadoParametro3.Validado = true;
                resultadoParametro4.Validado = true;
                resultadoParametro5.Validado = true;
            }

            return errores;
        }
    }
}
