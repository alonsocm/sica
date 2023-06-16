using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class MuestreosValidadosExcel

    {   
        public string noEntrega { get; set; }
        public string? claveUnica { get; set; }
        public string claveSitio { get; set; }
        public string claveMonitoreo { get; set; }
        public string nombreSitio { get; set; }
        public string? claveParametro { get; set; }
        public string laboratorio { get; set; }
        public string? tipoCuerpoAgua { get; set; }
        public string? resultado { get; set; }
        public string? observacionSECAIA  { get; set; }
        public string? fechaRevision { get; set; }
        public string nombreUsuario { get; set; }
        public string estatusResultado { get; set; }
        public MuestreosValidadosExcel()
        {
            
            this.noEntrega = string.Empty;
            this.claveUnica = null;
            this.claveSitio = string.Empty;
            this.claveMonitoreo= string.Empty;
            this.nombreSitio  = string.Empty;
            this.claveParametro= string.Empty;
            this.laboratorio  = string.Empty;
            this.tipoCuerpoAgua = string.Empty;
            this.resultado = string.Empty;
            this.observacionSECAIA = string.Empty;
            this.fechaRevision = string.Empty;
            this.nombreUsuario  = string.Empty;
            this.estatusResultado = string.Empty;
        }

    }
}
