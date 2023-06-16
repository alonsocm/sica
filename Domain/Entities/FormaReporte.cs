using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class FormaReporte
    {
        public FormaReporte()
        {
            ReglaReporteFormaReporte = new HashSet<ReglaReporteFormaReporte>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<ReglaReporteFormaReporte> ReglaReporteFormaReporte { get; set; }
    }
}
