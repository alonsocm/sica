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
using Application.Features.Sitios.Queries.GetSitioById;

namespace Application.Features.Usuarios.Queries
{
    public class GetUserbyIdQuery : IRequest<Response<UserDto>>
    {
        public long Id { get; set; }
    }

    public class GetUserbyIdHandler : IRequestHandler<GetUserbyIdQuery, Response<UserDto>>
    {
        private readonly IRepositoryAsync<Usuario> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetUserbyIdHandler(IRepositoryAsync<Usuario> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> Handle(GetUserbyIdQuery request, CancellationToken cancellationToken)
        { 
            var usuario =await _repositoryAsync.ListAsync(new UsuariosPerfilesSpec(),cancellationToken);
            var usuariofil = usuario.Where(x => x.Id == request.Id).FirstOrDefault();
            if (usuariofil == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            var dto = _mapper.Map<UserDto>(usuariofil);
            return new Response<UserDto>(dto);
        }
    }
}
