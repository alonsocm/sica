using Application.Features.Catalogos.IntervalosClasificacion.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class IntervalosClasificacion : BaseApiController
    {
        [HttpGet("IntervalosClasificacion")]
        public async Task<IActionResult> GetIntervalosClasificacionQuery()
        {
            return Ok(await Mediator.Send(new GetIntervalosClasificacionQuery()));
        }
    }
}
