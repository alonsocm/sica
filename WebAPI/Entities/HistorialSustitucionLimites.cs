using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class HistorialSustitucionLimites
{
    /// <summary>
    /// Identificador principal del catalogo del historial de limites
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la tabla de Muestreo
    /// </summary>
    public long MuestreoId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de TipoSustitucion
    /// </summary>
    public int TipoSustitucionId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la tabla de Usuario indicando el usuario que esta realizando la sustitución del limite
    /// </summary>
    public long UsuarioId { get; set; }

    /// <summary>
    /// Campo que indica la fecha en la que se esta realizando la sustitución del limite
    /// </summary>
    public DateTime Fecha { get; set; }

    public virtual Muestreo Muestreo { get; set; } = null!;

    public virtual TipoSustitucion TipoSustitucion { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
