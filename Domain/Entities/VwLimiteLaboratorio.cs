using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwLimiteLaboratorio
{
    public long ParametroId { get; set; }

    public string? Limite { get; set; }

    public long LaboratorioId { get; set; }

    public long? LaboratorioMuestreoId { get; set; }

    public int? Periodo { get; set; }

    public bool Activo { get; set; }

    public long? LaboratorioSubrogaId { get; set; }

    public string? Ldm { get; set; }

    public string? Lpc { get; set; }

    public bool? EsLdm { get; set; }

    public string Anio { get; set; } = null!;
}
