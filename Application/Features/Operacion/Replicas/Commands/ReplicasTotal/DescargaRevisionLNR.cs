using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Commands.ReplicasTotal
{
    public class DescargaRevisionLNR : IRequest<Response<bool>>
    {   
        public List<RevisionReplicasLNRExcel> Replicas;
    }

    public class DescargaRevisionLNRHandler : IRequestHandler<DescargaRevisionLNR, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;
        private readonly IVwReplicaRevisionResultadoRepository _replicaRepository;

        public DescargaRevisionLNRHandler(IResultado resultadoRepository, IVwReplicaRevisionResultadoRepository replicaRepository)
        {
            _resultadoRepository=resultadoRepository;
            _replicaRepository=replicaRepository;
        }

        public async Task<Response<bool>> Handle(DescargaRevisionLNR request, CancellationToken cancellationToken)
        {
            foreach (var replica in request.Replicas)
            {
                var replicaDb = _replicaRepository.ObtenerElementosPorCriterio(x => x.ClaveUnica == replica.ClaveUnica).FirstOrDefault();

                if (replicaDb != null)
                {
                    var resultado = await _resultadoRepository.ObtenerElementoPorIdAsync(replicaDb.ResultadoMuestreoId);

                    if (resultado != null)
                    {                        
                        resultado.EstatusResultado = 15;

                        _resultadoRepository.Actualizar(resultado);
                    }
                }
            }

            return new Response<bool>(true);
        }
    }
}
