using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwEstatusMuestreosAdministracion
{
    public int? TotalMuestreo { get; set; }

    public int? TotalResultados { get; set; }

    public string Etapa { get; set; } = null!;
}
