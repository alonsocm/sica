using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EvidenciaReplica
{
    /// <summary>
    /// Identificador principal de tabla EvidenciaReplica
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la tabla de  ResultadoMuestreo
    /// </summary>
    public long ResultadoMuestreoId { get; set; }

    /// <summary>
    /// Campo de nombre de archivo
    /// </summary>
    public string NombreArchivo { get; set; } = null!;

    /// <summary>
    /// Campo que indica la clave única
    /// </summary>
    public string ClaveUnica { get; set; } = null!;

    /// <summary>
    /// Campo que describe el archivo
    /// </summary>
    public byte[]? Archivo { get; set; }

    public virtual ResultadoMuestreo ResultadoMuestreo { get; set; } = null!;
}
