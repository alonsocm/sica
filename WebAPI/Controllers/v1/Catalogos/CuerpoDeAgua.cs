using Application.Features.Catalogos.CuerpoDeAgua.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class CuerpoDeAgua : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetTipoCuerpoAguaQuery()));
        }

        [HttpGet("TipoHomologado")]
        public async Task<IActionResult> GetTipoHomologado()
        {
            return Ok(await Mediator.Send(new GetTipoHomologadoQuery()));
        }
    }
}
