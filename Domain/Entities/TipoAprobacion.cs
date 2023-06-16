using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TipoAprobacion
    {
        public TipoAprobacion()
        {
            Muestreo = new HashSet<Muestreo>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Muestreo> Muestreo { get; set; }
    }
}
