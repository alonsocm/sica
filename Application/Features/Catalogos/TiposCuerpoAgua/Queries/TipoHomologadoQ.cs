using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Catalogos.TiposCuerpoAgua.Queries
{
    public class GetTipoHomologadoQ : IRequest<Response<List<TipoHomologadoDto>>>
    {
        public List<Filter> Filter { get; set; }
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
            var data = tipoHomoloDto.AsQueryable(); // Se define data como la lista de DTOs

            if (request.Filter.Any()) // se relizan los filtros 
            {
                var e = QueryExpression<TipoHomologadoDto>.GetExpressionList(request.Filter);
                foreach (var filter in e)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            return new Response<List<TipoHomologadoDto>>(data.OrderBy(o => o.Descripcion).ToList()); // Se devuelve la lista filtrada y ordenada
        }
    }
}
