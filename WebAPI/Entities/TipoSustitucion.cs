using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoSustitucion
{
    /// <summary>
    /// Identificador principal de la tabla TipoSustitucion
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el tipo de sustitución
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<HistorialSustitucionLimites> HistorialSustitucionLimites { get; set; } = new List<HistorialSustitucionLimites>();
}
