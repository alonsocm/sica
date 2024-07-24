using Application.Features.Catalogos.Acuiferos.Queries;
using Application.Features.Catalogos.CuencaDireccionesLocales.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class Acuiferos : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        { return Ok(await Mediator.Send(new GetAllAcuiferosQuery { })); }
    }
}
