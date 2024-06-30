using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoHomologado
{
    /// <summary>
    /// Identificador principal de la tabla TipoHomologado
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el tipo homologado
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Camo que describe si se enceuntra activo el tipo homologado
    /// </summary>
    public bool Activo { get; set; }

    public virtual ICollection<TipoCuerpoAgua> TipoCuerpoAgua { get; set; } = new List<TipoCuerpoAgua>();
}
