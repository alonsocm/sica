using Application.Features.Usuarios.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Seguridad
{
    [ApiVersion("1.0")]
    [ApiController]
    public class UsuariosAD : BaseApiController
    {
        [HttpGet("{UserName}")]
        public async Task<IActionResult> Get([FromRoute] ADUsersQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }
    }
}
