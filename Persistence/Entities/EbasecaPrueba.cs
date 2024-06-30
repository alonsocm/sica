using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EbasecaPrueba
{
    public double? Selecciona { get; set; }

    public double? NoEntrega { get; set; }

    public string? Muestreo { get; set; }

    public string? ClaveConalab { get; set; }

    public string? ClaveConagua { get; set; }

    public DateTime? FechaProgramadaVisita { get; set; }

    public DateTime? FechaRealVisita { get; set; }

    public DateTime? HoraInicioMuestreo { get; set; }

    public DateTime? HoraFinMuestreo { get; set; }

    public string? TipoDeCuerpoDeAgua { get; set; }

    public string? SubtipoCuerpoAgua { get; set; }

    public string? LaboratorioBaseDeDatos { get; set; }

    public string? LaboratorioRealizoMuestreo { get; set; }

    public string? LaboratorioSubrogado { get; set; }

    public string? GrupoDeParametro { get; set; }

    public string? SubgrupoParametro { get; set; }

    public string? ClaveParametro { get; set; }

    public string? Parametro { get; set; }

    public string? UnidadMedida { get; set; }

    public string? Resultado { get; set; }

    public string? Lpclaboratorio { get; set; }

    public string? Ldmlaboratorio { get; set; }

    public string? ObservacionesLaboratorio { get; set; }

    public string? NoVecesLiberadoConagua { get; set; }

    public string? PrecioDeParametro { get; set; }

    public string? ObservacionesConagua { get; set; }

    public string? ObservacionesConagua2 { get; set; }

    public string? ObservacionesConagua3 { get; set; }

    public string? AnioOperacion { get; set; }

    public double? IdResultado { get; set; }

    public DateTime? FechaEntrega { get; set; }
}
