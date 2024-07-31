using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Catalogos.TiposCuerpoAgua.Queries
{
    public class GetTipoHomologadoQ : IRequest<Response<List<TipoHomologadoDto>>>
    {
    }
    public class TipoHomologadoQHandler : IRequestHandler<GetTipoHomologadoQ, Response<List<TipoHomologadoDto>>>
    {
        private IRepositoryAsync<TipoHomologado> _repository;
        private IMapper _mapper;

        public TipoHomologadoQHandler(IRepositoryAsync<TipoHomologado> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<TipoHomologadoDto>>> Handle(GetTipoHomologadoQ request, CancellationToken cancellationToken)
        {
            var TipoHomolo = await _repository.ListAsync(cancellationToken);
            var tipoHomoloDto = _mapper.Map<List<TipoHomologadoDto>>(TipoHomolo);
            return new Response<List<TipoHomologadoDto>>(tipoHomoloDto);
        }
    }
}
