using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BrigadaMuestreo
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<ProgramaMuestreo> ProgramaMuestreo { get; set; } = new List<ProgramaMuestreo>();
}
