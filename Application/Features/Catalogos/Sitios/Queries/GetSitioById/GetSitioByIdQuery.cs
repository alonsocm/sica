using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Sitios.Queries.GetSitioById
{
    public class GetSitioByIdQuery : IRequest<Response<SitioDto>>
    {
        public int Id { get; set; }
    }

    public class GetClientByIdQueryHandler : IRequestHandler<GetSitioByIdQuery, Response<SitioDto>>
    {
        private readonly IRepositoryAsync<Sitio> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IRepositoryAsync<Sitio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync=repositoryAsync;
            _mapper=mapper;
        }

        public async Task<Response<SitioDto>> Handle(GetSitioByIdQuery request, CancellationToken cancellationToken)
        {
            var sitio = await _repositoryAsync.GetByIdAsync(request.Id);

            if (sitio == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            var dto = _mapper.Map<SitioDto>(sitio);
            return new Response<SitioDto>(dto);
        }
    }
}
