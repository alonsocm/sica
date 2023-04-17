using Microsoft.AspNetCore.Mvc;
using Application.Features.Paginas.Queries;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class Paginas : BaseApiController
    {
        [HttpGet("{perfil}")]
        public async Task<IActionResult> Get(string perfil)
        {
            return Ok(await Mediator.Send(new GetAllPaginasByPerfil() { Perfil=perfil }));
        }
    }
}
