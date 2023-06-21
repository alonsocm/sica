using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoSitio
{
    public long Id { get; set; }

    public string? TipoSitio1 { get; set; }

    public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; } = new List<ProgramaSitio>();
}
