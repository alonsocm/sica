using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ClasificacionRegla
    {
        public ClasificacionRegla()
        {
            ReglasMinimoMaximo = new HashSet<ReglasMinimoMaximo>();
            ReglasReporte = new HashSet<ReglasReporte>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; }
        public virtual ICollection<ReglasReporte> ReglasReporte { get; set; }
    }
}
