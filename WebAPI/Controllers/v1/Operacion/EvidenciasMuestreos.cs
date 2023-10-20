using Application.DTOs;
using Application.Features.CargaMasivaEvidencias.Commands;
using Application.Features.Evidencias.Queries;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    public class EvidenciasMuestreos : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EvidenciasMuestreos(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<IFormFile> archivos)
        {
            if (!archivos.Any())
            {
                return BadRequest("No se encontraron archivos para procesar.");
            }

            return Ok(await Mediator.Send(new CargaEvidenciasCommand { Archivos=archivos }));
        }

        [HttpGet]
        public async Task<ActionResult> Get(string nombreArchivo)
        {
            if (nombreArchivo is null || string.IsNullOrEmpty(nombreArchivo))
            {
                return BadRequest("Debe especificar un nombre de archivo para descargar");
            }

            var archivo = await Mediator.Send(new GetEvidenciaByNombre { NombreArchivo = nombreArchivo });

            if (archivo is null)
            {
                throw new ApplicationException("No se encontró el archivo de la evidencia solicitada");
            }

            return File(archivo.Data.Archivo, "application/octet-stream", archivo.Data.NombreArchivo);
        }

        [HttpGet("Archivos")]
        public async Task<ActionResult> Get([FromQuery] List<int> muestreos)
        {
            if (!muestreos.Any())
            {
                return BadRequest("Debe especificar al menos un muestreo");
            }

            var archivos = await Mediator.Send(new GetEvidenciasByMuestreo { MuestreosId = muestreos });

            if (archivos is null)
            {
                throw new ApplicationException("No se encontró el archivo de la evidencia solicitada");
            }

            var archivoZip = ZipService.GenerarZip(archivos.Data);

            return File(archivoZip, "application/octet-stream", "evidencias.zip");
        }

        #region Consulta Evidencias

        [HttpPost("ExportarExcelConsultaEvidencia")]
        public IActionResult Post(List<ConsultaEvidenciaDto> resultados)
        {
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("Evidencias");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(resultados, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        #endregion

        [HttpGet("InformacionEvidencias")]
        public async Task<ActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetInformacionEvidencias { }));
        }
    }
}
