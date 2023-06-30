using Application.DTOs;
using Application.DTOs.Users;
using Application.Features.Muestreos.Commands.Liberacion;
using Application.Features.Muestreos.Queries;
using Application.Features.Operacion.Muestreos.Commands.Actualizar;
using Application.Features.Operacion.Muestreos.Commands.Carga;
using Application.Features.Operacion.Muestreos.Queries;
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

        public Muestreos(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration=configuration;
            _env=env;
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

            return Ok(await Mediator.Send(new CargaMuestreosCommand { Muestreos = registros, Validado = cargaMuestreos.Validado, Reemplazar=cargaMuestreos.Reemplazar }));
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
        public async Task<IActionResult> Get([FromQuery] bool esLiberacion)
        {
            return Ok(await Mediator.Send(new GetMuestreos { EsLiberacion = esLiberacion }));
        }

        [HttpPut]
        public async Task<IActionResult> Put(List<MuestreoRevisionDto> request)
        {
            return Ok(await Mediator.Send(new EnvioRevisionMuestreosCommand { Muestreos=request }));
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

        [HttpGet("CambioEstatus")]
        public async Task<IActionResult> CambioEstatus(int estatus, long muestreoId)
        {
            return Ok(await Mediator.Send(new PutMuestreoEstatus { estatus = estatus, muestreoId =  muestreoId }));
        }
    }
}
