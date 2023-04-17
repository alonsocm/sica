using Application.DTOs.Users;
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

namespace Application.Features.Operacion.Muestreos.Commands.Carga
{
    public class CargaMuestreosCommand : IRequest<Response<bool>>
    {
        public List<CargaMuestreoDto> Muestreos { get; set; } = new List<CargaMuestreoDto>();
        public bool Validado { get; set; }
    }

    public class CargaMasivaMuestreosCommandHandler : IRequestHandler<CargaMuestreosCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _repository;

        public CargaMasivaMuestreosCommandHandler(IMuestreoRepository repositoryAsync)
        {
            _repository=repositoryAsync;
        }

        public async Task<Response<bool>> Handle(CargaMuestreosCommand request, CancellationToken cancellationToken)
        {
            var muestreos = _repository.ConvertToMuestreosList(request.Muestreos, request.Validado);

            _repository.InsertarRango(muestreos);

            return new Response<bool>(true);
        }
    }
}
