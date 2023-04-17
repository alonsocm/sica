using Domain.Entities;
using Application.DTOs;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Application.Specifications;

namespace Application.Features.Usuarios.Queries
{
    public class GetAllUsersQuery : IRequest<Response<List<UserDto>>>
    {
    }

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserDto>>>
    {
        private IRepositoryAsync<Usuario> _repository;
        private IMapper _mapper;

        public GetAllUsersHandler(IRepositoryAsync<Usuario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _repository.ListAsync(new UsuariosPerfilesSpec(), cancellationToken);
            var userDto = _mapper.Map<List<UserDto>>(usuarios);
            return new Response<List<UserDto>>(userDto);
        }
    }
}
