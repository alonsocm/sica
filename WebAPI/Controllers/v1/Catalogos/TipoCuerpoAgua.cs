using Application.Features.Catalogos.CuerpoDeAgua.Queries;
using Application.Features.Catalogos.TipoCuerpoDeAgua.Queries;
using Application.Features.Catalogos.TiposCuerpoAgua.Commands.UpdateTipoCuerpoAguaCommand;
using Application.Features.Sitios.Commands.UpdateSitioCommand;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class TipoCuerpoAgua : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetTipoCuerpoAguaQuery()));
        }



        [HttpPut]
        public async Task<ActionResult> Put(int id, UpdateTipoCuerpoAguaCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }
    }
}
