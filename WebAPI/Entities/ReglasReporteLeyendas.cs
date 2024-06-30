using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReglasReporteLeyendas
{
    /// <summary>
    /// Identificador principal de la tabla ReglasReporteLeyendas
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la leyenda de la regla de reporte
    /// </summary>
    public string ReglaReporte { get; set; } = null!;

    /// <summary>
    /// Campo que hace referencia a la leyenda
    /// </summary>
    public string Leyenda { get; set; } = null!;
}
