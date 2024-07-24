using Application.DTOs;
using Application.Features.Sitios.Queries.GetAllSitios;
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

namespace Application.Features.Catalogos.CuencaDireccionesLocales.Queries
{
    public class GetAllCuencaDireccionLocalQuery: IRequest<IEnumerable<CuencaDireccionesLocalesDto>>
    {
    }

    public class GetAllCuencaDireccionHandler : IRequestHandler<GetAllCuencaDireccionLocalQuery, IEnumerable<CuencaDireccionesLocalesDto>>
    {
        private readonly IRepositoryAsync<Domain.Entities.CuencaDireccionesLocales> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllCuencaDireccionHandler(IRepositoryAsync<Domain.Entities.CuencaDireccionesLocales> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CuencaDireccionesLocalesDto>> Handle(GetAllCuencaDireccionLocalQuery request, CancellationToken cancellationToken)
        {
            var cuencasDirecciones = await _repositoryAsync.ListAsync(new CuencaDireccionLocalSpecification(), cancellationToken);
            return _mapper.Map<IEnumerable<CuencaDireccionesLocalesDto>>(cuencasDirecciones);
           
        }
    }


}
