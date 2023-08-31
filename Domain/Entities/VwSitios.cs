using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwSitios
{
    public long SitioId { get; set; }

    public string ClaveMuestreo { get; set; } = null!;

    public string ClaveSitio { get; set; } = null!;

    public string NombreSitio { get; set; } = null!;

    public long CuencaDireccionesLocalesId { get; set; }

    public double Latitud { get; set; }

    public double Longitud { get; set; }

    public string TipoCuerpoAgua { get; set; } = null!;
}
