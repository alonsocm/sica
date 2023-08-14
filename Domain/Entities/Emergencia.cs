using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Emergencia
{
    public int Id { get; set; }

    public int Anio { get; set; }

    public string NombreEmergencia { get; set; } = null!;

    public string? ClaveSitio { get; set; }

    public string NombreSitio { get; set; } = null!;

    public string? Cuenca { get; set; }

    public string? OrganismoCuenca { get; set; }

    public string? Estado { get; set; }

    public string? ClaveMunicipio { get; set; }

    public string? Municipio { get; set; }

    public string? CuerpoAgua { get; set; }

    public string? TipoCuerpoAgua { get; set; }

    public string? SubTipoCuerpoAgua { get; set; }

    public string Latitud { get; set; } = null!;

    public string Longitud { get; set; } = null!;

    public DateTime? FechaRealizacion { get; set; }
}
