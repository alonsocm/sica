using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TotalesExcel
    {        
        public string? NumeroEntrega { get; set; }
        public string ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string Nombre { get; set; }
        public string ClaveParametro { get; set; }
        public string Laboratorio { get; set; }
        public string? TipoCuerpoAgua { get; set; }
        public string? TipoCuerpoAguaOriginal { get; set; }
        public string Resultado { get; set; }
        public string? ObservacionOCDL { get; set; }
    }

    public class ExcelValidadosOCDL
    {
        public string? NumeroEntrega { get; set; }
        public string ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string Nombre { get; set; }
        public string ClaveParametro { get; set; }
        public string Laboratorio { get; set; }
        public string? TipoCuerpoAgua { get; set; }
        public string? TipoCuerpoAguaOriginal { get; set; }
        public string Resultado { get; set; }
        public string TipoAprobacion { get; set; }
        public string ResultadoCorrecto { get; set; }
        public string? ObservacionOCDL { get; set; }
        public string FechaLimite { get; set; }
        public string Usuario { get; set; }
        public string FechaRealizacion { get; set; }
        public string Estatus { get; set; }
    }
}

