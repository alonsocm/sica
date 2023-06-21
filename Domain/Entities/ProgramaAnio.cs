using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProgramaAnio
{
    public long Id { get; set; }

    public string Anio { get; set; } = null!;

    public virtual ICollection<ParametrosCostos> ParametrosCostos { get; set; } = new List<ParametrosCostos>();

    public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; } = new List<ProgramaSitio>();
}
