using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Observaciones
{
    /// <summary>
    /// Identificador principal del catálogo de Observaciones
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la observación
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que indica si se encuentra activa la observación
    /// </summary>
    public bool Activo { get; set; }

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreoObservacionSrenamecaNavigation { get; set; } = new List<ResultadoMuestreo>();

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreoObservacionesOcdlNavigation { get; set; } = new List<ResultadoMuestreo>();

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreoObservacionesSecaiaNavigation { get; set; } = new List<ResultadoMuestreo>();
}
