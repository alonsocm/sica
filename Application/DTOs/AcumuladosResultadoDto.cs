using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AcumuladosResultadoDto :MuestreoDto
    {
        public string ClaveUnica { get; set; }
        public string SubTipoCuerpoAgua { get; set; }
        public string laboratorioRealizoMuestreo { get; set; }
        public string SubGrupo { get; set; }
        public string claveParametro { get; set; }
        public string Parametro { get; set; }
        public string UnidadMedida { get; set; }
        public string Resultado { get; set; }

        public AcumuladosResultadoDto()
        {   this.ClaveUnica = string.Empty;
            this.SubTipoCuerpoAgua = string.Empty;
            this.laboratorioRealizoMuestreo = string.Empty;
            this.SubGrupo = string.Empty;
            this.claveParametro = string.Empty;
            this.Parametro = string.Empty;
            this.UnidadMedida = string.Empty;
            this.Resultado = string.Empty;
        }
    }
}
