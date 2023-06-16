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

namespace Application.Features.Operacion.Replicas.Queries.ReplicasTotal
{
    public class ObtenerEvidenciasReplica : IRequest<Response<List<ArchivoDto>>>
    {
        public List<string> ClavesUnicas { get; set; }
    }

    public class ObtenerEvidenciasReplicaHandler : IRequestHandler<ObtenerEvidenciasReplica, Response<List<ArchivoDto>>>
    {
        private readonly IEvidenciaReplicaRepository _evidenciaReplicaRepository;

        public ObtenerEvidenciasReplicaHandler(IEvidenciaReplicaRepository resultadoRepository)
        {
            _evidenciaReplicaRepository=resultadoRepository;
        }

        public async Task<Response<List<ArchivoDto>>> Handle(ObtenerEvidenciasReplica request, CancellationToken cancellationToken)
        {
            var evidenciasReplica = new List<ArchivoDto>();

            foreach (var claveUnica in request.ClavesUnicas)
            {
                var evidencias = await _evidenciaReplicaRepository.ObtenerElementosPorCriterioAsync(x => x.ClaveUnica == claveUnica && x.Archivo != null);

                evidencias.ToList().ForEach(evidencia =>
                {
                    var evidenciaDto = new ArchivoDto()
                    {
                        NombreArchivo = $"{evidencia.ClaveUnica}&{evidencia.NombreArchivo}",
                        Archivo = evidencia.Archivo
                    };

                    evidenciasReplica.Add(evidenciaDto);
                });
            }

            return new Response<List<ArchivoDto>>(evidenciasReplica);
        }
    }
}
