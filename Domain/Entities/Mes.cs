using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Mes
{
    /// <summary>
    /// Identificador principal de tabla de la tabla Mes
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el mes
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<InformeMensualSupervision> InformeMensualSupervision { get; set; } = new List<InformeMensualSupervision>();
}
