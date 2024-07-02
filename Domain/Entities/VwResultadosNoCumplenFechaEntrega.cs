using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwResultadosNoCumplenFechaEntrega
{
    public long MuestreoId { get; set; }

    public string ClaveMuestreo { get; set; } = null!;

    public DateTime FechaEntrega { get; set; }

    public DateTime? FechaMaxima { get; set; }

    public DateTime? FechaRealVisita { get; set; }

    public string ClaveParametro { get; set; } = null!;
}
