using Application.DTOs;
using Application.Features.Catalogos.Laboratorios.Commands;
using Application.Features.Catalogos.Laboratorios.Queries;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class Laboratorios : BaseApiController
    {

        [HttpGet("Laboratorios")]
        public async Task<IActionResult> GetLaboratoriosQuery([FromQuery] int page, int pageSize, string? filter = "", string? order = "")
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

            return Ok(await Mediator.Send(new GetLaboratoriosQuery
            {

                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));
        }

        [HttpGet("LaboratoriosMuestradores")]
        public async Task<IActionResult> GetLaboratoriosMuestradores()
        {
            return Ok(await Mediator.Send(new GetLaboratoriosMuestradoresQuery()));
        }

        [HttpGet("GetDistinctValuesFromColumn")]
        public async Task<IActionResult> Get(string column, string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            return Ok(await Mediator.Send(new GetDistinctValuesFromColumn { Column = column, Filters = filters }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateLaboratorio parametro)
        {
            return Ok(await Mediator.Send(new CreateLaboratorio
            {
                Descripcion = parametro.Descripcion,
                Nomenclatura = parametro.Nomenclatura,
            }));
        }
    }
}
