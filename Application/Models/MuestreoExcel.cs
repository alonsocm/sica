using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class MuestreoExcel
    {
        public string OCDL { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string Estado { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string Laboratorio { get; set; }
        public string FechaRealizacion { get; set; }
        public string FechaLimiteRevision { get; set; }
        public string NumeroEntrega { get; set; }
        public string Estatus { get; set; }
        public string? TipoCargaResultados { get; set; } = string.Empty;
    }
}
