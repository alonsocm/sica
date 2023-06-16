using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Pagina
    {
        public Pagina()
        {
            InverseIdPaginaPadreNavigation = new HashSet<Pagina>();
            PerfilPagina = new HashSet<PerfilPagina>();
        }

        public long Id { get; set; }
        public long? IdPaginaPadre { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Url { get; set; } = null!;
        public long Orden { get; set; }
        public bool Activo { get; set; }

        public virtual Pagina? IdPaginaPadreNavigation { get; set; }
        public virtual ICollection<Pagina> InverseIdPaginaPadreNavigation { get; set; }
        public virtual ICollection<PerfilPagina> PerfilPagina { get; set; }
    }
}
