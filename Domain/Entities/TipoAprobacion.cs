using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoAprobacion
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Muestreo> Muestreo { get; set; } = new List<Muestreo>();
}
