using Application.Features.Catalogos.TiposCuerpoAgua.Commands;
using Application.Features.Catalogos.TiposCuerpoAgua.Queries;
using Application.Features.TiposCuerpoAgua.Commands.AddTipoCuerpoAguaCommand;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class TipoCuerpoAgua : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetTipoCuerpoAguaQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await Mediator.Send(new GetTipoCuerpoAguaIdQuery { Id = id })); ;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTipoCuerpoAguaCommand { Id = id })); ;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AddTipoCuerpoAguaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, UpdateTipoCuerpoAguaCommand tipoCuerpoAgua)
        {
            if (id != tipoCuerpoAgua.Id)
            {
                return BadRequest("El Id no existe.");
            }

            return Ok(await Mediator.Send(tipoCuerpoAgua));
        }
    }
}
