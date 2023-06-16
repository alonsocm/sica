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

namespace Application.Features.Sitios.Commands.CreateSitioCommand
{
    public class CreateSitioCommand : IRequest<Response<long>>
    {
        public string Nombre { get; set; }
        public string Clave { get; set; }
    }

    public class CreateSitioCommandHandler : IRequestHandler<CreateSitioCommand, Response<long>>
    {
        private readonly IRepositoryAsync<Sitio> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateSitioCommandHandler(IRepositoryAsync<Sitio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync=repositoryAsync;
            _mapper=mapper;
        }

        public async Task<Response<long>> Handle(CreateSitioCommand request, CancellationToken cancellationToken)
        {
            var nuevoRegistro = _mapper.Map<Sitio>(request);
            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<long>(data.Id);
        }
    }
}
