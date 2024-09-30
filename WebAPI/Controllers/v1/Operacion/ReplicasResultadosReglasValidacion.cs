using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.DTOs.Users;
using Application.Features.CargaMasivaEvidencias.Commands;
using Application.Features.Catalogos.Sitios.Commands;
using Application.Features.Evidencias.Queries;
using Application.Features.Muestreos.Queries;
using Application.Features.Operacion.Muestreos.Commands.Carga;
using Application.Features.Operacion.ReplicasResultadosReglasValidacion.Commands;
using Application.Features.Operacion.ReplicasResultadosReglasValidacion.Queries;
using Application.Features.Operacion.Resultados.Queries;
using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Shared.Utilities.Services;
using System.Collections.Generic;
using System.Drawing.Printing;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ReplicasResultadosReglasValidacion : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailSenderRepository _email;
        private readonly IResultado _resultados;
        public ReplicasResultadosReglasValidacion(IMapper mapper, IConfiguration configuration, IWebHostEnvironment env, IEmailSenderRepository email, IResultado resultados)
        {
            _mapper = mapper;
            _configuration = configuration;
            _env = env;
            _email = email;
            _resultados = resultados;
        }

        [HttpPost("GetResultadosReplicas")]
        public async Task<IActionResult> Get(List<int> estatusId, int page, int pageSize, string? filter = "", string? order = "")
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

            return Ok(await Mediator.Send(new GetReplicasResultadosReglaValByEstatus
            {
                EstatusId = estatusId,
                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));

        }

        [HttpPost]
        public IActionResult descargaReplicasResultados(List<ReplicasResultadosReglasValidacionDto> resultados)
        {
            List<ReplicasResultadosRegValidacionExcel> aprobados = _mapper.Map<List<ReplicasResultadosRegValidacionExcel>>(resultados);
            aprobados.ForEach(resumen =>
            {
                resumen.AceptaRechazo = (resumen.AceptaRechazo.IsNullOrEmpty()) ? "" : ((resumen.AceptaRechazo == "True") ? "SI" : "NO");
                resumen.CorrectoResultadoReglaValidacion = (resumen.CorrectoResultadoReglaValidacion.IsNullOrEmpty()) ? "" :((resumen.CorrectoResultadoReglaValidacion == "True") ? "SI" : "NO");
                resumen.MismoResultado = (resumen.MismoResultado.IsNullOrEmpty()) ? "" : ((resumen.MismoResultado == "True") ? "SI" : "NO");
                resumen.EsDatoCorrectoSrenameca = (resumen.EsDatoCorrectoSrenameca.IsNullOrEmpty()) ? "" : ((resumen.EsDatoCorrectoSrenameca == "True") ? "SI" : "NO");
                resumen.ApruebaResultadoReplica = (resumen.ApruebaResultadoReplica.IsNullOrEmpty()) ? "" : ((resumen.ApruebaResultadoReplica == "True") ? "SI" : "NO");
            });

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ReplicasResultadosValidacionExcel");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(aprobados, fileInfo, true);

            string tempFilePath = Path.Combine(Path.GetTempPath(), "nombrearchivo.xlsx");
            System.IO.File.Copy(temporalFilePath, tempFilePath, true);
            FileInfo fileinfo = new(tempFilePath);

            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("GetDistinctValuesFromColumn")]
        public IActionResult Post(string column, List<int> estatusId, string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new GetReplicasResultadosReglaValByEstatus
            { Filter = filters, EstatusId = estatusId}).Result.Data;

            return Ok(new Response<object>(AuxQuery.GetDistinctValuesFromColumn(column, data)));
        }

        [HttpPost("SendEmailCreateFile")]
        public IActionResult SendEmailCreateFile(List<ReplicasResultadosReglasValidacionDto> resultados, int tipoArchivo,string destinatario,
        string asunto, string body, string cc)
        {            
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla((tipoArchivo == (int)Application.Enums.TipoReplicaReglaValidacion.ReplicaLaboratorioExterno) ? "ReplicasLaboratorioExterno" : "ReplicasResultadosSrenameca");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);         

            if ((int)Application.Enums.TipoReplicaReglaValidacion.ReplicaLaboratorioExterno == tipoArchivo)
                ExcelService.ExportToExcel(_mapper.Map<List<ReplicasResultadoLabExterno>>(resultados), fileInfo, true);
            else { ExcelService.ExportToExcel(_mapper.Map<List<ReplicasResultadoSrenameca>>(resultados), fileInfo, true); }

            string tempFilePath = Path.Combine(Path.GetTempPath(), (tipoArchivo == (int)Application.Enums.TipoReplicaReglaValidacion.ReplicaLaboratorioExterno) ? "ReplicasLaboratorioExterno.xlsx" : "ReplicasSrenameca.xlsx");
            System.IO.File.Copy(temporalFilePath, tempFilePath, true);
            FileInfo fileinfo = new(tempFilePath);
            
            List<string> rutas = new List<string>();
            rutas.Add(tempFilePath);
            _email.SendEmail(destinatario, asunto, body, rutas, cc);

            var idResultados = resultados.Select(x => x.ResultadoMuestreoId).ToList();
            var resultadosMuestreo = _resultados.ObtenerElementosPorCriterio(x => idResultados.Contains(x.Id));          

            resultadosMuestreo.ToList().ForEach(resultado =>
            {
                resultado.EstatusResultadoId = (tipoArchivo == (int)Application.Enums.TipoReplicaReglaValidacion.ReplicaLaboratorioExterno) ? 
                (int)Application.Enums.EstatusResultado.EnvíoLaboratorioExterno : (int)Application.Enums.EstatusResultado.EnvíoaSRENAMECA; 
                _resultados.Actualizar(resultado);
            });          
         
            return (Ok(true));
        }

        [HttpPost("uploadfileReplicas")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> uploadfileReplicas([FromForm] IFormFile archivo, int tipoArchivo, int? usuarioIdValido)
        {
            string filePath = string.Empty;

            if (archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            switch (tipoArchivo)
            {

                case (int)Application.Enums.TipoReplicaReglaValidacion.ReplicaLaboratorioExterno:
                    ExcelService.Mappings = ReplicasReglasValidacionLaboratorioSettings.KeyValues;
                    var registros = ExcelService.Import<ReplicasResultadoLabExterno>(fileInfo, "Hoja1");
                    System.IO.File.Delete(filePath);
                    Ok(await Mediator.Send(new CargaReplicasCommand { Replicas = registros }));
                    break;

                    case (int)Application.Enums.TipoReplicaReglaValidacion.ReplicaSrenameca:
                         ExcelService.Mappings = ReplicasReglasValidacionSRENAMECASettings.KeyValues;
                    var datos = ExcelService.Import<ReplicasResultadoSrenameca>(fileInfo, "Hoja1");
                    System.IO.File.Delete(filePath);
                    Ok(await Mediator.Send(new CargaSRENAMECACommand { Replicas = datos }));
                    break;

                    case (int)Application.Enums.TipoReplicaReglaValidacion.RepilcasAprobacion:
                    ExcelService.Mappings = ReplicasReglasValidacionAprobacionSettings.KeyValues;
                    var aprobacion = ExcelService.Import<ReplicasResultadosAprobacion>(fileInfo, "Hoja1");
                    System.IO.File.Delete(filePath);
                    Ok(await Mediator.Send(new CargaAprobacionCommand { Replicas = aprobacion, usuarioIdValido = usuarioIdValido }));

                    break;
                default:
                    break; 
            }
            return Ok(true);
        }

        [HttpPost("uploadfileEvidencias")]
        public async Task<IActionResult> Post(List<IFormFile> archivos)
        {
            if (!archivos.Any())
            {
                return BadRequest("No se encontraron archivos para procesar.");
            }

            return Ok(await Mediator.Send(new CargaEvidenciaCommand { Archivos = archivos }));
        }

        [HttpGet("downloadfileEvidencias")]
        public async Task<ActionResult> Get([FromQuery] List<int> replicas)
        {
            if (!replicas.Any())
            {
                return BadRequest("Debe especificar al menos un muestreo");
            }

            var archivos = await Mediator.Send(new GetEvidenciasByReplica { lstReplicasResultadosId = replicas });

            if (archivos is null)
            {
                throw new ApplicationException("No se encontró el archivo de la evidencia solicitada");
            }

            var archivoZip = ZipService.GenerarZip(archivos.Data);

            return File(archivoZip, "application/octet-stream", "evidencias.zip");
        }

        [HttpPost("enviarResumenResultados")]

        public async Task<ActionResult> enviarResumenResultados([FromQuery] List<int> replicas)
        {
            

            var archivos = await Mediator.Send(new GetEvidenciasByReplica { lstReplicasResultadosId = replicas });

            if (archivos is null)
            {
                throw new ApplicationException("No se encontró el archivo de la evidencia solicitada");
            }

            var archivoZip = ZipService.GenerarZip(archivos.Data);

            return File(archivoZip, "application/octet-stream", "evidencias.zip");
        }


    }
}
