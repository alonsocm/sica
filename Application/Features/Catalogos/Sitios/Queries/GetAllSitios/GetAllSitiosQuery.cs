using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Sitios.Queries.GetAllSitios
{
    public class GetAllSitiosQuery : IRequest<PagedResponse<IEnumerable<SitioDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Nombre { get; set; }
        public string? Clave { get; set; }
    }

    public class GetAllSitiosHandler : IRequestHandler<GetAllSitiosQuery, PagedResponse<IEnumerable<SitioDto>>>
    {
        private readonly IRepositoryAsync<Sitio> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllSitiosHandler(IRepositoryAsync<Sitio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync=repositoryAsync;
            _mapper=mapper;
        }

        public async Task<PagedResponse<IEnumerable<SitioDto>>> Handle(GetAllSitiosQuery request, CancellationToken cancellationToken)
        {
            var sitios = await _repositoryAsync.ListAsync(new PagedSitiosSpecification(request.Nombre, request.Clave), cancellationToken);
            var sitiosDto = _mapper.Map<IEnumerable<SitioDto>>(sitios);

            var pagedResponse = PagedResponse<SitioDto>.CreatePagedReponse(sitiosDto, request.Page, request.PageSize);
            return pagedResponse;
        }
    }
}
