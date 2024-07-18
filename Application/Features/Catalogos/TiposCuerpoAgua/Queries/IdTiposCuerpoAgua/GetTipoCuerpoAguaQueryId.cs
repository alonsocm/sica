using Application.DTOs;
using Application.Features.Catalogos.TiposCuerpoAgua.Queries.AllTiposCuerpoAgua;
using Application.Features.Sitios.Queries.GetSitioById;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
namespace Application.Features.Catalogos.TiposCuerpoAgua.Queries.IdTiposCuerpoAgua
{
    public class GetTipoCuerpoAguaQueryId : IRequest<Response<IEnumerable<TipoCuerpoAguaDto>>>
    {
        public long Id { get; set; }
    }
    public class GetTipoCuerpoAguaQueryIdHandler : IRequestHandler<GetTipoCuerpoAguaQueryId, Response<IEnumerable<TipoCuerpoAguaDto>>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;
        private readonly IMapper _mapper;

        public GetTipoCuerpoAguaQueryIdHandler(ITipoCuerpoAguaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<TipoCuerpoAguaDto>>> Handle(GetTipoCuerpoAguaQueryId request, CancellationToken cancellationToken)
        {
            
            var tipoCuerposAgua = _repository.ObtenerElementoConInclusiones(x=> x.Id == request.Id);

            if (tipoCuerposAgua == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }            
            var TCA = _mapper.Map<IEnumerable<TipoCuerpoAguaDto>>(tipoCuerposAgua);
            return new Response<IEnumerable<TipoCuerpoAguaDto>>(TCA);
        }
    }
}
