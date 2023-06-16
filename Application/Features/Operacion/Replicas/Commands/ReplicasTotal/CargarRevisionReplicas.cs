using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Commands.ReplicasTotal
{
    public class CargarRevisionReplicas : IRequest<Response<bool>>
    {
        public List<RevisionReplicasDto> Replicas { get; set; }
    }

    public class CargarRevisionReplicasHandler : IRequestHandler<CargarRevisionReplicas, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;
        private readonly IVwReplicaRevisionResultadoRepository _replicaRepository;

        public CargarRevisionReplicasHandler(IResultado resultadoRepository, IVwReplicaRevisionResultadoRepository replicaRepository)
        {
            _resultadoRepository=resultadoRepository;
            _replicaRepository=replicaRepository;
        }

        public async Task<Response<bool>> Handle(CargarRevisionReplicas request, CancellationToken cancellationToken)
        {
            foreach (var replica in request.Replicas)
            {
                var replicaDb = _replicaRepository.ObtenerElementosPorCriterio(x => x.ClaveUnica == replica.ClaveUnica).FirstOrDefault();

                if (replicaDb != null)
                {
                    var resultado = await _resultadoRepository.ObtenerElementoPorIdAsync(replicaDb.ResultadoMuestreoId);

                    if (resultado != null)
                    {
                        resultado.SeAceptaRechazoSiNo = replica.SeAceptaRechazo.ToUpper() == "SI";
                        resultado.ResultadoReplica = replica.ResultadoReplica;
                        resultado.EsMismoResultado = replica.ResultadoReplica == resultado.Resultado;
                        resultado.ObservacionLaboratorio = replica.ObservacionLaboratorio;
                        resultado.FechaReplicaLaboratorio = DateTime.Now;
                        resultado.EstatusResultado = 14;

                        //Aquí vamos a sacar los nombres de las evidencias, que deben venir separados por comas
                        var nombresEvidencias = replica.NombreArchivoEvidencia.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (var nombre in nombresEvidencias)
                        {
                            var evidenciaReplica = new EvidenciaReplica()
                            {
                                ResultadoMuestreoId = resultado.Id,
                                NombreArchivo = nombre.Trim(),
                                ClaveUnica = replica.ClaveUnica,
                            };

                            resultado.EvidenciaReplica.Add(evidenciaReplica);
                        };

                        _resultadoRepository.Actualizar(resultado);
                    }
                }
            }

            return new Response<bool>(true);
        }
    }
}
