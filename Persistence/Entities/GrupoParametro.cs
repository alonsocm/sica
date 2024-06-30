using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class GrupoParametro
{
    /// <summary>
    /// Identificador principal del catalogo que indica el grupo al que pertenece el parámetro
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el grupo del parámetro
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ParametrosGrupo> ParametrosGrupo { get; set; } = new List<ParametrosGrupo>();
}
