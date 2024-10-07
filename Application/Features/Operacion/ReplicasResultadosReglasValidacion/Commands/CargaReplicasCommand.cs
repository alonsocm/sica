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
     

        private readonly IEvidenciasReplicasResultadoReglasValidacionRepository _evidenciaReplicaRepository;
        private readonly IRelacionEvidenciasReplicaResultadosReglasRepository _relacionEvidenciaReplicaRepository;


        public CargaReplicasHandler(IReplicasResultadosReglasValidacionRepository replicasRepository, 
            IResultado resultadoMuestreoRepository,           
            IEvidenciasReplicasResultadoReglasValidacionRepository evidenciaReplicaRepository, 
            IRelacionEvidenciasReplicaResultadosReglasRepository relacionEvidenciaReplicaRepository)
        {

            _replicasRepository = replicasRepository;
            _resultadoMuestreoRepository = resultadoMuestreoRepository;
            _evidenciaReplicaRepository = evidenciaReplicaRepository;
            _relacionEvidenciaReplicaRepository = relacionEvidenciaReplicaRepository;

            
        }

        public async Task<Response<bool>> Handle(CargaReplicasCommand request, CancellationToken cancellationToken)
        {

            List<string> archivos = new List<string>();
            List<long> lstReplicas = new List<long>();

            List<long> lstResultadosArchivo = new List<long>();

            foreach (var replica in request.Replicas)
            {
                var nuevoRegistro = new Domain.Entities.ReplicasResultadosReglasValidacion()
                {
                    ResultadoMuestreoId = Convert.ToInt64(replica.ResultadoMuestreoId),
                    AceptaRechazo = (replica.AceptaRechazo.ToUpper() == "SI") ? true : false,
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
                lstReplicas.Add(_replicasRepository.Insertar(nuevoRegistro));

            }

            var replicasInsertadas = _replicasRepository.ObtenerElementosPorCriterioAsync(x => lstReplicas.Contains(x.Id)).Result.ToList();

            archivos = archivos.Distinct().ToList();

            foreach (var archivo in archivos)
            {
                var evidencia = _evidenciaReplicaRepository.Insertar(new Domain.Entities.EvidenciasReplicasResultadoReglasValidacion()
                {
                    NombreArchivo = archivo
                });

                List<ReplicasResultadoLabExterno> lsrReplicasArchivo = request.Replicas.Where(x => x.NombreArchivoEvidencia.Contains(archivo)).ToList();
                var datos = replicasInsertadas.Where(x => lsrReplicasArchivo.Select(x => Convert.ToInt64(x.ResultadoMuestreoId)).Contains(x.ResultadoMuestreoId));             

                datos.ToList().ForEach(x => _relacionEvidenciaReplicaRepository.Insertar(new Domain.Entities.RelacionEvidenciasReplicaResultadosReglas()
                { ReplicasResultadosReglasValidacionId = x.Id, EvidenciasReplicasResultadoReglasValidacionId = evidencia }));
            }           
            return new Response<bool>(true);
        }
    }
}
