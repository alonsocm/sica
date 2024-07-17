using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Catalogos.TipoCuerpoDeAgua.Queries
{
    public class GetTipoCuerpoAguaQuery : IRequest<Response<IEnumerable<TipoCuerpoAguaDto>>>
    {
    }
    public class GetTipoCuerpoAguaQueryHandler : IRequestHandler<GetTipoCuerpoAguaQuery, Response<IEnumerable<TipoCuerpoAguaDto>>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;
        private readonly IMapper _mapper;

        public GetTipoCuerpoAguaQueryHandler(ITipoCuerpoAguaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TipoCuerpoAguaDto>>> Handle(GetTipoCuerpoAguaQuery request, CancellationToken cancellationToken)
        {
            var tiposCuerpoAgua = _repository.ObtenerElementoConInclusiones(x => true, i => i.TipoHomologado);
            var tipoCuerpoAguaDto = _mapper.Map<IEnumerable<TipoCuerpoAguaDto>>(tiposCuerpoAgua);
            return new Response<IEnumerable<TipoCuerpoAguaDto>>(tipoCuerpoAguaDto);
        }
    }
}
