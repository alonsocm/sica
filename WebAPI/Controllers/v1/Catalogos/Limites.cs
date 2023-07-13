using Application.DTOs;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;

namespace WebAPI.Controllers.v1.Catalogos
{
    [Route("api/[controller]")]
    [ApiController]
    public class Limites : BaseApiController
    {
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post(IFormFile archivo)
        {
            string filePath = string.Empty;

            if (archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();
                using var stream = System.IO.File.Create(filePath);
                await archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);
            ExcelService.Mappings = ExcelLimitesComunes.keyValues;
            var registros = ExcelService.Import<LimiteMaximoComunDto>(fileInfo, "Límites 2012-2022");
            System.IO.File.Delete(filePath);

            return Ok();
        }
    }
}
