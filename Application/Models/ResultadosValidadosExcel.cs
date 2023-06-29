using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ResultadosValidadosExcel
    {
        public string claveSitio { get; set; }
        public string claveMonitoreo { get; set; }
        public string nombreSitio { get; set; }
        public string fechaRealizacion { get; set; }
        public string fechaProgramada { get; set; }                
        public string laboratorioRealizoMuestreo { get; set; }
        public string cuerpoAgua { get; set; }
        public string tipoCuerpoAgua { get; set; }
        public string subTipoCuerpoAgua { get; set; }       
        public string muestreoCompletoPorResultados { get; set; }

        public ResultadosValidadosExcel()
        {
            this.claveSitio = string.Empty;
            this.claveMonitoreo = string.Empty;
            this.nombreSitio = string.Empty;
            this.fechaRealizacion = string.Empty;
            this.fechaProgramada = string.Empty;            
            this.laboratorioRealizoMuestreo = string.Empty;
            this.cuerpoAgua = string.Empty;
            this.tipoCuerpoAgua = string.Empty;
            this.subTipoCuerpoAgua = string.Empty;            
            this.muestreoCompletoPorResultados = string.Empty;
        }
    }
  
}
