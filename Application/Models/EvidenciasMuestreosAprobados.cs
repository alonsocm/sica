using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EvidenciasMuestreosAprobados
    {
        public string ClaveMuestreo { get; set; }
        public string ClaveSitio { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string Laboratorio { get; set; }
        public string LaboratorioMuestreo { get; set; }
        public string ConEventualidades { get; set; }
        public string FechaProgramada { get; set; }
        public string FechaRealVisita { get; set; }
        public string Brigada { get; set; }
        public string ConQcmuestreo { get; set; }
        public string TipoSupervision { get; set; }
        public string TipoEventualidad { get; set; }
        public string FechaReprogramacion { get; set; }
        public string PorcentajePago { get; set; }
        public string FechaValidacion { get; set; }

    }
}
