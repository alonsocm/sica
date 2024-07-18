using Application.Features.Catalogos.CuerpoDeAgua.Queries;
using Application.Features.Catalogos.TiposCuerpoAgua.Queries.AllTiposCuerpoAgua;
using Application.Features.Catalogos.TiposCuerpoAgua.Queries.IdTiposCuerpoAgua;
using Application.Features.Sitios.Commands.UpdateSitioCommand;
using Application.Features.Sitios.Queries.GetSitioById;
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
            return Ok(await Mediator.Send(new GetTipoCuerpoAguaQueryId { Id = id })); ;
        }
    }
}
