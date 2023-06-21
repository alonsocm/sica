using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReglasRelacion
{
    public long Id { get; set; }

    public string ClaveRegla { get; set; } = null!;

    public long ClasificacionReglaId { get; set; }

    public long TipoReglaId { get; set; }

    public string RelacionRegla { get; set; } = null!;

    public string Leyenda { get; set; } = null!;

    public virtual ICollection<ReglasRelacionParametro> ReglasRelacionParametro { get; set; } = new List<ReglasRelacionParametro>();
}
