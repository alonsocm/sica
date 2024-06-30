using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwValidacionEvidenciaTotales
{
    public string? Nomenclatura { get; set; }

    public int? MuestreosTotales { get; set; }

    public int? MuestreosAprobados { get; set; }

    public int? MuestreosRechazados { get; set; }
}
