using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwResultadosInicialReglas
{
    public string ClaveSitio { get; set; } = null!;

    public string ClaveMuestreo { get; set; } = null!;

    public string NombreSitio { get; set; } = null!;

    public DateTime? FechaRealizacion { get; set; }

    public DateTime FechaProgramada { get; set; }

    public int? DiferenciaEnDias { get; set; }

    public DateTime? FechaEntregaTeorica { get; set; }

    public string? Laboratorio { get; set; }

    public string CuerpoDeAgua { get; set; } = null!;

    public string TipoCuerpoAgua { get; set; } = null!;

    public string SubtipoCuerpoDeAgua { get; set; } = null!;

    public long TipoSitioId { get; set; }

    public long TipoCuerpoAguaId { get; set; }

    public int? NumDatosEsperados { get; set; }

    public int? NumDatosReportados { get; set; }

    public string? MuestreoCompletoPorResultados { get; set; }

    public int AnioOperacion { get; set; }
    public int NumeroEntrega { get; set; }
    public long MuestreoId { get; set; }    
    public int EstatusId { get; set; }
}
