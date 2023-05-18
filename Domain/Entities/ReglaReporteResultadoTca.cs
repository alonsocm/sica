using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ReglaReporteResultadoTca
    {
        public long Id { get; set; }
        public long ReglaReporteId { get; set; }
        public long TipoCuerpoAguaId { get; set; }

        public virtual ReglasReporte ReglaReporte { get; set; } = null!;
        public virtual TipoCuerpoAgua TipoCuerpoAgua { get; set; } = null!;
    }
}
