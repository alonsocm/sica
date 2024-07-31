using Application.DTOs;
using Application.DTOs.Catalogos;
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
    public class GetCuerpoAguaQuery: IRequest<Response<List<CuerpoAguaDto>>>
    {
    }

    public class GetCuerpoAguaHandler : IRequestHandler<GetCuerpoAguaQuery, Response<List<CuerpoAguaDto>>>
    {
        private readonly IRepositoryAsync<CuerpoAgua> _repository;
        private readonly IMapper _mapper;

        public GetCuerpoAguaHandler(IRepositoryAsync<CuerpoAgua> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<CuerpoAguaDto>>> Handle(GetCuerpoAguaQuery request, CancellationToken cancellationToken)
        {
            var cuerposAgua = await _repository.ListAsync(cancellationToken);
            var cuerposAguaDto = _mapper.Map<List<CuerpoAguaDto>>(cuerposAgua);
            return new Response<List<CuerpoAguaDto>>(cuerposAguaDto);
        }
    }
}
