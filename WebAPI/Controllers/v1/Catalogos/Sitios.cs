using Application.Features.Sitios.Commands.CreateSitioCommand;
using Application.Features.Sitios.Commands.DeleteSitioCommand;
using Application.Features.Sitios.Commands.UpdateSitioCommand;
using Application.Features.Sitios.Queries.GetAllSitios;
using Application.Features.Sitios.Queries.GetSitioById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class Sitios : BaseApiController
    {
        //GET: api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetSitioByIdQuery { Id = id }));
        }

        //GET: api/<controller>
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllSitiosParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllSitiosQuery { Nombre = filter.Nombre, Clave = filter.Nombre, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        //POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateSitioCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(int id, UpdateSitioCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        //Delete api/<controller>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteSitioCommand { Id = id }));
        }
    }
}
