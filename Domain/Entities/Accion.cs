using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Accion
    {
        public Accion()
        {
            PerfilPaginaAccion = new HashSet<PerfilPaginaAccion>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<PerfilPaginaAccion> PerfilPaginaAccion { get; set; }
    }
}
