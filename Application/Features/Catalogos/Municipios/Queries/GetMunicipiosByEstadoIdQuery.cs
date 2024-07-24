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

namespace Application.Features.Catalogos.Municipios.Queries
{
    public class GetMunicipiosByEstadoIdQuery: IRequest<Response<List<MunicipioDto>>>
    {
        public long EstadoId { get; set; }
    }

    public class GetMunicipiosByEstadoIdHandler : IRequestHandler<GetMunicipiosByEstadoIdQuery, Response<List<MunicipioDto>>>
    {
        private readonly IRepositoryAsync<Municipio> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetMunicipiosByEstadoIdHandler(IRepositoryAsync<Municipio> repositoryAsync, IMapper mapper)
        {
            this._repositoryAsync = repositoryAsync;
            this._mapper = mapper;
        }
        public async Task<Response<List<MunicipioDto>>> Handle(GetMunicipiosByEstadoIdQuery request, CancellationToken cancellationToken)
        {
            var municipio = await _repositoryAsync.ListAsync(new PageMunicipioSpecification(), cancellationToken);
            var dtomunicipio = _mapper.Map<List<MunicipioDto>>(municipio).ToList().Where(x => x.EstadoId.Equals(request.EstadoId));
            return new Response<List<MunicipioDto>>(dtomunicipio.ToList());
        }

    }
}
