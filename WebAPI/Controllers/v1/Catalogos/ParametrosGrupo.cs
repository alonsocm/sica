using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.Features.Catalogos.GrupoParametro.Queries;
using Application.Features.Catalogos.ParametrosGrupo.Commands;
using Application.Features.Catalogos.ParametrosGrupo.Queries;
using Application.Features.Catalogos.UnidadMedida.Queries;
using Application.Wrappers;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
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
        public async Task<IActionResult> Get(int page, int pageSize, string? filter, string? order = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            OrderBy orderBy = null;

            if (!string.IsNullOrEmpty(order) && order.Split('_').Length == 2)
            {
                orderBy = new OrderBy
                {
                    Column = order.Split('_')[0],
                    Type = order.Split('_')[1]
                };
            }

            return Ok(await Mediator.Send(new ParametrosQuery { Page = page, PageSize = pageSize, Filter = filters, OrderBy = orderBy }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateParametro parametro)
        {
            return Ok(await Mediator.Send(new CreateParametro
            {
                Clave = parametro.Clave,
                Descripcion = parametro.Descripcion,
                GrupoId = parametro.GrupoId,
                SubgrupoId = parametro.SubgrupoId,
                ParametroPadreId = parametro.ParametroPadreId,
                UnidadMedidaId = parametro.UnidadMedidaId,
            }));
        }

        [HttpPost("CargaMasiva")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromQuery] bool actualizar, [FromForm] IFormFile archivo)
        {
            string filePath = string.Empty;

            if (archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelParametrosSettings.KeyValues;

            var registros = ExcelService.Import<ExcelParametroDTO>(fileInfo, "Hoja1");
            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CreateParametros { Parametros = registros, Actualizar = actualizar }));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateParametro parametro)
        {
            return Ok(await Mediator.Send(new UpdateParametro
            {
                Id = parametro.Id,
                Clave = parametro.Clave,
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
