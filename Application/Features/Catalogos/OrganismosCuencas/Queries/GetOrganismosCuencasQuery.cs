using Application.DTOs;
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

namespace Application.Features.OrganismosCuencas.Queries
{
    public record GetOrganismosCuencasQuery : IRequest<Response<List<OrganismoCuencaDto>>> {}

    public class GetOrganismosCuencasQueryHandler : IRequestHandler<GetOrganismosCuencasQuery, Response<List<OrganismoCuencaDto>>>
    {
        private IRepositoryAsync<OrganismoCuenca> _repository;
        private IMapper _mapper;

        public GetOrganismosCuencasQueryHandler(IRepositoryAsync<OrganismoCuenca> repository, IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<Response<List<OrganismoCuencaDto>>> Handle(GetOrganismosCuencasQuery request, CancellationToken cancellationToken)
        {
            var organismosCuencas = await _repository.ListAsync(cancellationToken);
            var organismosCuencasDto = _mapper.Map<List<OrganismoCuencaDto>>(organismosCuencas);
            return new Response<List<OrganismoCuencaDto>>(organismosCuencasDto);
        }
    }
}
