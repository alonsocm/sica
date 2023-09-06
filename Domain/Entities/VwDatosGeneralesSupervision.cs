using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwDatosGeneralesSupervision
{
    public long Id { get; set; }

    public string OcdlRealiza { get; set; } = null!;

    public string? Laboratorio { get; set; }

    public string NombreSitio { get; set; } = null!;

    public string ClaveMuestreo { get; set; } = null!;

    public DateTime FechaMuestreo { get; set; }

    public string TipoCuerpoAgua { get; set; } = null!;

    public decimal PuntajeObtenido { get; set; }
}
