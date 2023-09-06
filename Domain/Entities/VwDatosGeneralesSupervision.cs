using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwDatosGeneralesSupervision
{
    public long Id { get; set; }

    public string OcDlRealizaLaSupervision { get; set; } = null!;

    public string? LaboratorioQueRealizaMuestreo { get; set; }

    public string Sitio { get; set; } = null!;

    public string ClaveDelMuestreo { get; set; } = null!;

    public DateTime FechaDeMuestreo { get; set; }

    public string TipoCuerpoDeAgua { get; set; } = null!;

    public decimal PuntajeObtenido { get; set; }
}
