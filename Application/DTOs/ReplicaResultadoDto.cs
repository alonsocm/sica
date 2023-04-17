using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReplicaResultadoDto
    {
        public string NoEntrega { get; set; }
        public string ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string Nombre { get; set; }
        public string ClaveParametro { get; set; }
        public string Laboratorio { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string TipoCuerpoAguaOriginal { get; set; }
        public string Resultado { get; set; }
        public string EsCorrectoOCDL { get; set; }
        public string ObservacionOCDL { get; set; }
        public string EsCorrectoSECAIA { get; set; }
        public string ObservacionSECAIA { get; set; }
        public string ClasificacionObservacion { get; set; }
        public string CausaRechazo { get; set; }
        public string ResultadoAceptado { get; set; }
        public string ResultadoReplica { get; set; }
        public string EsMismoResultado { get; set; }
        public string ObservacionLaboratorio { get; set; }
        public string FechaReplicaLaboratorio { get; set; }
        public string ObservacionSRNAMECA { get; set; }
        public string Comentarios { get; set; }
        public string FechaObservacionRENAMECA { get; set; }
        public string ResultadoAprobadoDespuesReplica { get; set; }
        public string FechaEstatusFinal { get; set; }
        public string UsuarioRevisor { get; set; }
        public string EstatusResultado { get; set; }
        public string NombreArchivoEvidencias { get; set; }
    }
}
