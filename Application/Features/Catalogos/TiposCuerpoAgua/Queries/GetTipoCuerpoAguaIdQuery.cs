using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
namespace Application.Features.Catalogos.TiposCuerpoAgua.Queries
{
    public class GetTipoCuerpoAguaIdQuery : IRequest<Response<TipoCuerpoAguaDto>>
    {
        public long Id { get; set; }

    }
    public class GetTipoCuerpoAguaQueryIdHandler : IRequestHandler<GetTipoCuerpoAguaIdQuery, Response<TipoCuerpoAguaDto>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;
        private readonly IMapper _mapper;

        public GetTipoCuerpoAguaQueryIdHandler(ITipoCuerpoAguaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<TipoCuerpoAguaDto>> Handle(GetTipoCuerpoAguaIdQuery request, CancellationToken cancellationToken)
        {
            var tipoCuerpoAgua = _repository.ObtenerElementoConInclusiones(x => x.Id == request.Id, i => i.TipoHomologado).FirstOrDefault();

            if (tipoCuerpoAgua == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            return new Response<TipoCuerpoAguaDto>(_mapper.Map<TipoCuerpoAguaDto>(tipoCuerpoAgua));
        }
    }
}
