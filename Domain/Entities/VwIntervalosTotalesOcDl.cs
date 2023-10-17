using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwIntervalosTotalesOcDl
{
    public long Ocid { get; set; }

    public long Ocdlid { get; set; }

    public string OrganismoCuencaDireccionLocal { get; set; } = null!;

    public decimal PuntajeObtenido { get; set; }

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int? _50 { get; set; }

    public int? _5160 { get; set; }

    public int? _6170 { get; set; }

    public int? _7180 { get; set; }

    public int? _8185 { get; set; }

    public int? _8690 { get; set; }

    public int? _9195 { get; set; }

    public int? _96100 { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime FehaMuestreo { get; set; }
}
