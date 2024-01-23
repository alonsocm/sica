using Application.DTOs;
using Application.Features.Operacion.ValidacionEvidencias.Commands;
using Application.Interfaces.IRepositories;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;

namespace WebAPI.Controllers.v1.Operacion
{
    public class ValidacionEvidencias : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IProgramaAnioRepository _progrepor;

        public ValidacionEvidencias(IConfiguration configuration, IWebHostEnvironment env, IProgramaAnioRepository progepo)
        {
            _configuration = configuration;
            _env = env;
            _progrepor = progepo;
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




    }
}
