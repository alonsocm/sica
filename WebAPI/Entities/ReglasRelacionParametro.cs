using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReglasRelacionParametro
{
    /// <summary>
    /// Identificador principal de la tabla ReglasRelacionParametro
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al  catalogo ReglasRelacion
    /// </summary>
    public long ReglasRelacionId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo ParametrosGrupo
    /// </summary>
    public long ParametroId { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual ReglasRelacion ReglasRelacion { get; set; } = null!;
}
