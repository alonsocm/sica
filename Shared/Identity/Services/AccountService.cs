using Application.DTOs.Users;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Shared.Identity.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Entities;
using Application.Specifications;
using Shared.Utilities.Services;
using System.Linq;
using Application.Interfaces.IRepositories;

namespace Shared.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly IRepositoryAsync<Usuario> _repositoryAsync;
        private readonly IActiveDirectoryService _activeDirectoryService;

        public AccountService(IRepositoryAsync<Usuario> repositoryAsync, IOptions<JWTSettings> jwtSettings, IDateTimeService dateTimeService, IActiveDirectoryService activeDirectoryService)
        {
            _repositoryAsync = repositoryAsync;
            _jwtSettings=jwtSettings.Value;
            _activeDirectoryService=activeDirectoryService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var usuario = await _repositoryAsync.FirstOrDefaultAsync(new UsuarioByUserNameSpec(request.UserName));

            if (usuario == null)
            {
                throw new ApiException($"No hay una cuenta registrada con el nombre de usuario: {request.UserName}");
            }

            var valid = await _activeDirectoryService.IsUserValid(request.UserName, request.Password);

            if (!valid)
                throw new ApiException($"Las credenciales del usuario no son válidas");

            JwtSecurityToken jwtSecurityToken = await GenerateJWTToken(usuario);
            AuthenticationResponse response = new()
            {
                Id = usuario.Id.ToString(),
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = usuario.UserName,
            };

            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"Usuario autenticado {usuario.UserName}");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var usuarioConElMismoUserName = await _repositoryAsync.FirstOrDefaultAsync(new UsuarioByUserNameSpec(request.UserName));

            if (usuarioConElMismoUserName != null)
            {
                throw new ApiException($"El nombre de usuario {request.UserName} ya fue registrado previamente");
            }
            
            var usuario = new Usuario
            {
                UserName = request.UserName,
                Nombre = request.Nombre,
                ApellidoPaterno = request.ApellidoPaterno,
                ApellidoMaterno = request.ApellidoMaterno,
                Email = request.Email,
                PerfilId = request.PerfilId,
                Activo = request.Activo,
                DireccionLocalId = request.DireccionLocalId,
                CuencaId = request.CuencaId,
                FechaRegistro = new DateTimeService().NowUtc
            };

            await _repositoryAsync.AddAsync(usuario);

            return new Response<string>(usuario.Id.ToString(), message: $"Usuario registrado exitosamente: {request.UserName}");
        }

        private Task<JwtSecurityToken> GenerateJWTToken(Usuario usuario)
        {
            var perfiles = usuario.Perfil.Nombre;

            var roleClaims = new List<Claim>
            {
                new Claim("perfil", perfiles)
            };

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", usuario.Id.ToString()),
                new Claim("ip", ipAddress),
            }
            //.Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return Task.FromResult(jwtSecurityToken);
        }

        private static RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedBy = ipAddress,
            };
        }

        private static string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-","");
        }
    }
}
