using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Estado
    {
        public Estado()
        {
            Localidad = new HashSet<Localidad>();
            Municipio = new HashSet<Municipio>();
            Sitio = new HashSet<Sitio>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Abreviatura { get; set; } = null!;

        public virtual ICollection<Localidad> Localidad { get; set; }
        public virtual ICollection<Municipio> Municipio { get; set; }
        public virtual ICollection<Sitio> Sitio { get; set; }
    }
}
