using Application.Features.Catalogos.IntervalosPuntajeSupervision.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class IntervalosPuntajeSupervision : BaseApiController
    {
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetIntervalosPuntajeSupervisionQuery()));
        }
    }
}
