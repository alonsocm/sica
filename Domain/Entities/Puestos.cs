using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Puestos
{
    /// <summary>
    /// Identificador principal del catálogo Puestos
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el puesto
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Directorio> Directorio { get; set; } = new List<Directorio>();
}
