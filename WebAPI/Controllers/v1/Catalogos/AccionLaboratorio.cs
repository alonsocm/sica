using Application.Features.Catalogos.AccionLaboratorio.Queries;
using Application.Features.Catalogos.Acuiferos.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class AccionLaboratorio: BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        { return Ok(await Mediator.Send(new GetAllAccionesLaboratorioQuery { })); }
    }
}
