﻿using Application.DTOs;
using Application.Enums;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Muestreos.Commands.Liberacion
{
    public class EnvioRevisionMuestreosCommand : IRequest<Response<bool>>
    {
        public List<MuestreoRevisionDto> Muestreos { get; set; }
    }

    public class EnvioRevisionMuestreosCommandHandler : IRequestHandler<EnvioRevisionMuestreosCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public EnvioRevisionMuestreosCommandHandler(IMuestreoRepository repositoryAsync)
        {
            _repositoryAsync=repositoryAsync;
        }

        public async Task<Response<bool>> Handle(EnvioRevisionMuestreosCommand request, CancellationToken cancellationToken)
        {
            foreach (var muestreoRequest in request.Muestreos)
            {
                var muestreo = await _repositoryAsync.ObtenerElementoPorIdAsync(muestreoRequest.MuestreoId);

                if (muestreo != null)
                {
                    muestreo.EstatusId = (int)Enums.EstatusMuestreo.Enviado;
                    muestreo.FechaLimiteRevision = DateTime.TryParse(muestreoRequest.FechaRevision, out var fechaLimite) ? fechaLimite : DateTime.Now;
                    muestreo.NumeroEntrega = 1;

                    _repositoryAsync.Actualizar(muestreo);
                }
            }

            return new Response<bool>(true);
        }
    }
}
