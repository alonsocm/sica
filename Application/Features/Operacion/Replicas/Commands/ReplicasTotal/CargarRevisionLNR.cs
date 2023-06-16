using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Commands.ReplicasTotal
{
    public class CargarRevisionLNR : IRequest<Response<bool>>
    {
        public List<RevisionLNRDto> Replicas { get; set; }
    }

    public class CargarRevisionLNRHandler : IRequestHandler<CargarRevisionLNR, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;
        private readonly IVwReplicaRevisionResultadoRepository _replicaRepository;

        public CargarRevisionLNRHandler(IResultado resultadoRepository, IVwReplicaRevisionResultadoRepository replicaRepository)
        {
            _resultadoRepository=resultadoRepository;
            _replicaRepository=replicaRepository;
        }

        public async Task<Response<bool>> Handle(CargarRevisionLNR request, CancellationToken cancellationToken)
        {
            foreach (var replica in request.Replicas)
            {
                var replicaDb = _replicaRepository.ObtenerElementosPorCriterio(x => x.ClaveUnica == replica.ClaveUnica).FirstOrDefault();

                if (replicaDb != null)
                {
                    var resultado = await _resultadoRepository.ObtenerElementoPorIdAsync(replicaDb.ResultadoMuestreoId);

                    if (resultado != null)
                    {
                        resultado.ObservacionSrenameca = replica.ObservacionSRENAMECA;
                        //resultado.Comentarios = replica.Comentarios;TODO: Agregar campo 'Comentarios' en tabla ResultadoMuestreo
                        resultado.FechaObservacionSrenameca = DateTime.Now;
                        resultado.EstatusResultado = 17;

                        _resultadoRepository.Actualizar(resultado);
                    }
                }
            }

            return new Response<bool>(true);
        }
    }
}
