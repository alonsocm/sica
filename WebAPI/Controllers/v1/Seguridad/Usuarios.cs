using Application.DTOs;
using Application.DTOs.Users;
using Application.Features.Authenticate.Commands.AuthenticateCommand;
using Application.Features.Authenticate.Commands.RegisterCommand;
using Application.Features.Muestreos.Queries;
using Application.Features.Seguridad.Usuarios.Queries;
using Application.Features.Sitios.Queries.GetSitioById;
using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.Queries;
using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Shared;
using GetDistinctValuesFromColumn = Application.Features.Seguridad.Usuarios.Queries.GetDistinctValuesFromColumn;

namespace WebAPI.Controllers.v1.Seguridad
{
    [ApiVersion("1.0")]
    [ApiController]
    public class Usuarios : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateCommand
            {
                UserName = request.UserName,
                Password = request.Password,
                IpAddress = GenerateIpAddress()
            }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            if (await IsExist(request.UserName.ToLower()))
                return BadRequest("Usuario ya existe");

            return Ok(await Mediator.Send(new RegisterCommand
            {
                Nombre = request.Nombre,
                ApellidoPaterno = request.ApellidoPaterno,
                ApellidoMaterno = request.ApellidoMaterno,
                UserName = request.UserName,
                Email = request.Email,
                PerfilId = request.PerfilId,
                Activo = request.Activo,
                CuencaId = request.CuencaId,
                DireccionLocalId = request.DireccionLocalId,
                Origin = Request.Headers["origin"]
            }));
        }

        // ACTUALIZAR 
        [HttpPut("update/{id}")]
        public async Task<ActionResult> Put(int id, UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(new UpdateUserCommand
            {
                Id = id,
                PerfilId = command.PerfilId,
                CuencaId = command.CuencaId,
                DireccionLocalId = command.DireccionLocalId,
                Activo = command.Activo
            }));
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }

        //AllUsersPaginados se cambiara para traer los usuarios paginados
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool esLiberacion, int page, int pageSize, string? filter = "", string? order = "")
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

            return Ok(await Mediator.Send(new GetMuestreosPaginados
            {
                EsLiberacion = esLiberacion,
                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));
        }






        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await Mediator.Send(new GetUserbyIdQuery { Id = id }));
        }

        private string GenerateIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress != null ? HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() : string.Empty;
            }
        }

        private async Task<bool>IsExist(string username)
        {
            var usrs = await Mediator.Send(new GetAllUsersQuery());            
            return usrs.Data.Where(x => x.UserName == username.ToLower()).ToList().Any();
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
    }
}
