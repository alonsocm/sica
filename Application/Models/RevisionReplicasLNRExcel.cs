using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class RevisionReplicasLNRExcel
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
        public string ResultadoCorrectoOCDL { get; set; }
        public string ObservacionOCDL { get; set; }
        public string ResultadoCorrectoSECAIA { get; set; }
        public string ObservacionSECAIA { get; set; }
        public string ClasificacionObservacion { get; set; }
        public string CausaRechazo { get; set; }
        public string SeAceptaRechazo { get; set; } 
        public string ResultadoReplica { get; set; }
        public string EsMismoResultado { get; set; }
        public string ObservacionLaboratorio { get; set; } 
        public string FechaReplicaLaboratorio { get; set; }
        public string ObservacionSRENAMECA { get; set; }
        public string Comentarios { get; set; }
    }
}
