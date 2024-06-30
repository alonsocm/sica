using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParametrosReglasNoRelacion
{
    /// <summary>
    /// Identificador de llave primaria de la tabla de ParametrosReglasNORelacion
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de ParametrosGrupo indicando el parametro que tiene regla de no relación
    /// 
    /// </summary>
    public long ParametroId { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;
}
