using Application.DTOs;
using Application.Features.DireccionesLocales.Queries;
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

namespace Application.Features.Catalogos.CuerpoDeAgua.Queries
{
    public class GetTipoHomologadoQuery: IRequest<Response<List<TipoHomologadoDto>>>
    {
    }
    public class TipoHomologadoQueryHandler : IRequestHandler<GetTipoHomologadoQuery, Response<List<TipoHomologadoDto>>>
    {
        private IRepositoryAsync<TipoHomologado> _repository;
        private IMapper _mapper;

        public TipoHomologadoQueryHandler(IRepositoryAsync<TipoHomologado> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<TipoHomologadoDto>>> Handle(GetTipoHomologadoQuery request, CancellationToken cancellationToken)
        {
            var TipoHomolo = await _repository.ListAsync(cancellationToken);
            var tipoHomoloDto = _mapper.Map<List<TipoHomologadoDto>>(TipoHomolo);
            return new Response<List<TipoHomologadoDto>>(tipoHomoloDto);
        }
    }
}
