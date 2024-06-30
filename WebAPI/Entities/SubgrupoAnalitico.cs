using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class SubgrupoAnalitico
{
    /// <summary>
    /// Identificador principal se la tabla SubGrupoAnalitico
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el subgrupo analítico
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ParametrosGrupo> ParametrosGrupo { get; set; } = new List<ParametrosGrupo>();
}
