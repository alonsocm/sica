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
        private readonly IResultado _resultadoMuestreoRepository;
        private readonly IRepositoryAsync<EvidenciasReplicasResultadoReglasValidacion> _evidenciaReplicaRepository;


        public CargaReplicasHandler(IReplicasResultadosReglasValidacionRepository replicasRepository, IResultado resultadoMuestreoRepository, 
            IRepositoryAsync<EvidenciasReplicasResultadoReglasValidacion> evidenciaReplicaRepository)
        {

            _replicasRepository = replicasRepository;
            _resultadoMuestreoRepository = resultadoMuestreoRepository;
            _evidenciaReplicaRepository = evidenciaReplicaRepository;
        }

        public async Task<Response<bool>> Handle(CargaReplicasCommand request, CancellationToken cancellationToken)
        {

            List<string> archivos = new List<string>();
            foreach (var replica in request.Replicas)
            {
                var nuevoRegistro = new Domain.Entities.ReplicasResultadosReglasValidacion()
                {
                    ResultadoMuestreoId = Convert.ToInt64(replica.ResultadoMuestreoId),
                    AceptaRechazo = (replica.AceptaRechazo.ToUpper() == "SI") ? true: false,
                    ResultadoReplica = replica.ResultadoReplica,
                    MismoResultado = (replica.MismoResultado.ToUpper() == "SI") ? true : false,
                    ObservacionLaboratorio = replica.ObservacionLaboratorio,
                    FechaReplicaLaboratorio = Convert.ToDateTime(replica.FechaReplicaLaboratorio),
                    
                };

                if (replica.NombreArchivoEvidencia != string.Empty)
                {
                    archivos.AddRange(replica.NombreArchivoEvidencia.Split('/'));
                }

                var resultado = await _resultadoMuestreoRepository.ObtenerElementoPorIdAsync(Convert.ToInt64(replica.ResultadoMuestreoId));
                resultado.EstatusResultadoId = (int?)Enums.EstatusResultado.CargaRéplicasLaboratorioExterno;
                _resultadoMuestreoRepository.Actualizar(resultado);
                _replicasRepository.Insertar(nuevoRegistro);               

            }

            archivos.Distinct().ToList().ForEach(x => _evidenciaReplicaRepository.AddAsync(new Domain.Entities.EvidenciasReplicasResultadoReglasValidacion()
            {
                NombreArchivo = x
            })); ;
            return new Response<bool>(true);
        }
    }
}
