using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Accion
{
    /// <summary>
    /// Identificador principal del catálogo Acción
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Descripción de Acción
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<PerfilPaginaAccion> PerfilPaginaAccion { get; set; } = new List<PerfilPaginaAccion>();
}
