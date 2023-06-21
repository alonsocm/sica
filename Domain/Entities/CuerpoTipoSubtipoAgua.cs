using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CuerpoTipoSubtipoAgua
{
    public long Id { get; set; }

    public long CuerpoAguaId { get; set; }

    public long TipoCuerpoAguaId { get; set; }

    public long SubtipoCuerpoAguaId { get; set; }

    public virtual CuerpoAgua CuerpoAgua { get; set; } = null!;

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();

    public virtual SubtipoCuerpoAgua SubtipoCuerpoAgua { get; set; } = null!;

    public virtual TipoCuerpoAgua TipoCuerpoAgua { get; set; } = null!;
}
