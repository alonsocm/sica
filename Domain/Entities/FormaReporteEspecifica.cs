using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class FormaReporteEspecifica
{
    public long Id { get; set; }

    public long ParametroId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ParametrosGrupo Parametro { get; set; } = null!;
}
