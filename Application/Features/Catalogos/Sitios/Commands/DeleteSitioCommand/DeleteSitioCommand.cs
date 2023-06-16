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

namespace Application.Features.Sitios.Commands.DeleteSitioCommand
{
    public class DeleteSitioCommand : IRequest<Response<long>>
    {
        public int Id { get; set; }
    }

    public class DeleteSitioCommandHandler : IRequestHandler<DeleteSitioCommand, Response<long>>
    {
        private readonly IRepositoryAsync<Sitio> _repositoryAsync;
        private readonly IMapper _mapper;

        public DeleteSitioCommandHandler(IRepositoryAsync<Sitio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync=repositoryAsync;
            _mapper=mapper;
        }

        public async Task<Response<long>> Handle(DeleteSitioCommand request, CancellationToken cancellationToken)
        {
            var sitio = await _repositoryAsync.GetByIdAsync(request.Id);

            if (sitio == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            await _repositoryAsync.DeleteAsync(sitio);

            return new Response<long>(sitio.Id);
        }
    }

}
