using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoCuerpoAgua
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public long? TipoHomologadoId { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<CuerpoTipoSubtipoAgua> CuerpoTipoSubtipoAgua { get; set; } = new List<CuerpoTipoSubtipoAgua>();

    public virtual ICollection<ParametrosSitioTipoCuerpoAgua> ParametrosSitioTipoCuerpoAgua { get; set; } = new List<ParametrosSitioTipoCuerpoAgua>();

    public virtual ICollection<ReglaReporteResultadoTca> ReglaReporteResultadoTca { get; set; } = new List<ReglaReporteResultadoTca>();

    public virtual TipoHomologado? TipoHomologado { get; set; }
}
