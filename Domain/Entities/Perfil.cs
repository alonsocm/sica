using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Perfil
{
    /// <summary>
    /// Identificador principal del catálogo Perfil
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el nombre del perfil
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Campo que describe el estatus del perfil
    /// </summary>
    public bool Estatus { get; set; }

    public virtual ICollection<PerfilPagina> PerfilPagina { get; set; } = new List<PerfilPagina>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
