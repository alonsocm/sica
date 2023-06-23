using Application.DTOs.Users;
using Application.Features.Authenticate.Commands.AuthenticateCommand;
using Application.Features.Authenticate.Commands.RegisterCommand;
using Application.Features.Sitios.Queries.GetSitioById;
using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            usrs.Data.Find(x => x.UserName == username.ToLower());

            return usrs.Data.Any();
        }
    }
}
