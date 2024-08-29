using Application.DTOs;
using Application.Enums;
using Application.Features.Catalogos.ParametrosGrupo.Queries;
using Application.Features.ObservacionesOCDL.Queries;
using Application.Features.Operacion.Muestreos.Commands.Carga;
using Application.Features.Operacion.Muestreos.Commands.Liberacion;
using Application.Features.Operacion.Resultados.Comands;
using Application.Features.Operacion.Resultados.Comands.Acumulacion;
using Application.Features.Operacion.Resultados.Queries;
using Application.Features.Operacion.RevisionResultados.Commands;
using Application.Features.Operacion.RevisionResultados.Queries;
using Application.Features.Resultados.Comands;
using Application.Features.ResumenResultados.Queries;
using Application.Features.RevisionResultados.Queries;
using Application.Features.Validados.Queries;
using Application.Models;
using Application.Wrappers;
using Domain.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
using System.Reflection;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class Resultados : BaseApiController
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        const int estatusVencido = (int)Application.Enums.EstatusMuestreo_1.PendienteDeEnvioAprobacionFinal;
        const int aprobacionSistema = (int)Application.Enums.TipoAprobacion.Sistema;

        public Resultados(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post(IFormFile formFile)
        {
            string filePath = string.Empty;

            if (formFile.Length > 0)
            {
                filePath = Path.GetTempFileName();
                using var stream = System.IO.File.Create(filePath);
                await formFile.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);
            ExcelService.Mappings = ExcelResultadosTotalesSettings.keyValues;
            var registros = ExcelService.Import<UpdateMuestreoExcelDto>(fileInfo, "ebaseca");
            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new UpdateResultadosExcelCommand { Parametros = registros }));
        }

        //CAMBIAR METODO AUN NO ESTA COMPLETO
        [HttpPost("CargarExcelObservacionesSECAIA")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CargarExcelObservacionesSECAIA(IFormFile formFile, int UserId)
        {
            string filePath = string.Empty;

            if (formFile.Length > 0)
            {
                filePath = Path.GetTempFileName();
                using var stream = System.IO.File.Create(filePath);
                await formFile.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);
            ExcelService.Mappings = ExcelResultadosTotalesSECAIASettings.keyValues;

            var registros = ExcelService.Import<UpdateMuestreoSECAIAExcelDto>(fileInfo, "ebaseca");
            //var registros = ExcelService.Import<UpdateMuestreoExcelDto>(fileInfo, "ebaseca");

            System.IO.File.Delete(filePath);

            //A PARTIR DE AQUI
            //return Ok(await Mediator.Send(new UpdateResultadosExcelCommand { Parametros = registros }));

            return Ok(await Mediator.Send(new UpdateResultadoExcelSecaiaCommand
            {
                Parametros = registros,
                UserId = UserId
            }));
        }

        [HttpGet("MuestreosxFiltro")]
        public async Task<IActionResult> Get(int estatusId, int userId, bool isOCDL)
        {
            return Ok(await Mediator.Send(new GetResumenRevisionResultados
            {
                EstatusId = estatusId,
                UserId = userId,
                isOCDL = isOCDL
            }));
        }

        [HttpGet("ResultadosMuestreoParametros")]
        public async Task<IActionResult> GetActionAsync(int id, bool isOCDL)
        {
            Response<List<ResultadoMuestreoDto>> lstResultados = await Mediator.Send(new GetResultadosParametrosQuery
            {
                UserId = id,
                isOCDL = isOCDL
            });

            List<ResultadoMuestreoDto> lstResultadosValidados = lstResultados.Data.ToList().Where(x => x.fechaLimiteRevisionVencidos >= DateTime.Now.Date).ToList();
            List<ResultadoMuestreoDto> lstResultadosVencidos = lstResultados.Data.ToList().Where(x => x.fechaLimiteRevisionVencidos < DateTime.Now.Date && (x.EstatusOCDL == null || x.EstatusSECAIA == null)).ToList();

            //Acatualizar a estatus vencidos
            if (lstResultadosVencidos.Count > 0)
            {
                foreach (var vencido in lstResultadosVencidos)
                {

                    await Mediator.Send(new UpdateResultadosCommand
                    {
                        MuestreosId = vencido.MuestreoId,
                        UsuarioId = 0,
                        TipoAprobId = aprobacionSistema,
                        lstparametros = vencido.lstParametros,
                        isOCDL = isOCDL,
                        EstatusId = estatusVencido
                    });
                }
            }

            return Ok(new Response<List<ResultadoMuestreoDto>>(lstResultadosValidados));

        }

        [HttpPost("ExportarExcelResultados")]
        public async Task<IActionResult> Post(List<ResultadoMuestreoDto> muestreos)
        {
            List<TotalesExcel> muestreosExcel = new();
            Response<List<ObservacionesDto>> observaciones = await Mediator.Send(new GetObservacionesQuery());
            int indice = 0;

            foreach (var datomuestreo in muestreos)
            {
                foreach (var datparametro in muestreos[indice].lstParametros)
                {
                    TotalesExcel datoresumen = new()
                    {
                        ClaveParametro = datparametro.ClaveParametro,
                        ClaveUnica = datparametro.ClaveUnica,
                        Resultado = datparametro.Resulatdo,
                        NumeroEntrega = datomuestreo.NoEntregaOCDL,
                        ClaveSitio = datomuestreo.ClaveSitio,
                        ClaveMonitoreo = datomuestreo.ClaveMonitoreo,
                        Nombre = datomuestreo.NombreSitio,
                        Laboratorio = datomuestreo.Laboratorio,
                        TipoCuerpoAgua = datomuestreo.TipoCuerpoAgua,
                        TipoCuerpoAguaOriginal = datomuestreo.TipoCuerpoAguaOriginal,
                        ObservacionOCDL = string.Empty
                    };
                    muestreosExcel.Add(datoresumen);
                }
                indice++;
            }

            var plantilla = new Plantilla(_configuration, _env);

            string templatePath = plantilla.ObtenerRutaPlantilla("TotalOCDL");

            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcelObservaciones(muestreosExcel, fileInfo, observaciones.Data);


            //ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("ExportarResultadosValidados")]
        public IActionResult ResultadosValidados(List<ResultadoMuestreoDto> muestreos)
        {
            var dataMuestreo = muestreos.OrderBy(x => x.ClaveUnica);
            List<ExcelValidadosOCDL> muestreosExcel = new();
            int indice = 0;

            foreach (var datomuestreo in dataMuestreo)
            {
                ExcelValidadosOCDL datoresumen = new()
                {
                    NumeroEntrega = datomuestreo.NoEntregaOCDL,
                    ClaveUnica = datomuestreo.ClaveUnica ?? string.Empty,
                    ClaveSitio = datomuestreo.ClaveSitio ?? string.Empty,
                    ClaveMonitoreo = datomuestreo.ClaveMonitoreo ?? string.Empty,
                    Nombre = datomuestreo.NombreSitio,
                    ClaveParametro = datomuestreo.ClaveParametro ?? string.Empty,
                    Laboratorio = datomuestreo.Laboratorio ?? string.Empty,
                    TipoCuerpoAgua = datomuestreo.TipoCuerpoAgua,
                    TipoCuerpoAguaOriginal = datomuestreo.TipoCuerpoAguaOriginal,
                    Resultado = datomuestreo.Resultado ?? string.Empty,
                    TipoAprobacion = datomuestreo.TipoAprobacion,
                    ResultadoCorrecto = datomuestreo.EsCorrectoResultado ?? string.Empty,
                    ObservacionOCDL = datomuestreo.Observaciones,
                    FechaLimite = datomuestreo.FechaLimiteRevision ?? string.Empty,
                    Usuario = datomuestreo.NombreUsuario,
                    FechaRealizacion = datomuestreo.FechaRealizacion ?? string.Empty,
                    Estatus = datomuestreo.EstatusResultado
                };
                muestreosExcel.Add(datoresumen);
                indice++;
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ResultadosValidados");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("ExportarExcelResulatdosSECAIA")]
        public IActionResult ExportarExcelTotalSecaia(List<ResultadoDescargaDto> muestreos, string tipoExcel, bool admin = false)
        {
            List<DescargaSecaia> lstSecaia = new List<DescargaSecaia>();
            List<DescargarConsultaFormato> lstConsulForm = new List<DescargarConsultaFormato>();

            foreach (var item in muestreos)
            {
                List<string> datosfinales = new();
                DescargaParametrosCabecerasDto dato = new DescargaParametrosCabecerasDto();
                DescargaSecaia secaia = new DescargaSecaia();
                DescargarConsultaFormato consulForm = new DescargarConsultaFormato();

                if (tipoExcel.ToUpper() == "SECAIA")
                {
                    secaia.NoEntregaOCDL = item.noEntregaOCDL;
                    secaia.Ocdl = item.ocdl;
                    secaia.NombreSitio = item.nombreSitio;
                    secaia.ClaveMonitoreo = item.claveMonitoreo;
                    secaia.FechaRealizacion = item.fechaRealizacion;
                    secaia.Laboratorio = item.laboratorio;
                    secaia.TipoCuerpoAgua = item.tipoCuerpoAgua;
                }
                else if (tipoExcel.ToUpper() == "RESULTADO" || tipoExcel.ToUpper() == "CONSULTA")
                {
                    if (admin)
                    {
                        consulForm.NoEntrega = item.noEntregaOCDL;
                    }
                    consulForm.ClaveSitioOriginal = item.claveSitioOriginal;
                    consulForm.ClaveSitio = item.claveSitio;
                    consulForm.ClaveMonitoreo = item.claveMonitoreo;
                    consulForm.FechaRealizacion = item.fechaRealizacion;
                    consulForm.Laboratorio = item.laboratorio;
                    consulForm.TipoCuerpoAguaOriginal = item.tipoCuerpoAgua;
                    consulForm.TipoCuerpoAgua = item.tipoHomologado;
                    consulForm.TipoSitio = item.tipoSitio;
                }
                foreach (ColumnaDto columna in muestreos[0].lstParametrosOrden)
                {
                    List<ParametrosDto> param = new();
                    param = item.lstParametros.Where(x => x.Id == columna.orden).ToList();

                    if (param.Count > 0)
                    {
                        datosfinales.Add(param[0].Resulatdo);
                    }
                    else
                    {
                        datosfinales.Add("");
                    }
                }
                int num = 0;
                PropertyInfo[] propiedad = typeof(DescargaParametrosCabecerasDto).GetProperties();
                foreach (PropertyInfo p in propiedad)
                {
                    if (tipoExcel.ToUpper() == "SECAIA")
                    {
                        p.SetValue(secaia, datosfinales[num]);
                    }
                    else if (tipoExcel == "RESULTADO" || tipoExcel.ToUpper() == "CONSULTA")
                    {
                        p.SetValue(consulForm, datosfinales[num].ToString());
                    }
                    num++;
                }

                if (tipoExcel.ToUpper() == "SECAIA")
                {
                    lstSecaia.Add(secaia);
                }
                else if (tipoExcel.ToUpper() == "RESULTADO" || tipoExcel.ToUpper() == "CONSULTA")
                {
                    lstConsulForm.Add(consulForm);
                }
            }
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = string.Empty;
            if (tipoExcel.ToUpper() == "SECAIA")
            {
                templatePath = plantilla.ObtenerRutaPlantilla("TotalSECAIA");
            }
            else if (tipoExcel.ToUpper() == "RESULTADO" || tipoExcel.ToUpper() == "CONSULTA")
            {
                templatePath = plantilla.ObtenerRutaPlantilla("ConsultaFormato");
            }

            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            if (tipoExcel.ToUpper() == "SECAIA")
            {
                ExcelService.ExportToExcel(lstSecaia, fileInfo, true);
            }
            else if (tipoExcel.ToUpper() == "RESULTADO" || tipoExcel.ToUpper() == "CONSULTA")
            {
                ExcelService.ExportToExcel(lstConsulForm, fileInfo, true);
            }
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("exportExcelResumenSECAIA")]
        public IActionResult ExportResultExcelResumenSECAIA(List<ResultadoMuestreoDto> muestreos)
        {
            List<MuestreosValidadosExcel> lstmuestreosExcel = new();

            List<TabResumenExcel> resTabResultados = new() {
              new TabResumenExcel() {
                Parametro = "TODOS",
                Resultados_Aprobados = muestreos.Where(x => (x.EsCorrectoResultado ?? string.Empty).Equals("SI")).Count(),
                Resultados_Rechazados = muestreos.Where(z => (z.EsCorrectoResultado ?? string.Empty).Equals("NO")).Count()
              }
            };

            List<TabResumenExcel> resTabParamRes = new();

            foreach (var item in muestreos)
            {
                TabResumenExcel dato = new()
                {
                    Parametro = item.ClaveParametro,
                    Resultados_Aprobados = muestreos.Where(x => x.ClaveParametro == item.ClaveParametro).Where(f => f.EsCorrectoResultado == "SI").Count(),
                    Resultados_Rechazados = muestreos.Where(x => x.ClaveParametro == item.ClaveParametro).Where(f => f.EsCorrectoResultado == "NO").Count()
                };
                resTabParamRes.Add(dato);
            }

            foreach (var item in muestreos)
            {
                MuestreosValidadosExcel muestreosvalid = new()
                {
                    noEntrega = item.NoEntregaOCDL,
                    claveUnica = item.ClaveUnica,
                    claveSitio = item.ClaveSitio,
                    claveMonitoreo = item.ClaveMonitoreo,
                    nombreSitio = item.NombreSitio,
                    claveParametro = item.ClaveParametro,
                    laboratorio = item.Laboratorio,
                    tipoCuerpoAgua = item.TipoCuerpoAgua,
                    resultado = item.Resultado,
                    observacionSECAIA = item.ObservacionSECAIA,
                    fechaRevision = item.FechaRevision,
                    nombreUsuario = item.NombreUsuario,
                    estatusResultado = item.EstatusResultado
                };
                lstmuestreosExcel.Add(muestreosvalid);
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ResumenSECAIA");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcelTwoSheets(lstmuestreosExcel, resTabResultados, resTabParamRes, fileInfo, true, true);

            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("exportExcelValidadosSECAIA")]
        public IActionResult ExportExcelValidadosSECAIA(List<ResultadoMuestreoDto> muestreos)
        {
            List<MuestreosValidadosExcel> lstmuestreosExcel = new();

            foreach (var item in muestreos)
            {
                MuestreosValidadosExcel muestreosvalid = new()
                {
                    noEntrega = item.NoEntregaOCDL,
                    claveUnica = item.ClaveUnica,
                    claveSitio = item.ClaveSitio,
                    claveMonitoreo = item.ClaveMonitoreo,
                    nombreSitio = item.NombreSitio,
                    claveParametro = item.ClaveParametro,
                    laboratorio = item.Laboratorio,
                    tipoCuerpoAgua = item.TipoCuerpoAgua,
                    resultado = item.Resultado,
                    observacionSECAIA = item.ObservacionSECAIA,
                    fechaRevision = item.FechaRevision,
                    nombreUsuario = item.NombreUsuario,
                    estatusResultado = item.EstatusResultado
                };
                lstmuestreosExcel.Add(muestreosvalid);
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ValidadosSECAIA");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(lstmuestreosExcel, fileInfo, true);

            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("ExportToExcelTwoSheets")]
        public IActionResult ActionResult(List<ResultadoMuestreoDto> resultados)
        {
            List<ResumenExcel> resumenExcel = new();

            List<TabResumenExcel> resTabResultados = new() {
              new TabResumenExcel() {
                Parametro = "TODOS",
                Resultados_Aprobados = resultados.Where(x => (x.EsCorrectoResultado ?? string.Empty).Equals("SI")).Count(),
                Resultados_Rechazados = resultados.Where(z => (z.EsCorrectoResultado ?? string.Empty).Equals("NO")).Count()
              }
            };

            List<TabResumenExcel> resTabParamRes = new();
            foreach (var item in resultados)
            {
                TabResumenExcel dato = new()
                {
                    Parametro = item.ClaveParametro,
                    Resultados_Aprobados = resultados.Where(x => x.ClaveParametro == item.ClaveParametro).Where(f => f.EsCorrectoResultado == "SI").Count(),
                    Resultados_Rechazados = resultados.Where(x => x.ClaveParametro == item.ClaveParametro).Where(f => f.EsCorrectoResultado == "NO").Count()
                };
                resTabParamRes.Add(dato);
            }

            resultados.ForEach(resumen =>
                resumenExcel.Add(new ResumenExcel
                {
                    NoEntrega = resumen.NoEntregaOCDL,
                    ClaveUnica = resumen.ClaveUnica,
                    ClaveSitio = resumen.ClaveSitio,
                    ClaveMonitoreo = resumen.ClaveMonitoreo,
                    Nombre = resumen.NombreSitio,
                    ClaveParametro = resumen.ClaveParametro,
                    Laboratorio = resumen.Laboratorio,
                    TipoCuerpoAgua = resumen.TipoCuerpoAgua,
                    TipoCuerpoAguaOriginal = resumen.TipoCuerpoAguaOriginal,
                    Resultado = resumen.Resultado,
                    TipoAprobacion = resumen.TipoAprobacion,
                    ResultadoCorrecto = resumen.EsCorrectoResultado,
                    ObservacionOCDL = resumen.Observaciones,
                    FechaRevision = resumen.FechaRevision,
                    UsuarioRevision = resumen.NombreUsuario,
                    EstatusResultado = resumen.EstatusResultado
                }
            ));
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ValidadosOCDL");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcelTwoSheets(resumenExcel, resTabResultados, resTabParamRes, fileInfo, false, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));

        }

        // ACTUALIZAR 
        [HttpPut("updateMuestreoParametros")]
        public async Task<IActionResult> Put(List<UpdateMuestreoDto> request)
        {
            try
            {
                foreach (var muestreo in request)
                {
                    await Mediator.Send(new UpdateResultadosCommand
                    {
                        MuestreosId = muestreo.MuestreoId,
                        UsuarioId = muestreo.UsuarioId,
                        TipoAprobId = muestreo.TipoAprobId,
                        lstparametros = muestreo.lstparametros,
                        isOCDL = muestreo.isOCDL,
                        EstatusId = muestreo.EstatusId
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                throw new ApplicationException(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(ResultadoDto resultados)
        {
            return Ok(await Mediator.Send(new ActualizarResultadoCommand { Resultados = resultados }));
        }

        [HttpPut("updateParametros")]
        public async Task<IActionResult> Put(List<ResultadoMuestreoDto> request)
        {
            return Ok(await Mediator.Send(new ActualizarParametroCommand { Parametros = request }));
        }

        [HttpGet("ParametrosMuestreo")]
        public async Task<IActionResult> Get(int usuario, string? filter, int page, int pageSize, string? order)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            OrderBy orderBy = null;

            if (!string.IsNullOrEmpty(order) && order.Split('_').Length == 2)
            {
                orderBy = new OrderBy
                {
                    Column = order.Split('_')[0],
                    Type = order.Split('_')[1]
                };
            }

            return Ok(await Mediator.Send(new GetResultadosParametrosPaginados
            {
                UserId = usuario,
                Filter = filters,
                Page = page,
                PageSize = pageSize,
                //Filter = filters,
                //OrderBy = orderBy
            }));
        }

        [HttpGet("GetDistinctValuesFromColumn")]
        public async Task<IActionResult> Get(int usuario, string column, string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            return Ok(await Mediator.Send(new Application.Features.Resultados.Queries.GetDistinctValuesFromColumn { UserId = usuario, Column = column, Filters = filters }));
        }

        [HttpGet("GetDistinctValuesParametro")]
        public async Task<IActionResult> GetDistinctValuesParametro(int usuario, string parametro, string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            return Ok(await Mediator.Send(new GetDistinctValuesParametro { Usuario = usuario, ClaveParametro = parametro, Filter = filters }));
        }

        [HttpGet("ValidarResultadosPorReglas")]
        public async Task<IActionResult> Get([FromQuery] ValidarResultadosPorReglasCommand request)
        {
            return Ok(await Mediator.Send(new ValidarResultadosPorReglasCommand { Muestreos = request.Muestreos }));
        }

        [HttpGet("ExportarResumenValidacion")]
        public async Task<IActionResult> Get([FromQuery] GetResumenValidacionReglas request)
        {
            var registros = await Mediator.Send(new GetResumenValidacionReglas { Anios = request.Anios, NumeroEntrega = request.NumeroEntrega });

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ResumenValidacionReglas");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(registros.Data, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpGet("ResultadosAcumuladosParametros")]
        public async Task<IActionResult> GetActionAsync(int estatusId, int page, int pageSize, string? filter = "", string? order = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            OrderBy orderBy = null;

            if (!string.IsNullOrEmpty(order) && order.Split('_').Length == 2)
            {
                orderBy = new OrderBy
                {
                    Column = order.Split('_')[0],
                    Type = order.Split('_')[1]
                };
            }

            return Ok(await Mediator.Send(new GetResultadosMuestreoEstatusMuestreoPaginadosQuery
            {
                EstatusId = estatusId,
                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));

        }

        [HttpPut("EnviarModuloInicialReglas")]
        public async Task<IActionResult> EnviarModuloInicialReglas([FromBody] IEnumerable<long> muestreos, [FromQuery] string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            if (!muestreos.Any())
            {
                var response = await Mediator.Send(new GetResultadosMuestreoEstatusMuestreoPaginadosQuery
                {
                    EstatusId = (int)EstatusMuestreo.AcumulacionResultados,
                    Filter = filters,
                });

                muestreos = response.Data.Select(s => s.MuestreoId);
            }

            return Ok(await Mediator.Send(new EnviarInicialReglasCommand { Muestreos = muestreos.Distinct() }));
        }

        [HttpGet("GetColumnValuesResultadosAcumuladosParametros")]
        public IActionResult GetColumnValuesResultadosAcumuladosParametros(int estatusId, string column, string? filter = "", string? order = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            OrderBy orderBy = null;

            if (!string.IsNullOrEmpty(order) && order.Split('_').Length == 2)
            {
                orderBy = new OrderBy
                {
                    Column = order.Split('_')[0],
                    Type = order.Split('_')[1]
                };
            }

            var data = Mediator.Send(new GetResultadosMuestreoEstatusMuestreoPaginadosQuery
            {
                EstatusId = estatusId,
                Filter = filters,
                OrderBy = orderBy
            }).Result.Data;

            return Ok(new Response<object>(AuxQuery.GetDistinctValuesFromColumn(column, data)));
        }

        [HttpGet("ResultadosporMuestreo")]
        public async Task<IActionResult> GetResultadosporMuestreoAsync(int estatusId, int page, int pageSize, string? filter = "", string? order = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }
            OrderBy orderBy = null;

            if (!string.IsNullOrEmpty(order) && order.Split('_').Length == 2)
            {
                orderBy = new OrderBy
                {
                    Column = order.Split('_')[0],
                    Type = order.Split('_')[1]
                };
            }
            return Ok(await Mediator.Send(new GetResultadosporMuestreoPaginadosQuery
            {
                EstatusId = estatusId,
                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));
        }


        [HttpGet("ResultadosporMuestreoDistinct")]
        public async Task<IActionResult> ResultadosporMuestreoDistinct(int estatusId, string column, string? filter = "", string? order = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            OrderBy orderBy = null;

            if (!string.IsNullOrEmpty(order) && order.Split('_').Length == 2)
            {
                orderBy = new OrderBy
                {
                    Column = order.Split('_')[0],
                    Type = order.Split('_')[1]
                };
            }
            var data = Mediator.Send(new GetResultadosporMuestreoPaginadosQuery
            {
                EstatusId = estatusId,
                Filter = filters,
                OrderBy = orderBy
            }).Result.Data;

            return Ok(new Response<object>(AuxQuery.GetDistinctValuesFromColumn(column, data)));

        }


        //ExportaciondeExcelPantallasValidacionReglas cambiar por el filtrado
        [HttpPost("exportExcelValidaciones")]
        public IActionResult ExportExcelValidaciones([FromQuery] int estatusId, [FromQuery] string? filter, [FromBody] List<long> muestreos)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new GetResultadosMuestreoEstatusMuestreoPaginadosQuery
            {
                EstatusId = estatusId,
                Filter = filters,
            }).Result.Data;

            if (muestreos != null && muestreos.Any())
            {
                data = data.Where(x => muestreos.Contains(x.MuestreoId)).ToList();
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("AcumulacionResultados");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportAcumulacionResultadosExcel(data, fileInfo.FullName);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("exportExcelResultadosaValidar")]
        public IActionResult ExportExcelResultadosaValidar(List<AcumuladosResultadoDto> muestreos)
        {
            List<ResultadosValidarExcel> lstmuestreosExcel = new();
            foreach (var dato in muestreos)
            {
                ResultadosValidarExcel resultadosaValidar = new()
                {
                    ClaveSitio = dato.ClaveSitio,
                    ClaveMonitoreo = dato.ClaveMonitoreo,
                    NombreSitio = dato.NombreSitio,
                    FechaRealizacion = dato.FechaRealizacion,
                    FechaProgramada = dato.FechaProgramada,
                    DiferenciaDias = dato.DiferenciaDias,
                    FechaEntregaTeorica = dato.FechaEntregaTeorica,
                    LaboratorioRealizoMuestreo = dato.LaboratorioRealizoMuestreo,
                    CuerpoAgua = dato.CuerpoAgua,
                    TipoCuerpoAgua = dato.TipoCuerpoAgua,
                    SubTipoCuerpoAgua = dato.SubTipoCuerpoAgua,
                    NumParametrosEsperados = dato.NumParametrosEsperados,
                    NumParametrosCargados = dato.NumParametrosCargados,
                    MuestreoCompletoPorResultados = dato.MuestreoCompletoPorResultados,
                    CumpleReglasCond = dato.CumpleReglasCondic,
                    Observaciones = dato.ClaveParametro,
                    CumpleFechaEntrega = dato.CumpleFechaEntrega,
                    CumpleTodosCriteriosAplicarReglas = dato.CumpleTodosCriterios ? "SI" : "NO",
                    AutorizacionIncompleto = dato.AutorizacionIncompleto ? "SI" : "NO",
                    AutorizacionFechaEntrega = dato.AutorizacionFechaEntrega ? "SI" : "NO",
                    CorreReglaValidacion = dato.CorreReglaValidacion
                };
                lstmuestreosExcel.Add(resultadosaValidar);
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("InicioReglas");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(lstmuestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("exportExcelResultadosValidados")]
        public IActionResult ExportExcelResultadosValidados(List<AcumuladosResultadoDto> muestreos)
        {
            List<ResultadosValidadosExcel> lstmuestreosExcel = new();
            foreach (var dato in muestreos)
            {
                ResultadosValidadosExcel resultadosValidados = new()
                {
                    ClaveSitio = dato.ClaveSitio,
                    ClaveMonitoreo = dato.ClaveMonitoreo,
                    NombreSitio = dato.NombreSitio,
                    FechaRealizacion = dato.FechaRealizacion,
                    FechaProgramada = dato.FechaProgramada,
                    LaboratorioRealizoMuestreo = dato.LaboratorioRealizoMuestreo,
                    CuerpoAgua = dato.CuerpoAgua,
                    TipoCuerpoAgua = dato.TipoCuerpoAgua,
                    SubTipoCuerpoAgua = dato.SubTipoCuerpoAgua,
                    MuestreoCompletoPorResultados = dato.MuestreoCompletoPorResultados
                };

                lstmuestreosExcel.Add(resultadosValidados);
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ResumenCargaResultadosAValidar");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(lstmuestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("exportExcelResumenResultados")]
        public IActionResult ExportExcelResumenResultados([FromQuery] int estatus, List<long>? resultados, [FromQuery] string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new GetResultadosMuestreoEstatusMuestreoPaginadosQuery
            {
                EstatusId = estatus,
                Filter = filters
            }).Result.Data;

            var parametros = Mediator.Send(new GetAllParametros()).Result.Data;

            if (resultados != null && resultados.Any())
            {
                data = data.Where(x => resultados.Contains(x.ResultadoMuestreoId)).ToList();
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ResumenResultados");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcelResumenValidacionReglasResultado(data, fileInfo);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpGet("ResultadosParametrosEstatus")]
        public async Task<IActionResult> GetActionAsync(long userId, long estatusId)
        {
            Response<List<ResultadoMuestreoDto>> lstResultados = await Mediator.Send(new GetResultadosParametrosEstatusQuery
            {
                userId = userId,
                estatusId = estatusId
            });
            return Ok(lstResultados);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(List<long> resultados)
        { return Ok(await Mediator.Send(new DeleteResultadosByIdCommand { lstResultadosId = resultados })); }

        [HttpDelete("DeleteAllResultados")]
        public async Task<IActionResult> Delete(int estatusId, string? filter = "")
        {
            var filters = new List<Filter>();
            if (!string.IsNullOrEmpty(filter))
            { filters = QueryParam.GetFilters(filter); }

            return Ok(await Mediator.Send(new DeleteResultadosByFilterCommand { Filters = filters, estatusId = estatusId }));
        }

        [HttpDelete("DeleteByMuestreoId")]
        public async Task<IActionResult> DeleteByMuestreoId(List<long> lstMuestreosId)
        { return Ok(await Mediator.Send(new DeleteResultadosByMuestreoIdCommand { lstMuestreoId = lstMuestreosId })); }

        [HttpPost("ExportConsultaRegistroOriginal")]
        public async Task<IActionResult> ExportConsultaRegistroOriginal([FromQuery] int usuario, List<long>? muestreos, [FromQuery] string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new GetResultadosParametrosPaginados
            {
                UserId = usuario,
                Filter = filters
            }).Result.Data;

            var parametros = Mediator.Send(new GetAllParametros()).Result.Data;

            if (muestreos != null && muestreos.Any())
            {
                data = data.Where(x => muestreos.Contains(x.MuestreoId)).ToList();
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ResultadosSustituidos");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportConsultaRegistroOriginalExcel(data, parametros, fileInfo.FullName);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }


        [HttpPost("obtenerResultadosNoCumplenFechaEntrega")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> obtenerResultadosNoCumplenFechaEntrega(List<long> muestreosId)
        {
            //var parametros = new GetVwResultadosNoCumplenFechaEntregaQuery { muestreosId = muestreosId };

            var parametros = Mediator.Send(new GetVwResultadosNoCumplenFechaEntregaQuery { muestreosId = muestreosId }).Result.Data;

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ParametrosNoCumplenFechaEntrega");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(parametros.Select(x => new { x.ClaveMuestreo, x.FechaEntrega, x.FechaMaxima, x.ClaveParametro }), fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("CargaObservacionesResumenValidacionReglas")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CargaObservacionesResumenValidacionReglas(IFormFile archivo)
        {
            string filePath = string.Empty;

            if (archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelResumenValidacionReglasSettings.KeyValues;

            var registros = ExcelService.Import<ResumenValidacionReglasExcel>(fileInfo, "ResumenValidacionReglas_Resulta");

            if (!registros.Any())
            {
                throw new ValidationException("El archivo no contiene registros");
            }

            return Ok(await Mediator.Send(new CargaObservacionesResumenValidacionReglasCommand { Resultados = registros }));
        }

        [HttpPost("Liberar")]
        public async Task<IActionResult> Liberar(List<long>? muestreos, [FromQuery] string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            return Ok(await Mediator.Send(new LiberarCommand { Muestreos = muestreos, Filters = filters }));
        }

        [HttpPost("EnviarIncidencias")]
        public async Task<IActionResult> EnviarIncidencias(List<long>? resultadosId, [FromQuery] string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            return Ok(await Mediator.Send(new EnviarIncidenciasCommand { ResulatdosId = resultadosId, Filters = filters }));
        }
    }
}
