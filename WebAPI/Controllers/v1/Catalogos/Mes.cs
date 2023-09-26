using Application.Features.Catalogos.Mes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class Mes : BaseApiController
    {
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetMesQuery()));
        }
    }
}
