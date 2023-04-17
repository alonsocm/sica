using Application.Wrappers;
using AutoMapper;
using MediatR;
using Application.DTOs;
using Domain.Entities;
using Application.Specifications;
using Application.Interfaces.IRepositories;
using Application.Features.Perfiles.Queries;

namespace Application.Features.ObservacionesOCDL.Queries
{
    public class GetObservacionesQuery :IRequest<Response<List<ObservacionesDto>>>
    {
    }

    public class GetObservacionesHandler : IRequestHandler<GetObservacionesQuery, Response<List<ObservacionesDto>>>
    {
        private readonly IRepositoryAsync<Observaciones> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetObservacionesHandler(IRepositoryAsync<Observaciones> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<ObservacionesDto>>> Handle(GetObservacionesQuery request, CancellationToken cancellationToken)
        {
            var observaciones = await _repositoryAsync.ListAsync(cancellationToken);
           
            var observacionesDto = _mapper.Map<List<ObservacionesDto>>(observaciones);
            return new Response<List<ObservacionesDto>>(observacionesDto);
        }
    }
}
