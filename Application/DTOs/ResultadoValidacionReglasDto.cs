using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ResultadoValidacionReglasDto
    {
        public int Anio { get; set; }
        public int NoEntrega { get; set; }
        public string TipoSitio { get; set; } = string.Empty;
        public string ClaveUnica { get; set; } = string.Empty;
        public string ClaveSitio { get; set; } = string.Empty;
        public string ClaveMonitoreo { get; set; } = string.Empty;
        public string FechaRealizacion { get; set; } = string.Empty;
        public string Laboratorio { get; set; } = string.Empty;
        public string ClaveParametro { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
        public string ValidacionPorReglas { get; set; } = string.Empty;
        public string FechaAplicacionReglas { get; set; } = string.Empty;
    }
}
