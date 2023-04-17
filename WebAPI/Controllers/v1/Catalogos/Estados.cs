using Application.Features.Estados.Queries.GetAllEstados;
using Application.Features.Estados.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class Estados : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllEstadosQuery()));
        }

    }
}
