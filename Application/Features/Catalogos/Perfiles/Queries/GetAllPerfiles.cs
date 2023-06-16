using Application.DTOs;
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

namespace Application.Features.Perfiles.Queries
{
    public class GetAllPerfiles : IRequest<Response<List<PerfilDto>>>
    {

    }

    public class GetAllPerfilesHandler : IRequestHandler<GetAllPerfiles, Response<List<PerfilDto>>>
    {
        private readonly IRepositoryAsync<Perfil> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllPerfilesHandler(IRepositoryAsync<Perfil> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync=repositoryAsync;
            _mapper=mapper;
        }

        public async Task<Response<List<PerfilDto>>> Handle(GetAllPerfiles request, CancellationToken cancellationToken)
        {
            var perfiles = await _repositoryAsync.ListAsync(cancellationToken);
            var perfilesActivos = perfiles.Where(x => x.Estatus == true);

            //var perfilesDto = _mapper.Map<List<PerfilDto>>(perfiles);
            var perfilesDto = _mapper.Map<List<PerfilDto>>(perfilesActivos);
            return new Response<List<PerfilDto>>(perfilesDto);
        }
    }
}
