using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            AprobacionResultadoMuestreo = new HashSet<AprobacionResultadoMuestreo>();
            MuestreoUsuarioRevisionOcdl = new HashSet<Muestreo>();
            MuestreoUsuarioRevisionSecaia = new HashSet<Muestreo>();
        }

        public long Id { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Activo { get; set; }
        public long? CuencaId { get; set; }
        public long? DireccionLocalId { get; set; }
        public long PerfilId { get; set; }

        public virtual OrganismoCuenca? Cuenca { get; set; }
        public virtual DireccionLocal? DireccionLocal { get; set; }
        public virtual Perfil Perfil { get; set; } = null!;
        public virtual ICollection<AprobacionResultadoMuestreo> AprobacionResultadoMuestreo { get; set; }
        public virtual ICollection<Muestreo> MuestreoUsuarioRevisionOcdl { get; set; }
        public virtual ICollection<Muestreo> MuestreoUsuarioRevisionSecaia { get; set; }
    }
}
