using Application.Features.Catalogos.AccionLaboratorio.Queries;
using Application.Features.Catalogos.ProgramaAnios.Queries;
using Application.Features.Operacion.Muestreos.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class ProgramaAnio: BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        { return Ok(await Mediator.Send(new GetAllProgramaAnios { })); }
    }
}
