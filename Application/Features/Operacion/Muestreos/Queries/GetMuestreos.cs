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

namespace Application.Features.Muestreos.Queries
{
    public class GetMuestreos : IRequest<Response<List<MuestreoDto>>>  
    {
        public bool EsLiberacion { get; set; }
    }

    public class GetMuestreosHandler : IRequestHandler<GetMuestreos, Response<List<MuestreoDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetMuestreosHandler(IMuestreoRepository repositoryAsync)
        {
            _repositoryAsync=repositoryAsync;
        }

        public async Task<Response<List<MuestreoDto>>> Handle(GetMuestreos request, CancellationToken cancellationToken)
        {
            var estatus = new List<long>();

            if (!request.EsLiberacion)
            {
                estatus.Add((long)Enums.EstatusMuestreo.Cargado);
                estatus.Add((long)Enums.EstatusMuestreo.EvidenciasCargadas);
            }
            else
            {
                estatus.Add((long)Enums.EstatusMuestreo.NoEnviado);
                estatus.Add((long)Enums.EstatusMuestreo.EvidenciasCargadas);
                estatus.Add((long)Enums.EstatusMuestreo.Enviado);
                estatus.Add((long)Enums.EstatusMuestreo.EnviadoConExtensionFecha);
                estatus.Add((long)Enums.EstatusMuestreo.Validado);
            }

            var muestreos = await _repositoryAsync.GetResumenMuestreosAsync(estatus);
            return new Response<List<MuestreoDto>>(muestreos.ToList());
        }
    }
}
