using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Observaciones
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreoObservacionSrenamecaNavigation { get; set; } = new List<ResultadoMuestreo>();

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreoObservacionesOcdlNavigation { get; set; } = new List<ResultadoMuestreo>();

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreoObservacionesSecaiaNavigation { get; set; } = new List<ResultadoMuestreo>();
}
