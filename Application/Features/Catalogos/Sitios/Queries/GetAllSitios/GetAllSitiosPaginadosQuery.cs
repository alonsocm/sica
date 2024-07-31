using Application.DTOs;
using Application.DTOs.Users;
using Application.Expressions;
using Application.Features.Muestreos.Queries;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;



namespace Application.Features.Sitios.Queries.GetAllSitios
{
    public class GetAllSitiosPaginadosQuery : IRequest<PagedResponse<IEnumerable<SitioDto>>>
    {
 
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }

    public class GetAllSitiosHandler : IRequestHandler<GetAllSitiosPaginadosQuery, PagedResponse<IEnumerable<SitioDto>>>
    {
        private readonly IRepositoryAsync<Sitio> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllSitiosHandler(IRepositoryAsync<Sitio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync=repositoryAsync;
            _mapper=mapper;
        }

        public async Task<PagedResponse<IEnumerable<SitioDto>>> Handle(GetAllSitiosPaginadosQuery request, CancellationToken cancellationToken)
        {
            var sitios = await _repositoryAsync.ListAsync( new PagedSitiosSpecification(), cancellationToken);           
            var sitiosDto = _mapper.Map<IEnumerable<SitioDto>>(sitios);

            if (request.Filter.Any())
            {
                var expressions = QueryExpression<SitioDto>.GetExpressionList(request.Filter);
                List<SitioDto> lstSitio = new();

                foreach (var filter in expressions)
                {
                    if (request.Filter.Count == 2 && request.Filter[0].Conditional == "equals" && request.Filter[1].Conditional == "equals")
                    {
                        var dataFinal = sitiosDto;
                        dataFinal = dataFinal.AsQueryable().Where(filter);
                        lstSitio.AddRange(dataFinal);
                        sitiosDto = lstSitio;
                    }
                    else
                    {
                        sitiosDto = sitiosDto.AsQueryable().Where(filter);
                    }
                }
            }
            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    sitiosDto = sitiosDto.AsQueryable().OrderBy(QueryExpression<SitioDto>.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    sitiosDto = sitiosDto.AsQueryable().OrderByDescending(QueryExpression<SitioDto>.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            if (sitiosDto == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }
            return PagedResponse<SitioDto>.CreatePagedReponse(sitiosDto, request.Page, request.PageSize);
        }
    }
}
