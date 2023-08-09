using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwClaveMuestreo
{
    public long ProgramaMuestreoId { get; set; }

    public string ClaveMuestreo { get; set; } = null!;

    public int Cargado { get; set; }
}
