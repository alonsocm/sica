using Application.DTOs;
using Application.Features.Operacion.Muestreos.Commands.Liberacion;
using Application.Features.Operacion.ReplicaDiferente.Querys.ReplicaDiferente;
using Application.Features.ReplicaDiferente.Commands;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Shared.Utilities.Services;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ReplicasDiferente : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ReplicasDiferente(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        #region Replica Diferente

        [HttpGet("ReplicaDiferenteliFiltro")]
        public async Task<IActionResult> Get(int userId)
        {
            return Ok(await Mediator.Send(new GetReplicaDiferente
            {

            }));
        }

        [HttpPost("ExportarExcelReplicaDiferente")]
        public IActionResult ExportarExcelReplicaDiferente(List<GeneralDescargaDiferenteDto> muestreos)
        {
            List<GeneralDescargaDiferenteExcel> muestreosExcel = new();

            foreach (var datoreplica in muestreos)
            {
                GeneralDescargaDiferenteExcel datoreplicadif = new();
                datoreplicadif.No_Entrega = datoreplica.NoEntrega;
                datoreplicadif.Clave_Unica = datoreplica.ClaveUnica;
                datoreplicadif.Clave_Sitio = datoreplica.ClaveSitio;
                datoreplicadif.Clave_Monitoreo = datoreplica.ClaveMonitoreo;
                datoreplicadif.Nombre_Sitio = datoreplica.NombreSitio;
                datoreplicadif.Clave_Parametro = datoreplica.ClaveParametro;
                datoreplicadif.Laboratorio = datoreplica.Laboratorio;
                datoreplicadif.Tipo_Cuerpo_Agua = datoreplica.TipoCuerpoAgua;
                datoreplicadif.Tipo_Cuerpo_Agua_Original = datoreplica.TipoCuerpoAguaOriginal;
                datoreplicadif.Resultado_Actualizado_por_Replica = datoreplica.ResultadoActualizadoporReplica;
                datoreplicadif.Es_Correcto_OCDL = datoreplica.EsCorrectoOCDL;
                datoreplicadif.Observacion_OCDL = datoreplica.ObservacionOCDL;
                datoreplicadif.Es_Correcto_SECAIA = datoreplica.EsCorrectoSECAIA;
                datoreplicadif.Observacion_SECAIA = datoreplica.ObservacionSECAIA;
                datoreplicadif.Clasificacion_Observacion = datoreplica.ClasificacionObservacion;
                datoreplicadif.ObservacionSRENAMECA = datoreplica.ObservacionSRENAMECA;
                datoreplicadif.Comentarios_Aprobacion_Resultados = datoreplica.ComentariosAprobacionResultados;
                muestreosExcel.Add(datoreplicadif);
            }

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ReplicaRevisionLNR");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("CargarArchivoRepDiferente")]
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

            ExcelService.Mappings = ExcelSettingsReplicaDiferente.keyValues;

            var registros = ExcelService.Import<GeneralDescargaDiferente>(fileInfo, "ebaseca");

            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CargaReplicaDiferenteCommand { Resultados = registros }));
        }


        [HttpPost("ExportarExcelReplicaDiferenteGeneral")]
        public async Task<IActionResult> ExportarExcelReplicaDiferenteGeneral(List<GeneralDescargaDiferenteDto> muestreos)
        {
            List<GeneralDescargaDiferenteDto> muestreosExcel = new();
            int indice = 0;

            foreach (var datoreplica in muestreos)
            {
                GeneralDescargaDiferenteDto datoreplicadif = new GeneralDescargaDiferenteDto();
                datoreplicadif.NoEntrega = datoreplica.NoEntrega;
                datoreplicadif.ClaveUnica = datoreplica.ClaveUnica;
                datoreplicadif.ClaveSitio = datoreplica.ClaveSitio;
                datoreplicadif.ClaveMonitoreo = datoreplica.ClaveMonitoreo;
                datoreplicadif.NombreSitio = datoreplica.NombreSitio;
                datoreplicadif.ClaveParametro = datoreplica.ClaveParametro;
                datoreplicadif.Laboratorio = datoreplica.Laboratorio;
                datoreplicadif.TipoCuerpoAgua = datoreplica.TipoCuerpoAgua;
                datoreplicadif.TipoCuerpoAguaOriginal = datoreplica.TipoCuerpoAguaOriginal;
                datoreplicadif.ResultadoActualizadoporReplica = datoreplica.ResultadoActualizadoporReplica;
                datoreplicadif.EsCorrectoOCDL = datoreplica.EsCorrectoOCDL;
                datoreplicadif.ObservacionOCDL = datoreplica.ObservacionOCDL;
                datoreplicadif.EsCorrectoSECAIA = datoreplica.EsCorrectoSECAIA;
                datoreplicadif.ObservacionSECAIA = datoreplica.ObservacionSECAIA;
                datoreplicadif.ClasificacionObservacion = datoreplica.ClasificacionObservacion;
                datoreplicadif.ObservacionSRENAMECA = datoreplica.ObservacionSRENAMECA;
                datoreplicadif.ComentariosAprobacionResultados = datoreplica.ComentariosAprobacionResultados;
                datoreplicadif.FechaObservacionSRENAMECA = datoreplica.FechaObservacionSRENAMECA;
                datoreplicadif.SeApruebaResultadodespuesdelaReplica = datoreplica.SeApruebaResultadodespuesdelaReplica;
                datoreplicadif.UsuarioRevision = datoreplica.UsuarioRevision;
                datoreplicadif.Estatus = datoreplica.Estatus;
                muestreosExcel.Add(datoreplicadif);
                indice++;
            }

            var filePath = Path.GetTempFileName();
            FileInfo fileInfo = new(filePath);
            ExcelService.ExportToExcel(muestreosExcel, fileInfo);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            System.IO.File.Delete(filePath);

            return File(bytes, contentType, Path.GetFileName(filePath));
        }

        [HttpPut("Aprobacionporbloque")]
        public async Task<IActionResult> Aprobacionporbloque(List<ReplicaDiferenteObtenerDto> resultados)
        {
            foreach (var item in resultados)
            {
                await Mediator.Send(new CambiarEstatusReplicaDiferenteCommand
                {
                    ResultadosMuestreo = item.ResultadoMuestreoId,
                    IdUsuario = item.UsuarioRevisionId
                });
            }

            return Ok();
        }

        [HttpPut("EnvioAprobacion")]
        public async Task<IActionResult> EnvioAprobacion(List<ReplicaDiferenteObtenerDto> resultados)
        {
            foreach (var item in resultados)
            {
                await Mediator.Send(new EnviarAprobacionResultadosCommand
                {
                    ResultadoMuestreoId = item.ResultadoMuestreoId,
                    EstatusId = item.EstatusResultadoId
                });
            }
            return Ok();
        }

        #endregion
    }
}



