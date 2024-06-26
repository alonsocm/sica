using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ClasificacionCriterio
{
    /// <summary>
    /// Identificador principal de catálogo de clasificaciones de criterio
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descripción de la clasificación del criterio
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<CriteriosSupervisionMuestreo> CriteriosSupervisionMuestreo { get; set; } = new List<CriteriosSupervisionMuestreo>();
}
