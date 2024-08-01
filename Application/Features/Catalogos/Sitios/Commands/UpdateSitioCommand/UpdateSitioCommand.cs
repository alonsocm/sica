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
        public long Id { get; set; }
        public string ClaveSitio { get; set; }
        public string NombreSitio { get; set; }
        public long CuencaDireccionesLocalesId { get; set; }
        public long EstadoId { get; set; }
        public long MunicipioId { get; set; }
        public long CuerpoTipoSubtipoAguaId { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Observaciones { get; set; }
        public int? AcuiferoId { get; set; }
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
            var sitio = _mapper.Map<Sitio>(request);
            await _repositoryAsync.UpdateAsync(sitio);
            return new Response<long>(sitio.Id);
        }
    }
}
