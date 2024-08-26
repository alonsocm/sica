using Application.DTOs;
using Application.Exceptions;
using Application.Features.Operacion.Muestreos.Commands.Liberacion;
using Application.Features.Operacion.Replicas.Commandas;
using Application.Features.Operacion.Replicas.Commands;
using Application.Features.Operacion.Replicas.Commands.ReplicasTotal;
using Application.Features.Operacion.Replicas.Queries;
using Application.Features.Operacion.Replicas.Queries.ReplicasTotal;
using Application.Features.Replicas.Queries;
using Application.Models;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Shared.Utilities.Services;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]

    public class Replicas : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Replicas(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        #region Revisión Resultado

        [HttpGet("RevisionRepliFiltro")]
        public async Task<IActionResult> Get(int userId)
        {
            return Ok(await Mediator.Send(new GetReplicaRevicionResultados
            {
                UserId = userId
            }));
        }

        [HttpPost("ExportarExcelReplica")]
        public async Task<IActionResult> ExportarExcelReplica(List<AprobacionResultadoMuestreoDto> muestreos)
        {
            List<GeneralDescarga> muestreosExcel = new();
            int indice = 0;
            foreach (var datomuestreo in muestreos)
            {
                GeneralDescarga datoresumen = new()
                {
                    No_Entrega = datomuestreo.NoEntrega,
                    Clave_Unica = datomuestreo.ClaveUnica,
                    Clave_Sitio = datomuestreo.ClaveSitio,
                    Clave_Monitoreo = datomuestreo.ClaveMonitoreo,
                    Nombre_Sitio = datomuestreo.NombreSitio,
                    Clave_Parametro = datomuestreo.ClaveParametro,
                    Laboratorio = datomuestreo.Laboratorio,
                    Tipo_Cuerpo_Agua = datomuestreo.TipoCuerpoAgua,
                    Tipo_Cuerpo_Agua_Original = datomuestreo.TipoCuerpoAguaOriginal,
                    Resultado = datomuestreo.Resultado,
                    Es_Correcto_OCDL = datomuestreo.EsCorrectoOCDL??string.Empty,
                    Observacion_OCDL = datomuestreo.ObservacionOCDL,
                    Es_Correcto_SECAIA = datomuestreo.EsCorrectoSECAIA,
                    Observacion_SECAIA = datomuestreo.ObservacionSECAIA,
                    Clasificacion_Observacion = datomuestreo.ClasificacionObservacion,
                    Aprueba_Resultado = (datomuestreo.ApruebaResultado == null) ? "NO" : datomuestreo.ApruebaResultado,
                    Comentarios_Aprobacion_Resultados = datomuestreo.ComentariosAprobacionResultados,
                    Usuario = datomuestreo.UsuarioRevision,
                    estatusResultado = datomuestreo.estatusResultado
                };
                muestreosExcel.Add(datoresumen);
                indice++;
            }
            var rootPath = _env.WebRootPath;
            if (rootPath == null)
            {
                throw new ApiException("No se encontró la carpeta root");
            }

            var plantillaPath = _configuration["PlantillasExcel:RevisionResultadoGeneral"];
            if (plantillaPath == null)
            {
                throw new ApiException("No se encontró la ruta de la plantilla requerida");
            }

            var filePath = Path.Combine(rootPath, plantillaPath);
            if (!System.IO.File.Exists(filePath))
            {
                throw new ApiException("No se encontró el archivo de la plantilla requerida");
            }

            var tempFilePath = Path.GetTempFileName();
            System.IO.File.Copy(filePath, tempFilePath, true);
            FileInfo fileInfo = new(tempFilePath);
            ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);


            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(tempFilePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(tempFilePath);
            System.IO.File.Delete(tempFilePath);

            return File(bytes, contentType, Path.GetFileName(tempFilePath));
        }

        [HttpPost("DescargarEstatusReplica")]
        public async Task<IActionResult> DescargarEstatusReplica(List<AprobacionResultadoMuestreoDto> muestreos)
        {
            List<ResultadosconEstatus> muestreosExcel = new();
            int indice = 0;
            foreach (var datomuestreo in muestreos)
            {
                ResultadosconEstatus datoresumen = new ResultadosconEstatus();
                if (datomuestreo.ApruebaResultado == "")
                {
                    datoresumen.No_Entrega = datomuestreo.NoEntrega;
                    datoresumen.Clave_Unica = datomuestreo.ClaveUnica;
                    datoresumen.Clave_Sitio = datomuestreo.ClaveSitio;
                    datoresumen.Clave_Monitoreo = datomuestreo.ClaveMonitoreo;
                    datoresumen.Nombre_Sitio = datomuestreo.NombreSitio;
                    datoresumen.Clave_Parametro = datomuestreo.ClaveParametro;
                    datoresumen.Laboratorio = datomuestreo.Laboratorio;
                    datoresumen.Tipo_Cuerpo_Agua = datomuestreo.TipoCuerpoAgua;
                    datoresumen.Tipo_Cuerpo_Agua_Original = datomuestreo.TipoCuerpoAguaOriginal;
                    datoresumen.Resultado = datomuestreo.Resultado;
                    datoresumen.Es_Correcto_OCDL = datomuestreo.EsCorrectoOCDL??string.Empty;
                    datoresumen.Observacion_OCDL = datomuestreo.ObservacionOCDL;
                    datoresumen.Es_Correcto_SECAIA = datomuestreo.EsCorrectoSECAIA;
                    datoresumen.Observacion_SECAIA = datomuestreo.ObservacionSECAIA;
                    datoresumen.Clasificacion_Observacion = datomuestreo.ClasificacionObservacion;
                    datoresumen.Aprueba_Resultado = datomuestreo.ApruebaResultado;
                    datoresumen.Comentarios_Aprobacion_Resultados = datomuestreo.ComentariosAprobacionResultados;
                    muestreosExcel.Add(datoresumen);
                    indice++;
                }
            }
            var rootPath = _env.WebRootPath;
            if (rootPath == null)
            {
                throw new ApiException("No se encontró la carpeta root");
            }
            var plantillaPath = _configuration["PlantillasExcel:RevisionResultado"];
            if (plantillaPath == null)
            {
                throw new ApiException("No se encontró la ruta de la plantilla requerida");
            }
            var filePath = Path.Combine(rootPath, plantillaPath);
            if (!System.IO.File.Exists(filePath))
            {
                throw new ApiException("No se encontró el archivo de la plantilla requerida");
            }
            var tempFilePath = Path.GetTempFileName();
            System.IO.File.Copy(filePath, tempFilePath, true);
            FileInfo fileInfo = new(tempFilePath);
            //ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);
            ExcelService.ExportToExcelRevicion(muestreosExcel, fileInfo, true);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(tempFilePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(tempFilePath);
            System.IO.File.Delete(tempFilePath);

            return File(bytes, contentType, Path.GetFileName(tempFilePath));
        }

        [HttpPut("AutorizarRechaMuestreo")]
        public async Task<IActionResult> AutorizarRechaMuestreo(List<AprobacionResultadoMuestreoDto> muestreos)
        {
            foreach (var muestreo in muestreos)
            {
                await Mediator.Send(new updateAutorRechaAprobacionCommand
                {
                    MuestreoId = muestreo.MuestreoId,
                    UserId = (long)(muestreo.UsuarioRevisionId != null ? muestreo.UsuarioRevisionId : 0),
                    ResultadoMuestreoId = muestreo.ResultadoMuestreoId,
                    ApruebaResultado = muestreo.ApruebaResultado == "SI"
                });
            }
            return Ok();
        }

        [HttpPut("EnviarAprobacion")]
        public async Task<IActionResult> EnviarAprovacion(List<AprobacionResultadoMuestreoDto> muestreos)
        {
            foreach (var item in muestreos)
            {
                await Mediator.Send(new EnviarAprobacionResultadosCommand
                {
                    ResultadoMuestreoId = item.ResultadoMuestreoId,
                    EstatusId = item.EstatusResultadoId
                });
            }
            return Ok();
        }

        [HttpPost("CargaRevision")]
        public async Task<IActionResult> CargaRevision([FromForm] CargaRevisionModel formData)
        {

            IFormFile formFile = formData.formFile;
            long usuarioId = long.Parse(formData.usuarioId);

            string filePath = string.Empty;

            if (formFile.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await formFile.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelResultadosRevisionReplicaSettings.keyValues;

            var archivos = ExcelService.Import<ResultadosconEstatus>(fileInfo, "ebaseca");
            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CargaRevisionResultadoCommand
            {
                Revision = archivos,
                UsuairioId = usuarioId
            }));
        }

        #endregion

        #region Replicas total
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllReplicaResultado { }));
        }

        [HttpPost("ReplicasExcel")]
        public IActionResult Post(List<ReplicasExcel> replicas)
        {
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ResultadosParaReplica");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(replicas, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("CargarRevisionReplicas")]
        public async Task<IActionResult> Post(IFormFile archivoRevisionReplicas)
        {
            string filePath = string.Empty;

            if (archivoRevisionReplicas.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await archivoRevisionReplicas.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelSettingsRevisionReplicas.KeysValues;

            var registros = ExcelService.Import<RevisionReplicasDto>(fileInfo, "ebaseca");

            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CargarRevisionReplicas { Replicas = registros }));
        }

        [HttpPost("RevisionReplicasLNRExcel")]
        public async Task<IActionResult> Post(List<RevisionReplicasLNRExcel> replicas)
        {
            var estatusActualizado = await Mediator.Send(new DescargaRevisionLNR() { Replicas = replicas });

            if (estatusActualizado.Data)
            {
                var plantilla = new Plantilla(_configuration, _env);
                string templatePath = plantilla.ObtenerRutaPlantilla("RevisionLNR");
                var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

                ExcelService.ExportToExcel(replicas, fileInfo, true);
                var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

                return File(bytes, contentType, Path.GetFileName(temporalFilePath));
            }

            throw new ApiException();
        }

        [HttpPost("CargarRevisionLNR")]
        public async Task<IActionResult> Post([FromForm] RevisionLNRForm revisionLNRForm)
        {
            string filePath = string.Empty;

            if (revisionLNRForm.Archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await revisionLNRForm.Archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelSettingsRevisionReplicasLNR.KeysValues;

            var registros = ExcelService.Import<RevisionLNRDto>(fileInfo, "ebaseca");

            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CargarRevisionLNR { Replicas = registros }));
        }

        [HttpPut("AprobarResultadosBloque")]
        public async Task<IActionResult> Put(AprobarResultadosBloque aprobarResultadosBloque)
        {
            return Ok(await Mediator.Send(new AprobarResultadosBloque { Aprobado = aprobarResultadosBloque.Aprobado, ClavesUnicas = aprobarResultadosBloque.ClavesUnicas, UsuarioId = aprobarResultadosBloque.UsuarioId }));
        }

        [HttpPut("EnviarResultados")]
        public async Task<IActionResult> Put(EnviarResultados resultadosEnviar)
        {
            return Ok(await Mediator.Send(new EnviarResultados { Aprobado = resultadosEnviar.Aprobado, ClavesUnicas = resultadosEnviar.ClavesUnicas, UsuarioId = resultadosEnviar.UsuarioId }));
        }

        [HttpPut("EnviarResultadosDifDato")]
        public async Task<IActionResult> Put(EnviarResultadoDifDato resultadosEnviar)
        {
            return Ok(await Mediator.Send(new EnviarResultadoDifDato { ClavesUnicas = resultadosEnviar.ClavesUnicas, UsuarioId = resultadosEnviar.UsuarioId }));
        }

        [HttpPost("ExportarExcel")]
        public async Task<IActionResult> Exportar(List<RevisionReplicasLNRExcel> replicas)
        {
            var estatusActualizado = await Mediator.Send(new DescargaRevisionLNR() { Replicas = replicas });

            if (estatusActualizado.Data)
            {
                var plantilla = new Plantilla(_configuration, _env);
                string templatePath = plantilla.ObtenerRutaPlantilla("ReplicasGeneral");
                var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

                ExcelService.ExportToExcel(replicas, fileInfo, true);
                var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

                return File(bytes, contentType, Path.GetFileName(temporalFilePath));
            }

            throw new ApiException();
        }

        [HttpPost("CargarEvidenciasReplica")]
        public async Task<IActionResult> Post(List<IFormFile> archivos)
        {
            if (!archivos.Any())
            {
                return BadRequest("No se encontraron archivos para procesar.");
            }

            return Ok(await Mediator.Send(new CargarEvidenciasReplica { Archivos=archivos }));
        }

        [HttpGet("ObtenerEvidencias")]
        public async Task<ActionResult> Get([FromQuery] List<string> clavesUnicas)
        {
            if (!clavesUnicas.Any())
            {
                return BadRequest("Debe especificar un nombre de archivo para descargar");
            }

            var archivos = await Mediator.Send(new ObtenerEvidenciasReplica { ClavesUnicas=clavesUnicas });

            if (!archivos.Data.Any())
            {
                return BadRequest("No se encontraron evidencias para las claves únicas seleccionadas");
            }

            var archivoZip = ZipService.GenerarZip(archivos.Data);

            return File(archivoZip, "application/octet-stream", "evidencias.zip");
        }

        #endregion

        #region Resumen
        [HttpGet("Resumen")]
        public async Task<IActionResult> GetResumen()
        {
            return Ok(await Mediator.Send(new ReplicasResumen { }));
        }

        [HttpPost("ExportarExcelReplicaResumen")]
        public async Task<IActionResult> ExportarExcelReplicaResumen(List<GeneralDescargaResumenExcel> resultados)
        {
            List<GeneralDescargaResumenExcel> resultadosExcel = new();

            foreach (var resultado in resultados)
            {
                GeneralDescargaResumenExcel resultadoExcel = new();
                resultadoExcel.NoEntrega = resultado.NoEntrega;
                resultadoExcel.ClaveUnica = resultado.ClaveUnica;
                resultadoExcel.ClaveSitio = resultado.ClaveSitio;
                resultadoExcel.ClaveMonitoreo = resultado.ClaveMonitoreo;
                resultadoExcel.NombreSitio = resultado.NombreSitio;
                resultadoExcel.ClaveParametro = resultado.ClaveParametro;
                resultadoExcel.Laboratorio = resultado.Laboratorio;
                resultadoExcel.TipoCuerpoAgua = resultado.TipoCuerpoAgua;
                resultadoExcel.TipoCuerpoAguaOriginal = resultado.TipoCuerpoAguaOriginal;
                resultadoExcel.Resultado = resultado.Resultado;
                resultadoExcel.estatusResultado = resultado.estatusResultado;
                resultadosExcel.Add(resultadoExcel);
            }

            var filePath = Path.GetTempFileName();
            FileInfo fileInfo = new(filePath);
            ExcelService.ExportToExcel(resultadosExcel, fileInfo);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            System.IO.File.Delete(filePath);

            return File(bytes, contentType, Path.GetFileName(filePath));
        }
        #endregion
    }
}
