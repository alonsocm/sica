using Application.DTOs;
using Application.Features.Municipios.Queries;
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

namespace Application.Features.Localidades.Queries
{
    public class GetAllLocalidadesQuery : IRequest<Response<List<LocalidadDto>>>
    {
    }
    public class GetAllLocalidadesHandler : IRequestHandler<GetAllLocalidadesQuery, Response<List<LocalidadDto>>>
    {
        private readonly IRepositoryAsync<Localidad> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllLocalidadesHandler(IRepositoryAsync<Localidad> repositoryAsync, IMapper mapper)
        {
            this._repositoryAsync = repositoryAsync;
            this._mapper = mapper;
        }
        public async Task<Response<List<LocalidadDto>>> Handle(GetAllLocalidadesQuery request, CancellationToken cancellationToken)
        {
            var localidad       = await _repositoryAsync.ListAsync(new PageLocalidadSpecification(), cancellationToken);
            var dtolocalidad    = _mapper.Map<List<LocalidadDto>>(localidad);
            return new Response<List<LocalidadDto>>(dtolocalidad);
        }
    }
}
