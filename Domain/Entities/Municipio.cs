using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Municipio
    {
        public Municipio()
        {
            Localidad = new HashSet<Localidad>();
            Sitio = new HashSet<Sitio>();
        }

        public long Id { get; set; }
        public long EstadoId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual Estado Estado { get; set; } = null!;
        public virtual ICollection<Localidad> Localidad { get; set; }
        public virtual ICollection<Sitio> Sitio { get; set; }
    }
}
