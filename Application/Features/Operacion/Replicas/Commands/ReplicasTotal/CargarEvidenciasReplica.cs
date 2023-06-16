using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Commands.ReplicasTotal
{
    public class CargarEvidenciasReplica : IRequest<Response<bool>>
    {
        public List<IFormFile> Archivos { get; set; }
    }

    public class CargarEvidenciasReplicaHandler : IRequestHandler<CargarEvidenciasReplica, Response<bool>>
    {
        private readonly IEvidenciaReplicaRepository _evidenciaReplicaRepository;

        public CargarEvidenciasReplicaHandler(IEvidenciaReplicaRepository evidenciaReplicaRepository)
        {
            _evidenciaReplicaRepository=evidenciaReplicaRepository;
        }

        public async Task<Response<bool>> Handle(CargarEvidenciasReplica request, CancellationToken cancellationToken)
        {
            foreach (var archivo in request.Archivos)
            {
                var evidencias = _evidenciaReplicaRepository.ObtenerElementosPorCriterio(x => x.NombreArchivo == archivo.FileName);

                if (evidencias != null)
                {
                    evidencias.ToList().ForEach(evidencia =>
                    {
                        using var ms = new MemoryStream();
                        archivo.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        evidencia.Archivo = fileBytes;
                        _evidenciaReplicaRepository.Actualizar(evidencia);
                    });
                }
            }

            return new Response<bool>(true);
        }
    }
}
