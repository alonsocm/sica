using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParametrosCostos
{
    public long Id { get; set; }

    public long ParametroId { get; set; }

    public long ProgramaAnioId { get; set; }

    public decimal Precio { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual ProgramaAnio ProgramaAnio { get; set; } = null!;
}
