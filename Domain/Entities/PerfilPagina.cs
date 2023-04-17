using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class PerfilPagina
    {
        public PerfilPagina()
        {
            PerfilPaginaAccion = new HashSet<PerfilPaginaAccion>();
        }

        public long Id { get; set; }
        public long IdPerfil { get; set; }
        public long IdPagina { get; set; }
        public bool Estatus { get; set; }

        public virtual Pagina IdPaginaNavigation { get; set; } = null!;
        public virtual Perfil IdPerfilNavigation { get; set; } = null!;
        public virtual ICollection<PerfilPaginaAccion> PerfilPaginaAccion { get; set; }
    }
}
