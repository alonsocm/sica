using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Perfil
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estatus { get; set; }

    public virtual ICollection<PerfilPagina> PerfilPagina { get; set; } = new List<PerfilPagina>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
