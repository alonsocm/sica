using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MuestreoEmergencia
{
    public long Id { get; set; }

    public string? Numero { get; set; }

    public string NombreEmergencia { get; set; } = null!;

    public string ClaveUnica { get; set; } = null!;

    public string IdLaboratorio { get; set; } = null!;

    public string Sitio { get; set; } = null!;

    public DateTime FechaProgramada { get; set; }

    public DateTime FechaRealVisita { get; set; }

    public TimeSpan HoraMuestreo { get; set; }

    public string TipoCuerpoAgua { get; set; } = null!;

    public string SubtipoCuerpoAgua { get; set; } = null!;

    public string LaboratorioRealizoMuestreo { get; set; } = null!;

    public string? LaboratorioSubrogado { get; set; }

    public string GrupoParametro { get; set; } = null!;

    public string ClaveParametro { get; set; } = null!;

    public string Parametro { get; set; } = null!;

    public string Resultado { get; set; } = null!;

    public string? UnidadMedida { get; set; }
}
