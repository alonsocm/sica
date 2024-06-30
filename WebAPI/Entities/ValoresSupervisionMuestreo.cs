using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ValoresSupervisionMuestreo
{
    /// <summary>
    /// Identificador principal de la tabla ValoresSupervisionMuestreo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de CriteriosSupervision
    /// </summary>
    public int CriterioSupervisionId { get; set; }

    /// <summary>
    /// Campo que describe el resultado
    /// </summary>
    public string Resultado { get; set; } = null!;

    /// <summary>
    /// Campo que describe las observaciones del criterio
    /// </summary>
    public string? ObservacionesCriterio { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al atabla de SupervisiónMuestreo
    /// </summary>
    public long SupervisionMuestreoId { get; set; }

    public virtual SupervisionMuestreo SupervisionMuestreo { get; set; } = null!;
}
