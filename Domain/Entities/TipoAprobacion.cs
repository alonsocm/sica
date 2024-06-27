using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoAprobacion
{
    /// <summary>
    /// Identificador principal de la tabla TipoAprobacion
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el tipo de aprobación
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Muestreo> Muestreo { get; set; } = new List<Muestreo>();
}
