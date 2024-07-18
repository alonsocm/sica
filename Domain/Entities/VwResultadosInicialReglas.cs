﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwResultadosInicialReglas
{
    public long MuestreoId { get; set; }

    public DateTime? FechaRealizacion { get; set; }

    public int? AnioOperacion { get; set; }

    public string? NumeroCarga { get; set; }

    public int EstatusId { get; set; }

    public bool AutorizacionIncompleto { get; set; }

    public bool AutorizacionFechaEntrega { get; set; }

    public string ClaveSitio { get; set; } = null!;

    public string ClaveMuestreo { get; set; } = null!;

    public string NombreSitio { get; set; } = null!;

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

    public string MuestreoCompletoPorResultados { get; set; } = null!;

    public string? CumpleConLasReglasCondicionantes { get; set; }

    public int? NumFechasNoCumplidas { get; set; }

    public int? PorcentajePago { get; set; }

    public string? UsuarioValido { get; set; }
}
