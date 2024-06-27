using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReglasRelacion
{
    /// <summary>
    /// Identificador principal de la tabla ReglasRelacion
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la clave de la regla
    /// </summary>
    public string ClaveRegla { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace referencia al catálogo ClasificacionRegla
    /// </summary>
    public long ClasificacionReglaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo TipoRegla
    /// </summary>
    public long TipoReglaId { get; set; }

    /// <summary>
    /// Campo que describe la fórmula de la regla
    /// </summary>
    public string RelacionRegla { get; set; } = null!;

    /// <summary>
    /// Campo que describe a leyenda que se mostrará en caso de que no se cumpla la regla
    /// </summary>
    public string Leyenda { get; set; } = null!;

    public virtual ICollection<ReglasRelacionParametro> ReglasRelacionParametro { get; set; } = new List<ReglasRelacionParametro>();
}
