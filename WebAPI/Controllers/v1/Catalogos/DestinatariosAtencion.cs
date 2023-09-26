using Application.Features.Catalogos.DestinatariosAtencion.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class DestinatariosAtencion : BaseApiController
    {
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetDestinatariosAtencionQuery()));
        }
    }
}
