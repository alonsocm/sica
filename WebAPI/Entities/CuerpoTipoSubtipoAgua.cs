using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CuerpoTipoSubtipoAgua
{
    /// <summary>
    /// Identificador de Cuerpo Tipo Subtipo Agua
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de CuerpoAgua
    /// </summary>
    public long CuerpoAguaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de TipoCuerpoAgua
    /// </summary>
    public long TipoCuerpoAguaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de SubtipoCuerpoAgua
    /// </summary>
    public long SubtipoCuerpoAguaId { get; set; }

    public virtual CuerpoAgua CuerpoAgua { get; set; } = null!;

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();

    public virtual SubtipoCuerpoAgua SubtipoCuerpoAgua { get; set; } = null!;

    public virtual TipoCuerpoAgua TipoCuerpoAgua { get; set; } = null!;
}
