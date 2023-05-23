using Application.DTOs;
using Application.Features.ObservacionesOCDL.Queries;
using Application.Features.Operacion.Resultados.Comands;
using Application.Features.Operacion.RevisionResultados.Commands;
using Application.Features.Operacion.RevisionResultados.Queries;
using Application.Features.Resultados.Comands;
using Application.Features.ResumenResultados.Queries;
using Application.Features.RevisionResultados.Queries;
using Application.Features.Validados.Queries;
using Application.Models;
using Application.Wrappers;
using Domain.Entities;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
using System.Reflection;
using WebAPI.Controllers.v1.Catalogos;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class Resultados : BaseApiController
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        const int estatusVencido = (int)Application.Enums.EstatusMuestreo.PendienteDeEnvioAprobacionFinal;
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
            List<ResultadoMuestreoDto> lstResultadosVencidos = lstResultados.Data.ToList(). Where(x => x.fechaLimiteRevisionVencidos < DateTime.Now.Date && (x.EstatusOCDL == null || x.EstatusSECAIA == null)).ToList();
            
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
            List<ExcelValidadosOCDL> muestreosExcel = new List<ExcelValidadosOCDL>();
            int indice = 0;

            foreach (var datomuestreo in dataMuestreo)
            {
                ExcelValidadosOCDL datoresumen = new()
                {
                    NumeroEntrega = datomuestreo.NoEntregaOCDL,
                    ClaveUnica = datomuestreo.ClaveUnica,
                    ClaveSitio = datomuestreo.ClaveSitio,
                    ClaveMonitoreo = datomuestreo.ClaveMonitoreo,
                    Nombre = datomuestreo.NombreSitio,
                    ClaveParametro = datomuestreo.ClaveParametro,
                    Laboratorio = datomuestreo.Laboratorio,
                    TipoCuerpoAgua = datomuestreo.TipoCuerpoAgua,
                    TipoCuerpoAguaOriginal = datomuestreo.TipoCuerpoAguaOriginal,
                    Resultado = datomuestreo.Resultado,
                    TipoAprobacion = datomuestreo.TipoAprobacion,
                    ResultadoCorrecto = datomuestreo.EsCorrectoResultado,
                    ObservacionOCDL = datomuestreo.Observaciones,
                    FechaLimite = datomuestreo.FechaLimiteRevision,
                    Usuario = datomuestreo.NombreUsuario,
                    FechaRealizacion = datomuestreo.FechaRealizacion,
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
                
                if (tipoExcel == "secaia") {                  
                    secaia.NoEntregaOCDL = item.noEntregaOCDL;
                    secaia.Ocdl = item.ocdl;
                    secaia.NombreSitio = item.nombreSitio;
                    secaia.ClaveMonitoreo = item.claveMonitoreo;
                    secaia.FechaRealizacion = item.fechaRealizacion;
                    secaia.Laboratorio = item.laboratorio;
                    secaia.TipoCuerpoAgua = item.tipoCuerpoAgua;
                }
                else if (tipoExcel == "resultado" || tipoExcel == "consulta")
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
                    if (tipoExcel == "secaia")
                    {                        
                        p.SetValue(secaia, datosfinales[num]);
                    }
                    else if (tipoExcel == "resultado" || tipoExcel == "consulta")
                    {
                        p.SetValue(consulForm, datosfinales[num]);
                    }                    
                    num++;
                }                

                if (tipoExcel == "secaia")
                {
                    lstSecaia.Add(secaia);
                }
                else if (tipoExcel == "resultado" || tipoExcel == "consulta")
                {
                    lstConsulForm.Add(consulForm);
                }                
            }
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = string.Empty;
            if (tipoExcel == "secaia")
            {
                templatePath = plantilla.ObtenerRutaPlantilla("TotalSECAIA");
            }
            else if (tipoExcel == "Resultado" || tipoExcel == "consulta")
            {
                templatePath = plantilla.ObtenerRutaPlantilla("ConsultaFormato");
            }
             
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

           if (tipoExcel == "secaia")
            {
                ExcelService.ExportToExcel(lstSecaia, fileInfo, true);
            }
            else if (tipoExcel == "resultado" || tipoExcel == "consulta")
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
                Resultados_Aprobados = muestreos.Where(x => x.EsCorrectoResultado.Equals("SI")).Count(),
                Resultados_Rechazados = muestreos.Where(z => z.EsCorrectoResultado.Equals("NO")).Count()
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
        public async Task<IActionResult> ActionResultAsync(List<ResultadoMuestreoDto> resultados)
        {
            List<ResumenExcel> resumenExcel = new();

            List<TabResumenExcel> resTabResultados = new() {
              new TabResumenExcel() {
                Parametro = "TODOS",
                Resultados_Aprobados = resultados.Where(x => x.EsCorrectoResultado.Equals("SI")).Count(),
                Resultados_Rechazados = resultados.Where(z => z.EsCorrectoResultado.Equals("NO")).Count()
              }
            };

            List<TabResumenExcel> resTabParamRes = new();
            foreach(var item in resultados)
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
                    TipoCuerpoAguaOriginal =  resumen.TipoCuerpoAguaOriginal,
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

        [HttpGet("ResultadosMuestreoParametrosTemp")]
        public async Task<IActionResult> GetMuestreoParametrosTemp(int UserId, int CuerpAId, int EstausId, int anio)
        {
            return Ok(await Mediator.Send(new GetResultadosParametrosTempQuery
            {
                UserId = UserId,
                CuerpoAgua = CuerpAId,
                Estatus = EstausId,
                Anio = anio
            }));
        }

        [HttpGet("ValidarResultadosPorReglas")]
        public async Task<IActionResult> Get([FromQuery]ValidarResultadosPorReglasCommand request)
        {
            return Ok(await Mediator.Send(new ValidarResultadosPorReglasCommand { Anios = request.Anios, NumeroEntrega = request.NumeroEntrega }));
        }
    }
}
