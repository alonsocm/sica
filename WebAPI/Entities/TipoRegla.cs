using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoRegla
{
    /// <summary>
    /// Identificador principal de la tabla TipoRegla
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el tipo de regla
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; } = new List<ReglasMinimoMaximo>();
}
