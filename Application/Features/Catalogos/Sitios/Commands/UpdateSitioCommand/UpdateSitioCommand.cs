using Application.DTOs;
using Application.Exceptions;
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

namespace Application.Features.Sitios.Commands.UpdateSitioCommand
{
    public class UpdateSitioCommand : IRequest<Response<long>>
    {
        public SitioDto Sitio { get; set; }
    }

    public class UpdateSitioCommandHandler : IRequestHandler<UpdateSitioCommand, Response<long>>
    {
        private readonly IRepositoryAsync<Sitio> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateSitioCommandHandler(IRepositoryAsync<Sitio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync=repositoryAsync;
            _mapper=mapper;
        }

        public async Task<Response<long>> Handle(UpdateSitioCommand request, CancellationToken cancellationToken)
        {
            //var sitio = await _repositoryAsync.GetByIdAsync(request.Id);

            //if (sitio == null)
            //{
            //    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            //}

            var sitio = _mapper.Map<Sitio>(request.Sitio);

            await _repositoryAsync.UpdateAsync(sitio);

            return new Response<long>(sitio.Id);
        }
    }
}
