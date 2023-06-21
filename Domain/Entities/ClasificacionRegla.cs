using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ClasificacionRegla
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; } = new List<ReglasMinimoMaximo>();

    public virtual ICollection<ReglasReporte> ReglasReporte { get; set; } = new List<ReglasReporte>();
}
