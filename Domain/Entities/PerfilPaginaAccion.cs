using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class PerfilPaginaAccion
    {
        public long Id { get; set; }
        public long IdPerfilPagina { get; set; }
        public long IdAccion { get; set; }
        public bool Estatus { get; set; }

        public virtual Accion IdAccionNavigation { get; set; } = null!;
        public virtual PerfilPagina IdPerfilPaginaNavigation { get; set; } = null!;
    }
}
