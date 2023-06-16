using Domain.Entities;
using Application.DTOs;
using Application.Wrappers;
using MediatR;
using Application.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Collections;
using Application.Interfaces.IRepositories;

namespace Application.Features.DireccionesLocales.Queries
{
    public class GetDLbyOrganismoQuery :IRequest<Response<List<DireccionLocalDto>>> 
    {
        public Int64 Ip { get; set; }
    }

    public class GetDLbyOrganismoHandler : IRequestHandler<GetDLbyOrganismoQuery, Response<List<DireccionLocalDto>>>
    {
        private IRepositoryAsync<CuencaDireccionesLocales> _repository;
        private IMapper _mapper;

        public GetDLbyOrganismoHandler(IRepositoryAsync<CuencaDireccionesLocales> repository, IMapper mapper, IRepositoryAsync<DireccionLocal> repositoryLocal)
        {
            _repository = repository;
            _mapper = mapper;            
        }

        public async Task<Response<List<DireccionLocalDto>>> Handle(GetDLbyOrganismoQuery request, CancellationToken cancellationToken)
        {
            var CuencaDLocales = await _repository.ListAsync(new DLocalesByCuencaSpec(request.Ip));       
            List<DireccionLocalDto> lstdireclocalfinal = new List<DireccionLocalDto>();
            foreach (var item in CuencaDLocales)
            {
                lstdireclocalfinal.Add(_mapper.Map<DireccionLocalDto>(item.Dlocal));
            }       

            return new Response<List<DireccionLocalDto>>(lstdireclocalfinal);

        }
    }
}
