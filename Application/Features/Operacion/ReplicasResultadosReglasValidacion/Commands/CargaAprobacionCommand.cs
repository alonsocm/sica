using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ReplicasResultadosReglasValidacion.Commands
{
    public class CargaAprobacionCommand: IRequest<Response<bool>>
    {
        public List<ReplicasResultadosAprobacion> Replicas { get; set; }
        public int? usuarioIdValido { get; set; }
    }

    public class CargaAprobacionHandler : IRequestHandler<CargaAprobacionCommand, Response<bool>>
    {

        private readonly IReplicasResultadosReglasValidacionRepository _replicasRepository;
        private readonly IResultado _resultadoMuestreoRepository;


        public CargaAprobacionHandler(IReplicasResultadosReglasValidacionRepository replicasRepository, IResultado resultadoMuestreoRepository)
        {

            _replicasRepository = replicasRepository;
            _resultadoMuestreoRepository = resultadoMuestreoRepository;
        }

        public async Task<Response<bool>> Handle(CargaAprobacionCommand request, CancellationToken cancellationToken)
        {

            foreach (var replica in request.Replicas)
            {
                var replicaResultado = _replicasRepository.ObtenerElementosPorCriterioAsync(x => x.ResultadoMuestreoId.Equals(Convert.ToInt64(replica.ResultadoMuestreoId))).Result.FirstOrDefault();
                replicaResultado.ApruebaResultadoReplica = (replica.AprobacionResultadoReplica.ToUpper() == "SI") ? true : false;
                replicaResultado.FechaEstatusFinal = DateTime.Now;
                replicaResultado.UsuarioIdReviso = Convert.ToInt64(request.usuarioIdValido);
                _replicasRepository.Actualizar(replicaResultado);

                var resultado = await _resultadoMuestreoRepository.ObtenerElementoPorIdAsync(Convert.ToInt64(replica.ResultadoMuestreoId));
                resultado.EstatusResultadoId = (replicaResultado.ApruebaResultadoReplica == true) ? (int?)Enums.EstatusResultado.AprobaciónResultadosPorArchivo : (int?)Enums.EstatusResultado.RechazoResultadosPorArchivo;
                _resultadoMuestreoRepository.Actualizar(resultado);
            }
            return new Response<bool>(true);
        }
    }
}
