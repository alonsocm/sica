using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReglasReporteLeyendas
{
    public long Id { get; set; }

    public string ReglaReporte { get; set; } = null!;

    public string Leyenda { get; set; } = null!;
}
