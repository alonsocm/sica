using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParametrosSitioTipoCuerpoAgua
{
    /// <summary>
    /// Identificador principal de tabla que hace referencia a los parámetros por sitio y tipo cuerpo de agua
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo TipoSitio
    /// </summary>
    public long TipoSitioId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de TipoCuerpoAgua
    /// </summary>
    public long TipoCuerpoAguaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de  ParametrosGrpo
    /// </summary>
    public long ParametroId { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual TipoCuerpoAgua TipoCuerpoAgua { get; set; } = null!;

    public virtual TipoSitio TipoSitio { get; set; } = null!;
}
