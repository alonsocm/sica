using Application.DTOs;
using Application.Features.Operacion.Replicas.Queries.ReplicasTotal;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Queries
{
    public class ReplicasResumen : IRequest<Response<List<ReplicaResumenDto>>>
    {

    }

    public class ReplicasResumenHandler : IRequestHandler<ReplicasResumen, Response<List<ReplicaResumenDto>>>
    { 
        private readonly IVwReplicaRevisionResultadoRepository _vwReplicaRepository;

        public ReplicasResumenHandler(IVwReplicaRevisionResultadoRepository vwReplicaRepository)
        {
            _vwReplicaRepository = vwReplicaRepository;
        }

        public async Task<Response<List<ReplicaResumenDto>>> Handle(ReplicasResumen request, CancellationToken cancellationToken)
        {
            var replicasResultados = await _vwReplicaRepository.GetResumenResultadosReplicaAsync();
            return new Response<List<ReplicaResumenDto>>(replicasResultados.ToList());
        }
    }
}
