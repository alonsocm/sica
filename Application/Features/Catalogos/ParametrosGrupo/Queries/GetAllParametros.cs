using Application.DTOs;
using Application.Features.Catalogos.ParametrosGrupo;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalogos.ParametrosGrupo.Queries
{
    public class GetAllParametros : IRequest<Response<List<ParametrosDto>>>
    {
    }

    public class GetParametrosHandler : IRequestHandler<GetAllParametros, Response<List<ParametrosDto>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ParametrosGrupo> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetParametrosHandler(IRepositoryAsync<Domain.Entities.ParametrosGrupo> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<ParametrosDto>>> Handle(GetAllParametros request, CancellationToken cancellationToken)
        {
            var parametros = await _repositoryAsync.ListAsync(cancellationToken);
        

            var parametrosDto = _mapper.Map<List<ParametrosDto>>(parametros.OrderBy(x => x.Orden));
            return new Response<List<ParametrosDto>>(parametrosDto);
        }
    }

}
