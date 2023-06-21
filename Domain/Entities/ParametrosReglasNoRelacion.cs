using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParametrosReglasNoRelacion
{
    public long Id { get; set; }

    public long ParametroId { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;
}
