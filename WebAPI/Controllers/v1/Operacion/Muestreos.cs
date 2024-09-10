using Application.DTOs;
using Application.DTOs.Users;
using Application.Enums;
using Application.Features.Muestreos.Queries;
using Application.Features.Operacion.Muestreos.Commands.Actualizar;
using Application.Features.Operacion.Muestreos.Commands.Carga;
using Application.Features.Operacion.Muestreos.Commands.Liberacion;
using Application.Features.Operacion.Muestreos.Queries;
using Application.Features.Operacion.Resultados.Queries;
using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
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

            return Ok(await Mediator.Send(new CargaMuestreosCommand { Muestreos = registros, Validado = cargaMuestreos.Validado, Reemplazar = cargaMuestreos.Reemplazar, TipoCarga = cargaMuestreos.tipocarga }));
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
        public IActionResult ExportarExcel([FromQuery] bool esLiberacion, [FromQuery] string? filter, [FromBody] List<long> muestreos)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new GetMuestreosPaginados
            {
                EsLiberacion = esLiberacion,
                Filter = filters
            }).Result.Data;

            if (muestreos != null && muestreos.Any())
            {
                data = data.Where(x => muestreos.Contains(x.MuestreoId)).ToList();
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("LiberacionMonitoreos");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportLiberacionExcel(data, fileInfo.FullName);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool esLiberacion, int page, int pageSize, string? filter = "", string? order = "")
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

            return Ok(await Mediator.Send(new GetMuestreosPaginados
            {
                EsLiberacion = esLiberacion,
                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));
        }

        [HttpGet("GetDistinctValuesFromColumn")]
        public IActionResult Get(string column, bool esLiberacion, string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new GetMuestreosPaginados
            {
                Filter = filters,
                EsLiberacion = esLiberacion
            }).Result.Data;

            return Ok(new Response<object>(AuxQuery.GetDistinctValuesFromColumn(column, data)));
        }

        [HttpPut("SetFechaLimiteRevision")]
        public async Task<IActionResult> SetFechaLimiteRevision([FromBody] IEnumerable<long> request, [FromQuery] bool esLiberacion, [FromQuery] string fechaLimite, [FromQuery] string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            if (!request.Any())
            {
                var data = Mediator.Send(new GetMuestreosPaginados
                {
                    Filter = filters,
                    EsLiberacion = esLiberacion
                }).Result.Data;

                request = data.Select(w => w.MuestreoId);
            }

            return Ok(await Mediator.Send(new AsignarFechaLimiteCommand { Muestreos = request, FechaLimiteRevision = fechaLimite }));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] IEnumerable<long> muestreos, [FromQuery] bool esLiberacion, [FromQuery] string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            if (!muestreos.Any())
            {
                var data = Mediator.Send(new GetMuestreosPaginados
                {
                    Filter = filters,
                    EsLiberacion = esLiberacion
                }).Result.Data;

                muestreos = data.Select(w => w.MuestreoId);
            }

            return Ok(await Mediator.Send(new EliminarMuestreoCommand { Muestreos = muestreos }));
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll([FromBody] IEnumerable<long> muestreos, [FromQuery] bool esLiberacion, [FromQuery] string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            if (!muestreos.Any())
            {
                var data = Mediator.Send(new GetMuestreosPaginados
                {
                    Filter = filters,
                    EsLiberacion = esLiberacion
                }).Result.Data;

                muestreos = data.Select(w => w.MuestreoId);
            }

            return Ok(await Mediator.Send(new DeleteByFilterCommand { Muestreos = muestreos }));
        }

        [HttpGet("ResumenResultadosPorMuestreo")]
        public async Task<IActionResult> Get([FromQuery] IEnumerable<long> muestreos, bool selectAll, string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            return Ok(await Mediator.Send(new GetResumenResultadosByMuestreo { Muestreos = muestreos, SelectAll = selectAll, Filters = filters }));
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
            return Ok(await Mediator.Send(new ActualizarEstatusListMuestreos { EstatusId = datos.EstatusId, Muestreos = datos.Muestreos }));
        }

        [HttpPut("LiberarRevisionSECAIAOCDL")]
        public async Task<IActionResult> LiberarRevisionSECAIAOCDL([FromBody] IEnumerable<long> muestreos, [FromQuery] bool esLiberacion, [FromQuery] int estatusId, [FromQuery] string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            if (!muestreos.Any())
            {
                var data = Mediator.Send(new GetMuestreosPaginados
                {
                    Filter = filters,
                    EsLiberacion = esLiberacion
                }).Result.Data;

                muestreos = data.Select(w => w.MuestreoId);
            }

            return Ok(await Mediator.Send(new LiberarRevisionSECAIAOCDLCommand { EstatusId = estatusId, Muestreos = muestreos }));
        }

        [HttpPut("ActualizaEstatusMuestreos")]
        public async Task<IActionResult> CambioEstatusMuestreos(UpdateStatusMuestreosDto datos)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(datos.Filter))
            {
                filters = QueryParam.GetFilters(datos.Filter);
            }

            return Ok(await Mediator.Send(new ActualizarEstatusMuestreos { EstatusId = datos.Status, Filters = filters }));
        }

        [HttpPut("ActualizarMuestreos")]
        public async Task<IActionResult> ActualizarMuestreos([FromBody] IEnumerable<long> muestreos, [FromQuery] string? filter = "")
        {
            if (!muestreos.Any())
            {
                var filters = new List<Filter>();

                if (!string.IsNullOrEmpty(filter))
                {
                    filters = QueryParam.GetFilters(filter);
                }

                var result = await Mediator.Send(new GetResultadosporMuestreoPaginadosQuery
                {
                    EstatusId = (int)EstatusMuestreo.MóduloInicialReglas,
                    Filter = filters,
                });

                muestreos = result.Data.Select(s => s.MuestreoId);
            }

            return Ok(await Mediator.Send(new ActualizarMuestreoCommand { Muestreos = muestreos }));
        }

        [HttpGet("obtenerPuntosPorMuestreo")]
        public async Task<IActionResult> obtenerPuntosPorMuestreo(string claveMuestreo)
        {
            return Ok(await Mediator.Send(new GetPuntosMuestreo { claveMuestreo = claveMuestreo }));
        }

        [HttpPost("ExportarEbasecaExcel")]
        public IActionResult Post([FromQuery] bool esLiberacion, [FromQuery] string? filter, [FromBody] List<long> muestreos)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new GetMuestreosPaginados
            {
                EsLiberacion = esLiberacion,
                Filter = filters
            }).Result.Data;

            if (muestreos != null && muestreos.Any())
            {
                data = data.Where(x => muestreos.Contains(x.MuestreoId)).ToList();
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("CargaResultadosEbaseca");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportCargaResultadosValidadosExcel(data, fileInfo.FullName);
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
                    ClaveUnica = resultado.ClaveUnica,
                    ClaveMonitoreo = resultado.ClaveMonitoreo,
                    ClaveSitio = resultado.ClaveSitio,
                    NombreSitio = resultado.NombreSitio,
                    TipoSitio = resultado.TipoSitio,
                    TipoCuerpoAgua = resultado.TipoCuerpoAgua,
                    SubTipoCuerpoAgua = resultado.SubTipoCuerpoAgua,
                    FechaRealizacion = resultado.FechaRealizacion,
                    GrupoParametro = resultado.GrupoParametro,
                    Parametro = resultado.Parametro,
                    UnidadMedida = resultado.UnidadMedida ?? string.Empty,
                    Resultado = resultado.Resultado,
                    NuevoResultadoReplica = resultado.NuevoResultadoReplica,
                    Replica = resultado.Replica.ToString(),
                    CambioResultado = resultado.CambioResultado.ToString()


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
        public async Task<IActionResult> ObtenerTotalesAdministracion()
        {
            return Ok(await Mediator.Send(new GetTotalesMuestreosAdministracionQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarAutorizacionReglasIncompleto([FromBody] AutorizacionReglasIncompletoDTO registro)
        {
            var result = await Mediator.Send(new ActualizarAutorizacionReglasIncompleto { Registro = registro });

            return Ok(result);
        }
    }
}