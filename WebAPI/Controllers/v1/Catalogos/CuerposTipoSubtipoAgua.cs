using Application.Features.Catalogos.CuencaDireccionesLocales.Queries;
using Application.Features.Catalogos.CuerpoTipoSubtipoAgua.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class CuerposTipoSubtipoAgua : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(long CuerporAguaId)
        {

            return Ok(await Mediator.Send(new GetCuerpoTipoSubtipoAguaByCuerpoAguaIdQuery { CuerpoAguaId = CuerporAguaId }));
        }
    }
}
