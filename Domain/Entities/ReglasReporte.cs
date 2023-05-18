using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ReglasReporte
    {
        public ReglasReporte()
        {
            ReglaReporteFormaReporte = new HashSet<ReglaReporteFormaReporte>();
            ReglaReporteResultadoTca = new HashSet<ReglaReporteResultadoTca>();
        }

        public long Id { get; set; }
        public string ClaveRegla { get; set; } = null!;
        public long ClasificacionReglaId { get; set; }
        public long ParametroId { get; set; }
        public bool EsValidoResultadoCero { get; set; }
        public bool EsValidoMenorCero { get; set; }
        public bool EsValidoEspaciosBlanco { get; set; }
        public bool EsValidoResultadoNa { get; set; }
        public bool EsValidoResultadoNe { get; set; }
        public bool EsValidoResultadoIm { get; set; }
        public bool EsValidoResultadoMenorLd { get; set; }
        public bool EsValidoResultadoMenorCmc { get; set; }
        public bool EsValidoResultadoNd { get; set; }
        public bool EsValidoResultadoMenorLpc { get; set; }

        public virtual ClasificacionRegla ClasificacionRegla { get; set; } = null!;
        public virtual ParametrosGrupo Parametro { get; set; } = null!;
        public virtual ICollection<ReglaReporteFormaReporte> ReglaReporteFormaReporte { get; set; }
        public virtual ICollection<ReglaReporteResultadoTca> ReglaReporteResultadoTca { get; set; }
    }
}
