using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ReglaReporteFormaReporte
    {
        public long Id { get; set; }
        public long ReglaReporteId { get; set; }
        public long FormaReporteId { get; set; }

        public virtual FormaReporte FormaReporte { get; set; } = null!;
        public virtual ReglasReporte ReglaReporte { get; set; } = null!;
    }
}
