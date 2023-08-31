using Application.DTOs;
using Application.Features.Localidades.Queries;
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

namespace Application.Features.Catalogos.Laboratorios.Queries
{
    public class GetLaboratoriosQuery: IRequest<Response<List<LaboratoriosDto>>>
    {
    }
    public class GetLaboratoriosQueryHandler : IRequestHandler<GetLaboratoriosQuery, Response<List<LaboratoriosDto>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Laboratorios> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetLaboratoriosQueryHandler(IRepositoryAsync<Domain.Entities.Laboratorios> repositoryAsync, IMapper mapper)
        {
            this._repositoryAsync = repositoryAsync;
            this._mapper = mapper;
        }
        public async Task<Response<List<LaboratoriosDto>>> Handle(GetLaboratoriosQuery request, CancellationToken cancellationToken)
        {
            var laboratorios = await _repositoryAsync.ListAsync();
            var laboratoriosDto = _mapper.Map<List<LaboratoriosDto>>(laboratorios);
            return new Response<List<LaboratoriosDto>>(laboratoriosDto);
        }
    }
}
