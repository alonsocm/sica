using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EvidenciaSupervisionMuestreo
{
    /// <summary>
    /// Identificador principal de tabla para guardado de evidencias de supervisión 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la tabla de SupervisionMuestreo
    /// </summary>
    public long SupervisionMuestreoId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de tipo de evidencia
    /// </summary>
    public long TipoEvidenciaId { get; set; }

    /// <summary>
    /// Campo que describe el nombre del archivo
    /// </summary>
    public string NombreArchivo { get; set; } = null!;

    public virtual SupervisionMuestreo SupervisionMuestreo { get; set; } = null!;
}
