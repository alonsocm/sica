using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.Features.Catalogos.CuencaDireccionesLocales.Queries;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalogos.Acuiferos.Queries
{
    public class GetAllAcuiferosQuery: IRequest<IEnumerable<AcuiferoDto>>
    {
    }

    public class GetAllAcuiferoHandler : IRequestHandler<GetAllAcuiferosQuery, IEnumerable<AcuiferoDto>>
    {
        private readonly IRepositoryAsync<Acuifero> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllAcuiferoHandler(IRepositoryAsync<Acuifero> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AcuiferoDto>> Handle(GetAllAcuiferosQuery request, CancellationToken cancellationToken)
        {
            var acuiferos = await _repositoryAsync.ListAsync(new AcuiferoSpecification(), cancellationToken);
            return _mapper.Map<IEnumerable<AcuiferoDto>>(acuiferos);

        }
    }
}
