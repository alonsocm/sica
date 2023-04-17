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
    public class EnviarResultadoDifDato : IRequest<Response<bool>>
    {
        public List<string> ClavesUnicas { get; set; }
        public string UsuarioId { get; set; }
    }

    public class EnviarResultadoDifDatoHandler : IRequestHandler<EnviarResultadoDifDato, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;
        private readonly IVwReplicaRevisionResultadoRepository _replicaRepository;

        public EnviarResultadoDifDatoHandler(IResultado resultadoRepository, IVwReplicaRevisionResultadoRepository replicaRepository)
        {
            _resultadoRepository=resultadoRepository;
            _replicaRepository=replicaRepository;
        }

        public async Task<Response<bool>> Handle(EnviarResultadoDifDato request, CancellationToken cancellationToken)
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
                        resultado.EstatusResultado = 13;
                        _resultadoRepository.Actualizar(resultado);
                    }
                }
            }

            return new Response<bool>(true);
        }
    }
}
