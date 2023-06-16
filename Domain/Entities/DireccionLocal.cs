using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class DireccionLocal
    {
        public DireccionLocal()
        {
            CuencaDireccionesLocales = new HashSet<CuencaDireccionesLocales>();
            Sitio = new HashSet<Sitio>();
            Usuario = new HashSet<Usuario>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Clave { get; set; } = null!;

        public virtual ICollection<CuencaDireccionesLocales> CuencaDireccionesLocales { get; set; }
        public virtual ICollection<Sitio> Sitio { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
