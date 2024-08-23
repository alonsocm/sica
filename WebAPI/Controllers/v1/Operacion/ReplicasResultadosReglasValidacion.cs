using Application.DTOs;
using Application.Features.Operacion.ReplicasResultadosReglasValidacion.Queries;
using Application.Features.Operacion.Resultados.Queries;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ReplicasResultadosReglasValidacion:BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(int estatusId, int page, int pageSize, string? filter = "", string? order = "")
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

            return Ok(await Mediator.Send(new GetReplicasResultadosReglaValByEstatus
            {
                EstatusId = estatusId,
                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));

        }
    }
}
