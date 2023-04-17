using Application.DTOs;
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

namespace Application.Features.Sitios.Queries.GetAllSitios
{
    public class GetAllSitiosQuery : IRequest<PagedResponse<List<SitioDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
    }

    public class GetAllSitiosHandler : IRequestHandler<GetAllSitiosQuery, PagedResponse<List<SitioDto>>>
    {
        private readonly IRepositoryAsync<Sitio> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllSitiosHandler(IRepositoryAsync<Sitio> repositoryAsync, IMapper mapper)
        {
            this._repositoryAsync=repositoryAsync;
            this._mapper=mapper;
        }

        public async Task<PagedResponse<List<SitioDto>>> Handle(GetAllSitiosQuery request, CancellationToken cancellationToken)
        {
            var sitios = await _repositoryAsync.ListAsync(new PagedSitiosSpecification(request.PageSize, request.PageNumber, request.Nombre, request.Clave));
            var sitiosDto = _mapper.Map<List<SitioDto>>(sitios);

            return new PagedResponse<List<SitioDto>>(sitiosDto, request.PageNumber, request.PageSize);
        }
    }
}
