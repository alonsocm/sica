using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ResultadosValidarExcel
    {

        public string claveSitio { get; set; }
        public string claveMonitoreo { get; set; }
        public string nombreSitio { get; set; }
        public string fechaRealizacion { get; set; }
        public string fechaProgramada { get; set; }
        public int diferenciaDias { get; set; }
        public string fechaEntregaTeorica { get; set; }
        public string laboratorioRealizoMuestreo { get; set; }
        public string cuerpoAgua { get; set; }
        public string tipoCuerpoAgua { get; set; }
        public string subTipoCuerpoAgua { get; set; }
        public int numParametrosEsperados { get; set; }
        public int numParametrosCargados { get; set; }
        public string muestreoCompletoPorResultados { get; set; }

        public ResultadosValidarExcel()
        {
            this.claveSitio = string.Empty;
            this.claveMonitoreo = string.Empty;
            this.nombreSitio = string.Empty;
            this.fechaRealizacion = string.Empty;
            this.fechaProgramada = string.Empty;
            this.diferenciaDias = 0;
            this.fechaEntregaTeorica = string.Empty;
            this.laboratorioRealizoMuestreo = string.Empty;
            this.cuerpoAgua = string.Empty;
            this.tipoCuerpoAgua = string.Empty;
            this.subTipoCuerpoAgua = string.Empty;
            this.numParametrosEsperados = 0;
            this.numParametrosCargados = 0;
            this.muestreoCompletoPorResultados = string.Empty;
        }
    }

    


}
