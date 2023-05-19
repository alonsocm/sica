using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ParametrosGrupo
    {
        public ParametrosGrupo()
        {
            FormaReporteEspecifica = new HashSet<FormaReporteEspecifica>();
            ReglasLaboratorioLdmLpc = new HashSet<ReglasLaboratorioLdmLpc>();
            ReglasMinimoMaximo = new HashSet<ReglasMinimoMaximo>();
            ReglasRelacionParametro = new HashSet<ReglasRelacionParametro>();
            ReglasReporte = new HashSet<ReglasReporte>();
            ResultadoMuestreo = new HashSet<ResultadoMuestreo>();
        }

        public long Id { get; set; }
        public string ClaveParametro { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public long IdSubgrupo { get; set; }
        public long? IdUnidadMedida { get; set; }
        public long? Orden { get; set; }

        public virtual SubgrupoAnalitico IdSubgrupoNavigation { get; set; } = null!;
        public virtual UnidadMedida? IdUnidadMedidaNavigation { get; set; }
        public virtual ICollection<FormaReporteEspecifica> FormaReporteEspecifica { get; set; }
        public virtual ICollection<ReglasLaboratorioLdmLpc> ReglasLaboratorioLdmLpc { get; set; }
        public virtual ICollection<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; }
        public virtual ICollection<ReglasRelacionParametro> ReglasRelacionParametro { get; set; }
        public virtual ICollection<ReglasReporte> ReglasReporte { get; set; }
        public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; }
    }
}
