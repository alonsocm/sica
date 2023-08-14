using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MuestreoEmergencia
{
    public long Id { get; set; }

    public string? Numero { get; set; }

    public string NombreEmergencia { get; set; } = null!;

    public string ClaveUnica { get; set; } = null!;

    public string? IdLaboratorio { get; set; }

    public string Sitio { get; set; } = null!;

    public DateTime FechaProgramada { get; set; }

    public DateTime FechaRealVisita { get; set; }

    public string? HoraMuestreo { get; set; }

    public string TipoCuerpoAgua { get; set; } = null!;

    public string? SubtipoCuerpoAgua { get; set; }

    public string LaboratorioRealizoMuestreo { get; set; } = null!;

    public string? LaboratorioSubrogado { get; set; }

    public long ParametroId { get; set; }

    public string Resultado { get; set; } = null!;

    public string? ResultadoSustituidoPorLimite { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;
}
