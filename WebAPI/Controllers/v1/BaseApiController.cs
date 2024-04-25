using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected const int Page = 1;
        protected const int PageSize = 30;
    }
}
