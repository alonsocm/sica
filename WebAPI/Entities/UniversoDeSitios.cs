using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UniversoDeSitios
{
    public string? ClaveSitio { get; set; }

    public string? NombreDelSitio { get; set; }

    public string? ClaveSitio1 { get; set; }

    public string? Cuenca { get; set; }

    public string? ClaveAcuífero { get; set; }

    public string? Acuífero { get; set; }

    public string? F7 { get; set; }

    public string? OrganismoCuenca { get; set; }

    public string? DirecciónLocal { get; set; }

    public string? Estado { get; set; }

    public string? Municipio { get; set; }

    public string? CuerpoDeAgua { get; set; }

    public string? TipoDeCuerpoDeAgua { get; set; }

    public string? SubtipoCuerpoAgua { get; set; }

    public double? Latitud { get; set; }

    public double? Longitud { get; set; }
}
