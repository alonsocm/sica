using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class CuencaDireccionesLocales
    {
        public CuencaDireccionesLocales()
        {
            Sitio = new HashSet<Sitio>();
        }

        public long Id { get; set; }
        public long OcuencaId { get; set; }
        public long? DlocalId { get; set; }
        public bool Activo { get; set; }

        public virtual DireccionLocal? Dlocal { get; set; }
        public virtual OrganismoCuenca Ocuenca { get; set; } = null!;
        public virtual ICollection<Sitio> Sitio { get; set; }
    }
}
