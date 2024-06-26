using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class AprobacionResultadoMuestreo
{
    /// <summary>
    /// Identificador principal de tabla donde se guardara la aprobación de resultado de muestreo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Estatus de aprobación de resultados
    /// </summary>
    public bool ApruebaResultado { get; set; }

    /// <summary>
    /// Comentarios porque no fue aprobado
    /// </summary>
    public string ComentariosAprobacionResultados { get; set; } = null!;

    /// <summary>
    /// Fecha de aprobación o rechazo
    /// </summary>
    public DateTime FechaAprobRechazo { get; set; }

    /// <summary>
    /// Identificador de llave foránea de usuario que realizo la aprobación/rechazo
    /// </summary>
    public long UsuarioRevisionId { get; set; }

    /// <summary>
    /// Identificador de llave foránea que hace referencia a la tabla de ResultadoMuestreo
    /// </summary>
    public long ResultadoMuestreoId { get; set; }

    public virtual ResultadoMuestreo ResultadoMuestreo { get; set; } = null!;

    public virtual Usuario UsuarioRevision { get; set; } = null!;
}
