using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class HistorialSustitucionEmergencia
{
    /// <summary>
    /// Identificador principal de la tabla HistorialSustitucionEmergencia
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la tabla de MuestreoEmergencia
    /// </summary>
    public long MuestreoEmergenciaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la tabla de Usuario indicando el usuario que realizo la sustitución
    /// </summary>
    public long UsuarioId { get; set; }

    /// <summary>
    /// Campo que describe el año
    /// </summary>
    public long Anio { get; set; }

    /// <summary>
    /// Campo que describe la fecha
    /// </summary>
    public DateTime Fecha { get; set; }

    public virtual MuestreoEmergencia MuestreoEmergencia { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
