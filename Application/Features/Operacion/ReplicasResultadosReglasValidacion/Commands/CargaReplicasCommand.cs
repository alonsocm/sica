using Application.Features.Catalogos.Sitios.Commands;
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
    public class CargaReplicasCommand : IRequest<Response<bool>>
    {
        public List<ReplicasResultadoLabExterno> Replicas { get; set; } 

    }

    public class CargaReplicasHandler : IRequestHandler<CargaReplicasCommand, Response<bool>>
    {

        private readonly IReplicasResultadosReglasValidacionRepository _replicasRepository;


        public CargaReplicasHandler(IReplicasResultadosReglasValidacionRepository replicasRepository)
        {

            _replicasRepository = replicasRepository;

        }

        public async Task<Response<bool>> Handle(CargaReplicasCommand request, CancellationToken cancellationToken)
        {
            foreach (var replica in request.Replicas)
            {
                var nuevoRegistro = new Domain.Entities.ReplicasResultadosReglasValidacion()
                {
                    AceptaRechazo = (replica.AceptaRechazo.ToUpper() == "SI") ? true: false,
                    ResultadoReplica = replica.ResultadoReplica,
                    MismoResultado = (replica.MismoResultado.ToUpper() == "SI") ? true : false,
                    ObservacionLaboratorio = replica.ObservacionLaboratorio,
                    FechaReplicaLaboratorio = replica.FechaReplicaLaboratorio
                };
                _replicasRepository.Insertar(nuevoRegistro);

            }
            return new Response<bool>(true);
        }
    }
}
