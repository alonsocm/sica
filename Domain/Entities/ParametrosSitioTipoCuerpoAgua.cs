using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParametrosSitioTipoCuerpoAgua
{
    public long Id { get; set; }

    public long TipoSitioId { get; set; }

    public long TipoCuerpoAguaId { get; set; }

    public long ParametroId { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual TipoCuerpoAgua TipoCuerpoAgua { get; set; } = null!;

    public virtual TipoSitio TipoSitio { get; set; } = null!;
}
