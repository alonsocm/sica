using Application.Features.Catalogos.GrupoParametro.Queries;
using Application.Features.Catalogos.ParametrosGrupo.Commands;
using Application.Features.Catalogos.ParametrosGrupo.Queries;
using Application.Features.Catalogos.UnidadMedida.Queries;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ParametrosGrupo : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllParametros()));
        }

        [HttpGet("ParametrosPaginados")]
        public async Task<IActionResult> Get(int page, int pageSize, string? filter)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            return Ok(await Mediator.Send(new ParametrosQuery { Page = page, PageSize = pageSize, Filter = filters }));
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
                Id = parametro.Id,
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

        [HttpGet("GetDistinctFromColumn")]
        public IActionResult GetDistinctFromColumn(string column, string? filter)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new ParametrosQuery { Filter = filters }).Result.Data;

            return Ok(new Response<object>(AuxQuery.GetDistinctValuesFromColumn(column, data)));
        }

        [HttpGet("GetGruposParametros")]
        public async Task<IActionResult> GetGruposParametros()
        {
            return Ok(await Mediator.Send(new GetGrupoParametro()));
        }

        [HttpGet("GetUnidadesMedida")]
        public async Task<IActionResult> GetUnidadesMedida()
        {
            return Ok(await Mediator.Send(new GetUnidadesMedida()));
        }

        [HttpGet("GetSubGrupoAnalitico")]
        public async Task<IActionResult> GetSubGrupoAnalitico()
        {
            return Ok(await Mediator.Send(new GetSubGrupoAnalitico()));
        }
    }
}
