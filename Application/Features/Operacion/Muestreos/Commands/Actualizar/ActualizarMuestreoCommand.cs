using Application.DTOs;
using Application.Expressions;
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

namespace Application.Features.Operacion.Muestreos.Commands.Actualizar
{
    public class ActualizarMuestreoCommand: IRequest<Response<bool>>
    {
        public List<MuestreoDto> lstMuestreos { get; set; }
    }

    public class ActualizarMuestreoHandler : IRequestHandler<ActualizarMuestreoCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public ActualizarMuestreoHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(ActualizarMuestreoCommand request, CancellationToken cancellationToken)
        {           
            foreach (var muestreos in request.lstMuestreos)
            {
                Muestreo muestreo = _muestreoRepository.ObtenerElementoPorIdAsync(muestreos.MuestreoId).Result;
                muestreo.EstatusId = muestreos.EstatusId;
                muestreo.AutorizacionIncompleto = muestreos.autorizacionIncompleto;
                muestreo.AutorizacionFechaEntrega = muestreos.autorizacionFechaEntrega;
                _muestreoRepository.Actualizar(muestreo);
            }
          
            return new Response<bool>(true);
        }
    }



}
