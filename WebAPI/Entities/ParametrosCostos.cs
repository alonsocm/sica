using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParametrosCostos
{
    /// <summary>
    /// Identificador principal del catálogo ParametrosCostos
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de ParametrosGrupo indicando el parámetro
    /// </summary>
    public long ParametroId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia la catálogo de ProgramaAnio indicando el año al que pertenece el costo de este parámetro.
    /// </summary>
    public long ProgramaAnioId { get; set; }

    /// <summary>
    /// Campo que describe el precio del parámetro
    /// </summary>
    public decimal Precio { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual ProgramaAnio ProgramaAnio { get; set; } = null!;
}
