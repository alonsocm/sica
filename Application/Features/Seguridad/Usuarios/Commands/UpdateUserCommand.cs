using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Commands
{
    public class UpdateUserCommand : IRequest<Response<int>>
    {
        public long Id { get; set; }         
        public int PerfilId { get; set; }
        public long? CuencaId { get; set; }
        public long? DireccionLocalId { get; set; }
        public bool Activo { get; set; }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateUserCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Usuario> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(IRepositoryAsync<Usuario> _repository, IMapper mapper)
        {
            _repositoryAsync = _repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _repositoryAsync.GetByIdAsync(request.Id);

            if (usuario == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            usuario.PerfilId = request.PerfilId;
            usuario.CuencaId = request.CuencaId;
            usuario.DireccionLocalId = request.DireccionLocalId;
            usuario.Activo = request.Activo;
            
            await _repositoryAsync.UpdateAsync(usuario);
            return new Response<int>((int)usuario.Id);
        }
    }
}
