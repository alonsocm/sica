using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwValidacionEviencias
{
    public long AvisoRealizacionId { get; set; }

    public long MuestreoId { get; set; }

    public string ClaveMuestreo { get; set; } = null!;

    public string Sitio { get; set; } = null!;

    public string ClaveConalab { get; set; } = null!;

    public string TipoCuerpoAgua { get; set; } = null!;

    public string? Laboratorio { get; set; }

    public bool ConEventualidades { get; set; }

    public DateTime FechaProgramadaVisita { get; set; }

    public DateTime? FechaRealVisita { get; set; }

    public string BrigadaProgramaMuestreo { get; set; } = null!;

    public bool ConQcmuestreo { get; set; }

    public string TipoSupervision { get; set; } = null!;

    public string? TipoEventualidad { get; set; }

    public DateTime? FechaReprogramacion { get; set; }

    public int? EvidenciasEsperadas { get; set; }

    public int? TotalEvidencias { get; set; }

    public string CumpleEvidencias { get; set; } = null!;

    public DateTime FechaRealizacion { get; set; }

    public string CumpleFechaRealizacion { get; set; } = null!;

    public TimeSpan? HoraIncioMuestreo { get; set; }

    public TimeSpan? HoraFinMuestreo { get; set; }

    public int? TiempoMinimoMuestreo { get; set; }

    public int? Calculo { get; set; }

    public string CumpleTiempoMuestreo { get; set; } = null!;

    public string ClaveConalbaArm { get; set; } = null!;

    public string CumpleClaveConalab { get; set; } = null!;

    public string ClaveMuestreoArm { get; set; } = null!;

    public string CumpleClaveMuestreo { get; set; } = null!;

    public string? LiderBrigadaArm { get; set; }

    public string? LiderBrigadaBase { get; set; }

    public string ClaveBrigadaArm { get; set; } = null!;

    public string CumpleClaveBrigada { get; set; } = null!;

    public string? PlacasDeMuestreo { get; set; }

    public double Lat1MuestreoPrograma { get; set; }

    public double Log1MuestreoPrograma { get; set; }

    public string? LatSitioResultado { get; set; }

    public string? LongSitioResultado { get; set; }
}
