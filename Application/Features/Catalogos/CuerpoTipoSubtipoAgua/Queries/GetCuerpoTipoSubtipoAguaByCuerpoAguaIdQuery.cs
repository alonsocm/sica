using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.Features.Catalogos.CuencaDireccionesLocales.Queries;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalogos.CuerpoTipoSubtipoAgua.Queries
{
    public class GetCuerpoTipoSubtipoAguaByCuerpoAguaIdQuery: IRequest<IEnumerable<CuerpoTipoSubtipoAguaDto>>
    {
        public long CuerpoAguaId { get; set; }
    }

    public class GetCuerpoTipoSubtipoAguaByCuerpoAguaIdHandler : IRequestHandler<GetCuerpoTipoSubtipoAguaByCuerpoAguaIdQuery, IEnumerable<CuerpoTipoSubtipoAguaDto>>
    {
        private readonly IRepositoryAsync<Domain.Entities.CuerpoTipoSubtipoAgua> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetCuerpoTipoSubtipoAguaByCuerpoAguaIdHandler(IRepositoryAsync<Domain.Entities.CuerpoTipoSubtipoAgua> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CuerpoTipoSubtipoAguaDto>> Handle(GetCuerpoTipoSubtipoAguaByCuerpoAguaIdQuery request, CancellationToken cancellationToken)
        {
            var cuerposTiposSubtipos = await _repositoryAsync.ListAsync(new CuerpoTipoSubtipoAguaSpecification(), cancellationToken);
            return _mapper.Map<IEnumerable<CuerpoTipoSubtipoAguaDto>>(cuerposTiposSubtipos.Where(x => x.CuerpoAguaId.Equals(request.CuerpoAguaId)));

        }
    }
}
