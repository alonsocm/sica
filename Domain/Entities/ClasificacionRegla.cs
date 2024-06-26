using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ClasificacionRegla
{
    /// <summary>
    /// Identificador principal de catálogo ClasificacionRegla
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Descripción de la clasificación de regla
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; } = new List<ReglasMinimoMaximo>();

    public virtual ICollection<ReglasReporte> ReglasReporte { get; set; } = new List<ReglasReporte>();
}
