using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReglaReporteResultadoTca
{
    /// <summary>
    /// Identificador principal de la tabla ReglaReporteResultadoTCA
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de ReglaReporte
    /// </summary>
    public long ReglaReporteId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de TipoCuerpoAgua
    /// </summary>
    public long TipoCuerpoAguaId { get; set; }

    public virtual ReglasReporte ReglaReporte { get; set; } = null!;

    public virtual TipoCuerpoAgua TipoCuerpoAgua { get; set; } = null!;
}
