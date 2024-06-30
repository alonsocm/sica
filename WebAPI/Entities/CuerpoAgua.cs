using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CuerpoAgua
{
    /// <summary>
    /// Identificador prinicpal del catálogo CuerpoAgua
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Descripción Cuerpo Agua
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Estatus de  Cuerpo Agua
    /// </summary>
    public bool Activo { get; set; }

    public virtual ICollection<CuerpoTipoSubtipoAgua> CuerpoTipoSubtipoAgua { get; set; } = new List<CuerpoTipoSubtipoAgua>();
}
