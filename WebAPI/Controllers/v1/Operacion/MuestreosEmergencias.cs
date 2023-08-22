using Application.Features.Operacion.MuestreosEmergencias.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Operacion
{
    public class MuestreosEmergencias : BaseApiController
    {
        private readonly IConfiguration _configuration;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] List<int> anios)
        {
            return Ok(await Mediator.Send(new MuestreosEmergenciasPorAnioQuery { Anios = anios }));
        }
    }
}
