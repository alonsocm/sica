using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Perfil
    {
        public Perfil()
        {
            PerfilPagina = new HashSet<PerfilPagina>();
            Usuario = new HashSet<Usuario>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estatus { get; set; }

        public virtual ICollection<PerfilPagina> PerfilPagina { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
