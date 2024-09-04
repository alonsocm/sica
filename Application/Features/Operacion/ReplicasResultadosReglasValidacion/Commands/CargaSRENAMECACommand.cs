using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using FluentValidation.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ReplicasResultadosReglasValidacion.Commands
{
    public class CargaSRENAMECACommand: IRequest<Response<bool>>
    {
        public List<ReplicasResultadoSrenameca> Replicas { get; set; }

    }

    public class CargaSRENAMECAHandler : IRequestHandler<CargaSRENAMECACommand, Response<bool>>
    {

        private readonly IReplicasResultadosReglasValidacionRepository _replicasRepository;
        private readonly IResultado _resultadoMuestreoRepository;


        public CargaSRENAMECAHandler(IReplicasResultadosReglasValidacionRepository replicasRepository, IResultado resultadoMuestreoRepository)
        {

            _replicasRepository = replicasRepository;
            _resultadoMuestreoRepository = resultadoMuestreoRepository;
        }

        public async Task<Response<bool>> Handle(CargaSRENAMECACommand request, CancellationToken cancellationToken)
        {          

            foreach (var replica in request.Replicas)
            {
                var replicaResultado = _replicasRepository.ObtenerElementosPorCriterioAsync(x => x.ResultadoMuestreoId.Equals(Convert.ToInt64(replica.ResultadoMuestreoId))).Result.FirstOrDefault();
                replicaResultado.EsDatoCorrectoSrenameca = (replica.EsDatoCorrectoSrenameca.ToUpper() == "SI") ? true:false;
                replicaResultado.ObservacionSrenameca = replica.ObservacionSrenameca;
                replicaResultado.FechaObservacionSrenameca = Convert.ToDateTime(replica.FechaObservacionSrenameca);
                _replicasRepository.Actualizar(replicaResultado);

                var resultado = await _resultadoMuestreoRepository.ObtenerElementoPorIdAsync(Convert.ToInt64(replica.ResultadoMuestreoId));
                resultado.EstatusResultadoId = (int?)Enums.EstatusResultado.CargaValidaciónSRENAMECA;
                _resultadoMuestreoRepository.Actualizar(resultado);
            }
            return new Response<bool>(true);
        }
    }
}
