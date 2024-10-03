using Application.Features.Operacion.RevisionOCDL.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Operacion.RevisionOCDL
{
    public class MonitoreoOCDL : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetMonitoreosOCDL(int userId, int page, int pageSize)
        {
            var response = await Mediator.Send(new GetMonitoreos { UserId = userId, Page = page, PageSize = pageSize });
            return Ok(response);
        }
    }
}
