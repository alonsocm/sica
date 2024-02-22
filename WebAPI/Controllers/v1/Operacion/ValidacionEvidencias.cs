using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.Features.Operacion.Muestreos.Commands.Actualizar;
using Application.Features.Operacion.Muestreos.Commands.Liberacion;
using Application.Features.Operacion.ValidacionEvidencias.Commands;
using Application.Features.Operacion.ValidacionEvidencias.Queries;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Shared.Utilities.Services;
using System.Collections.Generic;
using WebAPI.Controllers.v1.Catalogos;
using WebAPI.Shared;


namespace WebAPI.Controllers.v1.Operacion
{
    public class ValidacionEvidencias : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IProgramaAnioRepository _progrepor;
        private readonly IMapper _mapper;
        private readonly IEmailSenderRepository _email;

        public ValidacionEvidencias(IConfiguration configuration, IWebHostEnvironment env, IProgramaAnioRepository progepo, IMapper mapper, IEmailSenderRepository email)
        {
            _configuration = configuration;
            _env = env;
            _progrepor = progepo;
            _mapper = mapper;
            _email = email;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] IFormFile archivo)
        {
            string filePath = string.Empty;

            if (archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelAvisoRealizacion.keyValues;

            var registros = ExcelService.Import<AvisoRealizacionDto>(fileInfo, "DataBank");

            System.IO.File.Delete(filePath);

            //return Ok(await Mediator.Send(true));
            return Ok(await Mediator.Send(new CargaARMCommand { Muestreos = registros }));
        }

        [HttpGet]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> ObtenerDatosGenerales() {

            return Ok(await Mediator.Send(new GetValidacionEvidenciasQuery()));

        }

        [HttpPost("validarMuestreo")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromBody] vwValidacionEvienciasDto muestreo, long usuarioId)
        {

            return Ok(await Mediator.Send(new ValidarMuestreoCommand { Muestreos = muestreo, usuarioId = usuarioId }));

        }

        [HttpPost("validarMuestreoLista")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromBody] List<vwValidacionEvienciasDto> muestreo, long usuarioId)
        {

            return Ok(await Mediator.Send(new ValidarMuestreoListaCommand { Muestreos = muestreo, usuarioId = usuarioId }));

        }

        [HttpGet("obtenerResultadosEvidencia")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> obtenerResultadosEvidencia()
        {

            return Ok(await Mediator.Send(new GetVwValidacionEvidenciaTotalesQuery()));

        }

        [HttpGet("obtenerMuestreosAprobados")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> obtenerMuestreosAprobados(bool rechazo)
        {

            return Ok(await Mediator.Send(new GetVwValidacionEvidenciaRealizadaQuery { rechazo = rechazo }));

        }

        [HttpPut("actualizarPorcentaje")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> actualizarPorcentaje(List<VwValidacionEvidenciaRealizada> muestreos)
        {
            return Ok(await Mediator.Send(new UpdatePorcentajeCommand { Muestreos = muestreos }));
        }

        [HttpPost("extraerEventualidades")]
        public IActionResult Post(List<VwValidacionEvidenciaRealizada> muestreos)
        {
            List<EventualidadesMuestreoAprobados> eventualidades = _mapper.Map<List<EventualidadesMuestreoAprobados>>(muestreos);
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("EventualidadesMuestreoAprobados");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(eventualidades, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("extraerMuestreosAprobados")]
        public IActionResult extraerMuestreosAprobados(List<VwValidacionEvidenciaRealizada> muestreos)
        {
            List<EvidenciasMuestreosAprobados> aprobados = _mapper.Map<List<EvidenciasMuestreosAprobados>>(muestreos);
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("EvidenciaMuestreosAprobados");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(aprobados, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("extraerMuestreosRechazados")]
        public IActionResult extraerMuestreosRechazados(List<VwValidacionEvidenciaRealizada> muestreos)
        {
            List<EvidenciasMuestreosRechazados> rechazados = new();

            muestreos.ForEach(muestreo =>
                rechazados.Add(new EvidenciasMuestreosRechazados
                {
                    ClaveMuestreo = muestreo.ClaveMuestreo,
                    ClaveSitio = muestreo.ClaveSitio,
                    Laboratorio = muestreo.LaboratorioMuestreo ?? string.Empty,
                    FechaValidacion = muestreo.FechaValidacion.ToString("dd/mm/yyyy"),
                }
            ));
            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("EvidenciaMuestreosRechazados");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(rechazados, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpPost("envioCorreo")]
        public IActionResult extraerMuestenvioCorreoreosRechazados(string destinatario,string asunto, string body, List<string> attachmentPaths)
        {
            _email.SendEmail(destinatario,asunto,body,attachmentPaths);
            //new EmailSender().SendEmail(destinatario,asunto,body,attachmentPaths);
            // EmailSender.SendEmail(destinatario, asunto, body, attachmentPaths);
            return (Ok(true));
        }
    }
}
