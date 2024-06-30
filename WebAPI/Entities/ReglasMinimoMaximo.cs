using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReglasMinimoMaximo
{
    /// <summary>
    /// Identificador principal de la tabla ReglasMinimoMaximo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la clave de la regla
    /// 
    /// </summary>
    public string ClaveRegla { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace referencia al catálogo ClasificaciónRegla
    /// </summary>
    public long ClasificacionReglaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo TipoRegla
    /// </summary>
    public long TipoReglaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo ParametrosGrupo
    /// </summary>
    public long ParametroId { get; set; }

    /// <summary>
    /// Campo que describe si aplica la regla
    /// </summary>
    public bool Aplica { get; set; }

    /// <summary>
    /// Campo que describe el mínimo máximo
    /// </summary>
    public string MinimoMaximo { get; set; } = null!;

    /// <summary>
    /// Campo que describe la leyenda que se mostrara en caso de que no se cumpla la regla
    /// </summary>
    public string Leyenda { get; set; } = null!;

    public virtual ClasificacionRegla ClasificacionRegla { get; set; } = null!;

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual TipoRegla TipoRegla { get; set; } = null!;
}
