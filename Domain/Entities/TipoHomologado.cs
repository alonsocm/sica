using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoHomologado
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<TipoCuerpoAgua> TipoCuerpoAgua { get; set; } = new List<TipoCuerpoAgua>();
}
