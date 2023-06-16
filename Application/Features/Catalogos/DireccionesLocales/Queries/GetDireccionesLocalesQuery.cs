using Domain.Entities;
using Application.DTOs;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.Interfaces.IRepositories;

namespace Application.Features.DireccionesLocales.Queries
{
    public class GetDireccionesLocalesQuery : IRequest<Response<List<DireccionLocalDto>>>
    {
    }

    public class GetAllDireccionLocalHandler : IRequestHandler<GetDireccionesLocalesQuery, Response<List<DireccionLocalDto>>>
    {
        private IRepositoryAsync<DireccionLocal> _repository;
        private IMapper _mapper;

        public GetAllDireccionLocalHandler(IRepositoryAsync<DireccionLocal> repository, IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<Response<List<DireccionLocalDto>>> Handle(GetDireccionesLocalesQuery request, CancellationToken cancellationToken)
        {
            var direccionesLocales = await _repository.ListAsync(cancellationToken);
            var direccionesLocalesDto = _mapper.Map<List<DireccionLocalDto>>(direccionesLocales);
            return new Response<List<DireccionLocalDto>>(direccionesLocalesDto);
        }
    }
}
