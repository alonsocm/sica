using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Estados.Queries.GetAllEstados
{
    public class GetAllEstadosQuery : IRequest<Response<List<EstadoDto>>>
    {
    }

    public class GetAllEstadosHandler : IRequestHandler<GetAllEstadosQuery, Response<List<EstadoDto>>>
    {
        private readonly IRepositoryAsync<Estado> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllEstadosHandler(IRepositoryAsync<Estado> repositoryAsync, IMapper mapper)
        {
            this._repositoryAsync = repositoryAsync;
            this._mapper = mapper;
        }
        public async Task<Response<List<EstadoDto>>> Handle(GetAllEstadosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var estado = await _repositoryAsync.ListAsync(new PageEstadoSpecification(), cancellationToken);                
                var dtoEstado = _mapper.Map<List<EstadoDto>>(estado);
                return new Response<List<EstadoDto>>(dtoEstado);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw;
            }
        }

  
    }

}


//public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserDto>>>
//{
//    private IRepositoryAsync<Usuario> _repository;
//    private IMapper _mapper;

//    public GetAllUsersHandler(IRepositoryAsync<Usuario> repository, IMapper mapper)
//    {
//        _repository = repository;
//        _mapper = mapper;
//    }

//    public async Task<Response<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
//    {
//        var usuarios = await _repository.ListAsync(new UsuariosPerfilesSpec(), cancellationToken);
//        var userDto = _mapper.Map<List<UserDto>>(usuarios);
//        return new Response<List<UserDto>>(userDto);
//    }
//}