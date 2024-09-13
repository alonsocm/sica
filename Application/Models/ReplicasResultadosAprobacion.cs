using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ReplicasResultadosAprobacion
    {
        public string NumeroCarga { get; set; }
        public string ResultadoMuestreoId { get; set; }
        public string ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string Nombre { get; set; }
        public string ClaveParametro { get; set; }
        public string Laboratorio { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string TipoHomologado { get; set; }
        public string Resultado { get; set; }
        public string AprobacionResultadoReplica { get; set; }
    }
}
