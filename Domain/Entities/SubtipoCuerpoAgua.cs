using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class SubtipoCuerpoAgua
{
    /// <summary>
    /// Identificador principal se la tabla SubtipoCuerpoAgua
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el subtipo cuepo de agua
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Bandera que indica si se encuentra activo el subtipo cuerpo de agua
    /// </summary>
    public bool Activo { get; set; }

    public virtual ICollection<CuerpoTipoSubtipoAgua> CuerpoTipoSubtipoAgua { get; set; } = new List<CuerpoTipoSubtipoAgua>();
}
