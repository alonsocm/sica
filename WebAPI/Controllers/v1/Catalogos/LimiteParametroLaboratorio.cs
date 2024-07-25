using Application.DTOs;
using Application.Features.Catalogos.LimiteParametroLaboratorio.Queries;
using Application.Features.Sitios.Queries.GetAllSitios;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class LimiteParametroLaboratorio: BaseApiController
    {
        //GET: api/<controller> Obtiene  los limitesLaboratios mediante paginación
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, int pageSize, string? filter = "", string? order = "")
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

            return Ok(await Mediator.Send(new GetAllLimiteParametrosLaboratorioPaginadosQuery
            {

                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));
        }
    }
}
