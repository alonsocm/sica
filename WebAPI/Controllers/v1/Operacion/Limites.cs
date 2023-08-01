using Application.DTOs;
using Application.Enums;
using Application.Features.Operacion.SustitucionLimites.Commands;
using Application.Features.Operacion.SustitucionLimites.Queries;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    public class Limites : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Limites(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ParametrosSustitucionLimitesDto parametrosSustitucionLimites)
        {
            if (parametrosSustitucionLimites.OrigenLimites == (int)TipoSustitucionLimites.TablaTemporal)
            {
                if (parametrosSustitucionLimites.Archivo?.Length > 0)
                {
                    string filePath = Path.GetTempFileName();
                    using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await parametrosSustitucionLimites.Archivo.CopyToAsync(fs);
                    }

                    FileInfo fileInfo = new(filePath);
                    ExcelService.Mappings = ExcelLimitesComunes.keyValues;
                    var registros = ExcelService.Import<LimiteMaximoComunDto>(fileInfo, "Límites 2012-2022");
                    System.IO.File.Delete(filePath);
                    parametrosSustitucionLimites.LimitesComunes = registros;
                }
                else
                {
                    throw new Exception("El archivo está vacio");
                }

            }

            await Mediator.Send(new SustitucionMaximoComunCommand { ParametrosSustitucion = parametrosSustitucionLimites });

            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new ResultadosSustituidosQuery
            {

            }));
        }

        [HttpGet("ExportarExcel")]
        public async Task<IActionResult> Get(int? anio)
        {
            var registros = await Mediator.Send(new ResultadosSustituidosQuery { });

            if (registros.Data.Count > 0)
            {
                var plantilla = new Plantilla(_configuration, _env);
                string templatePath = plantilla.ObtenerRutaPlantilla("ResultadosSustituidos");
                var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

                ExcelService.ExportListToExcel(registros.Data, fileInfo.FullName);
                var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
                return File(bytes, contentType, Path.GetFileName(temporalFilePath));
            }

            return Ok();
        }
    }
}
