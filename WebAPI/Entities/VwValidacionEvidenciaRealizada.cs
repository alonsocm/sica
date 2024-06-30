using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwValidacionEvidenciaRealizada
{
    public long ValidacionEvidenciaId { get; set; }

    public string ClaveMuestreo { get; set; } = null!;

    public string ClaveSitio { get; set; } = null!;

    public string TipoCuerpoAgua { get; set; } = null!;

    public string? Laboratorio { get; set; }

    public string? LaboratorioMuestreo { get; set; }

    public bool ConEventualidades { get; set; }

    public DateTime FechaProgramada { get; set; }

    public DateTime FechaRealVisita { get; set; }

    public string Brigada { get; set; } = null!;

    public bool ConQcmuestreo { get; set; }

    public string TipoSupervision { get; set; } = null!;

    public string? TipoEventualidad { get; set; }

    public DateTime? FechaReprogramacion { get; set; }

    public DateTime FechaValidacion { get; set; }

    public int PorcentajePago { get; set; }

    public bool Rechazo { get; set; }
}
