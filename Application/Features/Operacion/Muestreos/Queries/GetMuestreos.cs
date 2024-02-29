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
                //Revisar porque se tiene los demas estatus
                estatus.Add((long)Enums.EstatusMuestreo.NoEnviado);
                
                //se comenta porque ya no es necesario este estatus aqui ya que despues de ser evidencias cargadas deben de pasar por la
                //validación de reglas
                //estatus.Add((long)Enums.EstatusMuestreo.EvidenciasCargadas);
                
                estatus.Add((long)Enums.EstatusMuestreo.Enviado);
                estatus.Add((long)Enums.EstatusMuestreo.EnviadoConExtensionFecha);
                estatus.Add((long)Enums.EstatusMuestreo.Validado);
            }

            var muestreos = await _repositoryAsync.GetResumenMuestreosAsync(estatus);
            return new Response<List<MuestreoDto>>(muestreos.ToList());
        }
    }
}
