using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoRegla
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; } = new List<ReglasMinimoMaximo>();
}
