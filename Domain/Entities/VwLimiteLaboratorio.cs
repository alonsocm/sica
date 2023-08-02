using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwLimiteLaboratorio
{
    public long ParametroId { get; set; }

    public string? Limite { get; set; }

    public long LaboratorioId { get; set; }

    public string? Descripcion { get; set; }

    public string? Nomenclatura { get; set; }

    public long? LaboratorioSubrogaId { get; set; }

    public string Anio { get; set; } = null!;
}
