using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Catalogos.CuerpoDeAgua.Queries
{
    public class GetTipoCuerpoAguaQuery1 : IRequest<Response<List<TipoCuerpoAguaDto>>>
    {
    }
    public class GetTipoCuerpoAguaQueryHandler : IRequestHandler<GetTipoCuerpoAguaQuery1, Response<List<TipoCuerpoAguaDto>>>
    {
        private readonly IRepositoryAsync<TipoCuerpoAgua> _repository;
        private readonly IMapper _mapper;

        public GetTipoCuerpoAguaQueryHandler(IRepositoryAsync<TipoCuerpoAgua> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<TipoCuerpoAguaDto>>> Handle(GetTipoCuerpoAguaQuery1 request, CancellationToken cancellationToken)
        {
            var tiposCuerpoAgua = await _repository.ListAsync(cancellationToken);
            var tipoCuerpoAguaDto = _mapper.Map<List<TipoCuerpoAguaDto>>(tiposCuerpoAgua);
            return new Response<List<TipoCuerpoAguaDto>>(tipoCuerpoAguaDto);
        }
    }
}
