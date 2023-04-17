using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TipoEvidenciaMuestreo
    {
        public TipoEvidenciaMuestreo()
        {
            EvidenciaMuestreo = new HashSet<EvidenciaMuestreo>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Sufijo { get; set; } = null!;
        public string Extension { get; set; } = null!;

        public virtual ICollection<EvidenciaMuestreo> EvidenciaMuestreo { get; set; }
    }
}
