using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ResultadosAdministracionExcel
    {       
        public string ClaveUnica { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string ClaveSitio { get; set; }
        public string NombreSitio { get; set; }
        public string FechaRealizacion { get; set; }
        public string TipoSitio { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string SubTipoCuerpoAgua { get; set; }
        public string GrupoParametro { get; set; }
        public string Parametro { get; set; }
        public string UnidadMedida { get; set; }
        public string Resultado { get; set; }
        public string NuevoResultadoReplica { get; set; }
        public string Replica { get; set; }
        public string CambioResultado { get; set; }

    }
}
