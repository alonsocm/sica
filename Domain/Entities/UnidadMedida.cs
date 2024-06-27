using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UnidadMedida
{
    /// <summary>
    /// Identificador principal de la tabla UnidadMedida
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la unidad de medida
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ParametrosGrupo> ParametrosGrupo { get; set; } = new List<ParametrosGrupo>();
}
