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
    public class EnviarResultados : IRequest<Response<bool>>
    {
        public List<string> ClavesUnicas { get; set; }
        public string UsuarioId { get; set; }
        public bool Aprobado { get; set; }
    }

    public class EnviarResultadosHandler : IRequestHandler<EnviarResultados, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;
        private readonly IVwReplicaRevisionResultadoRepository _replicaRepository;

        public EnviarResultadosHandler(IResultado resultadoRepository, IVwReplicaRevisionResultadoRepository replicaRepository)
        {
            _resultadoRepository=resultadoRepository;
            _replicaRepository=replicaRepository;
        }

        public async Task<Response<bool>> Handle(EnviarResultados request, CancellationToken cancellationToken)
        {
            foreach (var claveUnica in request.ClavesUnicas)
            {
                var replicaDb = _replicaRepository.ObtenerElementosPorCriterio(x => x.ClaveUnica == claveUnica).FirstOrDefault();

                if (replicaDb != null)
                {
                    var resultado = await _resultadoRepository.ObtenerElementoPorIdAsync(replicaDb.ResultadoMuestreoId);

                    if (resultado != null)
                    {
                        //TODO: Falta agregar el idusuario que cambió el estatus
                        //TODO: Agregar los estatus al enum
                        resultado.EstatusResultado = request.Aprobado ? 19 : 18;
                        _resultadoRepository.Actualizar(resultado);
                    }
                }
            }

            return new Response<bool>(true);
        }
    }
}
