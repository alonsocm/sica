using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoSustitucion
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<HistorialSustitucionLimites> HistorialSustitucionLimites { get; set; } = new List<HistorialSustitucionLimites>();
}
