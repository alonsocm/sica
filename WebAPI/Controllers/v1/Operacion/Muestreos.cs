using Application.DTOs;
using Application.DTOs.Users;
using Application.Features.Muestreos.Commands.Liberacion;
using Application.Features.Muestreos.Queries;
using Application.Features.Operacion.Muestreos.Commands.Actualizar;
using Application.Features.Operacion.Muestreos.Commands.Carga;
using Application.Features.Operacion.Muestreos.Queries;
using Application.Interfaces.IRepositories;
using Application.Models;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class Muestreos : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IProgramaAnioRepository _progrepor;

        public Muestreos(IConfiguration configuration, IWebHostEnvironment env, IProgramaAnioRepository progepo)
        {
            _configuration = configuration;
            _env = env;
            _progrepor = progepo;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] CargaMuestreos cargaMuestreos)
        {
            string filePath = string.Empty;

            if (cargaMuestreos.Archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await cargaMuestreos.Archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelSettings.keyValues;

            var registros = ExcelService.Import<CargaMuestreoDto>(fileInfo, "ebaseca");

            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CargaMuestreosCommand { Muestreos = registros, Validado = cargaMuestreos.Validado, Reemplazar = cargaMuestreos.Reemplazar }));
        }

        [HttpPost("CargaEmergencias")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] CargaEmergenciasDto cargaMuestreos)
        {
            string filePath = string.Empty;

            if (cargaMuestreos.Archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();
                using var stream = System.IO.File.Create(filePath);
                await cargaMuestreos.Archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);
            ExcelService.Mappings = CargaEmergencia.ColumnasPropiedades;

            var registros = ExcelService.Import<CargaMuestreoEmergenciaDto>(fileInfo, "EMERGENCIAS");
            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CargaMuestreosEmergenciaCommand { Muestreos = registros, Anio = cargaMuestreos.Anio, Reemplazar = cargaMuestreos.Reemplazar }));
        }

        [HttpGet("ExportarExcelEmergenciasPrevias")]
        public IActionResult Get([FromHeader] List<string> emergencias)
        {
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("EmergenciasPreviamenteCargadas");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(emergencias, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("ExportarExcel")]
        public IActionResult Post(List<MuestreoDto> muestreos)
        {
            List<MuestreoExcel> muestreosExcel = new();

            muestreos.ForEach(muestreo =>
                muestreosExcel.Add(new MuestreoExcel
                {
                    OCDL = muestreo.OCDL,
                    ClaveSitio = muestreo.ClaveSitio,
                    ClaveMonitoreo = muestreo.ClaveMonitoreo,
                    Estado = muestreo.Estado,
                    TipoCuerpoAgua = muestreo.TipoCuerpoAgua,
                    Laboratorio = muestreo.Laboratorio,
                    FechaRealizacion = muestreo.FechaRealizacion,
                    NumeroEntrega = muestreo.NumeroEntrega,
                    FechaLimiteRevision = muestreo.FechaLimiteRevision,
                    Estatus = muestreo.Estatus,
                    TipoCargaResultados = muestreo.TipoCargaResultados,
                }
            ));

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("LiberacionMonitoreos");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool esLiberacion, int page, int pageSize)
        {
            return Ok(await Mediator.Send(new GetMuestreos { EsLiberacion = esLiberacion, Page = page, PageSize = pageSize }));
        }

        [HttpPut]
        public async Task<IActionResult> Put(List<MuestreoRevisionDto> request)
        {
            return Ok(await Mediator.Send(new EnvioRevisionMuestreosCommand { Muestreos = request }));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(List<int> muestreos)
        {
            return Ok(await Mediator.Send(new EliminarMuestreoCommand { Muestreos = muestreos }));
        }

        [HttpGet("ResumenResultadosPorMuestreo")]
        public async Task<IActionResult> Get([FromQuery] List<int> muestreos)
        {
            return Ok(await Mediator.Send(new GetResumenResultadosByMuestreo { Muestreos = muestreos }));
        }

        [HttpGet("Aprobados")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetMuestreosAprobados()));
        }

        [HttpGet("AniosConRegistro")]
        public async Task<IActionResult> AniosConRegistro()
        {
            return Ok(await Mediator.Send(new GetAniosConOperacion()));
        }
        [HttpGet("NumerosEntrega")]
        public async Task<IActionResult> NumerosEntrega()
        {
            return Ok(await Mediator.Send(new GetNumeroEntrega()));
        }

        [HttpGet("ProgramaAnios")]
        public async Task<IActionResult> ProgramaAnios()
        {
            var datos = await _progrepor.ObtenerTodosElementosAsync();
            return Ok(datos.Select(x => Convert.ToInt32(x.Anio)).ToList());
        }

        [HttpGet("CambioEstatus")]
        public async Task<IActionResult> CambioEstatus(int estatus, long muestreoId)
        {
            return Ok(await Mediator.Send(new PutMuestreoEstatus { estatus = estatus, muestreoId = muestreoId }));
        }

        [HttpPut("cambioEstatusMuestreos")]
        public async Task<IActionResult> CambioEstatusMuestreos(ActualizarEstatusListMuestreos datos)
        {
            return Ok(await Mediator.Send(new ActualizarEstatusListMuestreos { estatusId = datos.estatusId, muestreos = datos.muestreos }));
        }


        [HttpGet("obtenerPuntosPorMuestreo")]
        public async Task<IActionResult> obtenerPuntosPorMuestreo(string claveMuestreo)
        {
            return Ok(await Mediator.Send(new GetPuntosMuestreo { claveMuestreo = claveMuestreo }));
        }

        [HttpPost("ExportarCargaResultadosEbaseca")]
        public IActionResult CargaResultadosEbaseca(List<MuestreoDto> muestreos)
        {
            List<CargaResultadosEbaseca> muestreosExcel = new();

            muestreos.ForEach(muestreo =>
                muestreosExcel.Add(new CargaResultadosEbaseca
                {
                    Estatus = muestreo.Estatus,
                    EvidenciasCompletas = (muestreo.Evidencias.Count > 0) ? "SI" : "NO",
                    NumeroCarga = muestreo.NumeroEntrega,
                    ClaveNOSEC = muestreo.ClaveSitio,
                    Clave5K = string.Empty,
                    ClaveMonitoreo = muestreo.ClaveMonitoreo,
                    TipoSitio = muestreo.TipoSitio,
                    NombreSitio = muestreo.NombreSitio,
                    OCDL = muestreo.OCDL,
                    TipoCuerpoAgua = muestreo.TipoCuerpoAgua,
                    SubtipoCuerpoAgua = muestreo.SubTipoCuerpoAgua,
                    ProgramaAnual = muestreo.ProgramaAnual,
                    Laboratorio = muestreo.Laboratorio,
                    LaboratorioSubrogado = muestreo.LaboratorioSubrogado,
                    FechaProgramacion = muestreo.FechaProgramada,
                    FechaRealizacion = muestreo.FechaRealizacion,
                    HoraInicioMuestreo = muestreo.HoraInicio,
                    HoraFinMuestreo = muestreo.HoraFin,
                    FechaCargaSica = muestreo.FechaCarga,
                    FechaEntrega = muestreo.FechaEntregaMuestreo

                }
            ));

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("CargaResultadosEbaseca");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }


        [HttpPost("ExportarMuestreosAdministracion")]
        public IActionResult ExportarMuestreosAdministracion(List<MuestreoDto> muestreos)
        {
            List<MuestreosAdministracionExcel> muestreosExcel = new();

            muestreos.ForEach(muestreo =>
                muestreosExcel.Add(new MuestreosAdministracionExcel
                {
                    Estatus = muestreo.Estatus,
                    NumeroEntrega = muestreo.NumeroEntrega,
                    ClaveSitio = muestreo.ClaveSitio,
                    Clave5K = string.Empty,
                    ClaveMonitoreo = muestreo.ClaveMonitoreo,
                    NombreSitio = muestreo.NombreSitio,
                    Ocdl = muestreo.OCDL,
                    TipoCuerpoAgua = muestreo.TipoCuerpoAgua,
                    ProgramaAnual = muestreo.ProgramaAnual,
                    Laboratorio = muestreo.Laboratorio,
                    LaboratorioSubrogado = muestreo.LaboratorioSubrogado,
                    FechaProgramada = muestreo.FechaProgramada,
                    FechaRealizacion = muestreo.FechaRealizacion,

                }
            ));

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("MuestreosAdministracion");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("ExportarResultadosAdministracion")]
        public IActionResult ExportarResultadosAdministracion(List<AcumuladosResultadoDto> muestreos)
        {
            List<ResultadosAdministracionExcel> muestreosExcel = new();

            muestreos.ForEach(resultado =>
                muestreosExcel.Add(new ResultadosAdministracionExcel
                {
                    ClaveUnica = resultado.claveUnica,
                    ClaveMonitoreo = resultado.ClaveMonitoreo,
                    ClaveSitio = resultado.ClaveSitio,
                    NombreSitio = resultado.NombreSitio,
                    TipoSitio = resultado.TipoSitio,
                    TipoCuerpoAgua = resultado.TipoCuerpoAgua,
                    SubTipoCuerpoAgua = resultado.SubTipoCuerpoAgua,
                    FechaRealizacion = resultado.FechaRealizacion,
                    GrupoParametro = resultado.grupoParametro,
                    Parametro = resultado.parametro,
                    UnidadMedida = resultado.unidadMedida ?? string.Empty,
                    Resultado = resultado.resultado,
                    NuevoResultadoReplica = resultado.nuevoResultadoReplica,
                    Replica = resultado.replica.ToString(),
                    CambioResultado = resultado.cambioResultado.ToString()


                }
            ));

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ResultadosAdministracion");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }


        [HttpGet("obtenerTotalesAdministracion")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> obtenerTotalesAdministracion()
        {

            return Ok(await Mediator.Send(new GetTotalesMuestreosAdministracionQuery()));

        }
    }
}