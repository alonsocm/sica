using Application.Features.Catalogos.GrupoParametro.Queries;
using Application.Features.Catalogos.ParametrosGrupo.Commands;
using Application.Features.Catalogos.ParametrosGrupo.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ParametrosGrupo : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new ParametrosQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateParametro parametro)
        {
            return Ok(await Mediator.Send(new CreateParametro
            {
                Clave = parametro.Descripcion,
                Descripcion = parametro.Descripcion,
                GrupoId = parametro.GrupoId,
                SubgrupoId = parametro.SubgrupoId,
                ParametroPadreId = parametro.ParametroPadreId,
                UnidadMedidaId = parametro.UnidadMedidaId,
            }));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateParametro parametro)
        {
            return Ok(await Mediator.Send(new UpdateParametro
            {
                ParametroId = parametro.ParametroId,
                Clave = parametro.Descripcion,
                Descripcion = parametro.Descripcion,
                GrupoId = parametro.GrupoId,
                SubgrupoId = parametro.SubgrupoId,
                ParametroPadreId = parametro.ParametroPadreId,
                UnidadMedidaId = parametro.UnidadMedidaId,
            }));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int parametroId)
        {
            return Ok(await Mediator.Send(new DeleteParametro { ParametroId = parametroId }));
        }

        [HttpGet("GetGruposParametros")]
        public async Task<IActionResult> GetGruposParametros()
        {
            return Ok(await Mediator.Send(new GetGrupoParametro()));
        }

    }
}
