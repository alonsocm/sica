using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CriteriosSupervisionMuestreo
{
    /// <summary>
    /// Identificador principal de catálogo de criterios de supervisión de muestreo
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descripción del criterio de muestreo
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que indica si el criterio es obligatorio
    /// </summary>
    public bool Obligatorio { get; set; }

    /// <summary>
    /// Campo que indica el valor del criterio
    /// </summary>
    public decimal Valor { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de ClasificacionCriterios
    /// </summary>
    public int ClasificacionCriterioId { get; set; }

    /// <summary>
    /// Campo que describe si puede ser un criterio con observación &quot;no aplica&quot;
    /// </summary>
    public bool EsExcepcionNoAplica { get; set; }

    public virtual ClasificacionCriterio ClasificacionCriterio { get; set; } = null!;
}
