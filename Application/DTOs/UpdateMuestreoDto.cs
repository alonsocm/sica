using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateMuestreoDto
    {

        public int UsuarioId { get; set; }
        public int TipoAprobId { get; set; }
        public int EstatusId { get; set; }
        public long MuestreoId { get; set; }
        public List<ParametrosDto> lstparametros { get; set; }
        public bool isOCDL { get; set; }

        public UpdateMuestreoDto()
        {
            this.UsuarioId = 0;
            this.TipoAprobId = 0;
            this.EstatusId = 0;
            this.MuestreoId = 0;
            this.lstparametros = new List<ParametrosDto>();
            this.isOCDL = false;
        }


    }

    public class UpdateMuestreoExcelDto
    {
        public string? ClaveParametro { get; set; }
        public string? Resultado { get; set; }
        public string? ClaveMonitoreo { get; set; }
        public string? ObservacionOCDL { get; set; }
        public int Linea { get; set; }
    }

    public class UpdateMuestreoSECAIAExcelDto
    {   
        public string? NumeroEntrega { get; set; }
        public string? ClaveUnica { get; set; }    
        public string? ClaveSitio { get; set; }
        public string? ClaveMonitoreo { get; set; }
        public string? Nombre { get; set; }
        public string? ClaveParametro { get; set; }
        public string? Laboratorio { get; set; }
        public string? TipoCuerpoAgua { get; set; }
        public string? Resultado { get; set; }        
        public string? ObservacionSECAIA { get; set; }
        public int Linea { get; set; }

    }

}
