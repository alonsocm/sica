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
            return PagedResponse<SitioDto>.CreatePagedReponse(sitiosDto, request.Page, request.PageSize);
        }
    }
}
