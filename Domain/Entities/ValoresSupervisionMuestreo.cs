using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ValoresSupervisionMuestreo
{
    public long Id { get; set; }

    public int CriterioSupervisionId { get; set; }

    public string? ObservacionesCriterio { get; set; }

    public long SupervisionMuestreoId { get; set; }

    public string Resultado { get; set; } = null!;

    public virtual SupervisionMuestreo SupervisionMuestreo { get; set; } = null!;
}
