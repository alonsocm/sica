using Application.DTOs;
using Application.Features.Estados.Queries.GetAllEstados;
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

namespace Application.Features.Municipios.Queries
{
    public class GetAllMunicipiosQuery : IRequest<Response<List<MunicipioDto>>>
    {
    }
    public class GetAllMunicipioHandler : IRequestHandler<GetAllMunicipiosQuery, Response<List<MunicipioDto>>>
    {
        private readonly IRepositoryAsync<Municipio> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllMunicipioHandler(IRepositoryAsync<Municipio> repositoryAsync, IMapper mapper)
        {
            this._repositoryAsync = repositoryAsync;
            this._mapper = mapper;
        }
        public async Task<Response<List<MunicipioDto>>> Handle(GetAllMunicipiosQuery request, CancellationToken cancellationToken)
        {         
            var municipio       = await _repositoryAsync.ListAsync(new PageMunicipioSpecification(), cancellationToken);
            var dtomunicipio    = _mapper.Map<List<MunicipioDto>>(municipio);
            return new Response<List<MunicipioDto>>(dtomunicipio);       
        }

    }
}
