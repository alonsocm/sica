using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Queries
{
    public class ADUsersQuery : IRequest<Response<List<UserDto>>>
    {
        public string UserName { get; set; }
    }

    public class GetADUsersHandler : IRequestHandler<ADUsersQuery, Response<List<UserDto>>>
    {
        private readonly IActiveDirectoryService _activeDirectoryService;

        public GetADUsersHandler(IActiveDirectoryService activeDirectoryService)
        {
            _activeDirectoryService=activeDirectoryService;
        }

        public async Task<Response<List<UserDto>>> Handle(ADUsersQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _activeDirectoryService.GetUsers(request.UserName);

            return new Response<List<UserDto>>(usuarios);
        }
    }
}
