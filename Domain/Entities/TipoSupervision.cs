using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoSupervision
{
    /// <summary>
    /// Identificador principal de tabla
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el tipo de supervisión
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<AvisoRealizacion> AvisoRealizacion { get; set; } = new List<AvisoRealizacion>();
}
