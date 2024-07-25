using Application.DTOs;
using Application.DTOs.Catalogos;
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

namespace Application.Features.Catalogos.LimiteParametroLaboratorio.Queries
{
    public class GetAllLimiteParametrosLaboratorioPaginadosQuery: IRequest<PagedResponse<IEnumerable<LimitesParametroLaboratorioDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }

    public class GetAllLimiteParametrosLaboratorioPaginadosHandler : IRequestHandler<GetAllLimiteParametrosLaboratorioPaginadosQuery, PagedResponse<IEnumerable<LimitesParametroLaboratorioDto>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.LimiteParametroLaboratorio> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllLimiteParametrosLaboratorioPaginadosHandler(IRepositoryAsync<Domain.Entities.LimiteParametroLaboratorio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<LimitesParametroLaboratorioDto>>> Handle(GetAllLimiteParametrosLaboratorioPaginadosQuery request, CancellationToken cancellationToken)
        {
            var limitesLaboratorio = await _repositoryAsync.ListAsync(new LimiteParametroLaboratorioSpecification(), cancellationToken);
            var limitesLaboratorioDto = _mapper.Map<IEnumerable<LimitesParametroLaboratorioDto>>(limitesLaboratorio);
            return PagedResponse<LimitesParametroLaboratorioDto>.CreatePagedReponse(limitesLaboratorioDto, request.Page, request.PageSize);
        }
    }
}
