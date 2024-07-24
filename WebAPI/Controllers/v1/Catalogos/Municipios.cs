using Application.Features.Catalogos.Municipios.Queries;
using Application.Features.Municipios.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class Municipios : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllMunicipiosQuery()));
        }

        [HttpGet("MunicipiosByEstadoId")]
        public async Task<IActionResult> MunicipiosByEstadoId(long EstadoId)
        {
            return Ok(await Mediator.Send(new GetMunicipiosByEstadoIdQuery { EstadoId= EstadoId }));
        }
    }
}
