using Application.DTOs;
using Application.Features.Operacion.SustitucionLimites.Commands;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;

namespace WebAPI.Controllers.v1.Operacion
{
    public class Limites : BaseApiController
    {
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ParametrosSustitucionLimitesDto parametrosSustitucionLimites)
        {
            var sustítucionCorrecta = await Mediator.Send(new SustitucionMaximoComunCommand { ParametrosSustitucion = parametrosSustitucionLimites });

            string filePath = string.Empty;

            if (parametrosSustitucionLimites.Archivo?.Length > 0)
            {
                filePath = Path.GetTempFileName();
                using var stream = System.IO.File.Create(filePath);
                await parametrosSustitucionLimites.Archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);
            ExcelService.Mappings = ExcelLimitesComunes.keyValues;
            var registros = ExcelService.Import<LimiteMaximoComunDto>(fileInfo, "Límites 2012-2022");
            System.IO.File.Delete(filePath);


            return Ok();
        }
    }
}
