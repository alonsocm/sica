using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProgramaMuestreo
{
    /// <summary>
    /// Identificador prinicpal de la tabla ProgramaMuestreo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la tabla ProgramaSitio
    /// </summary>
    public long ProgramaSitioId { get; set; }

    /// <summary>
    /// Campo describe la fecha del dia programado
    /// </summary>
    public DateTime DiaProgramado { get; set; }

    /// <summary>
    /// Campo que describe el número de la semana programada
    /// </summary>
    public int SemanaProgramada { get; set; }

    /// <summary>
    /// Campo que describe el número del domingo de semana programada
    /// </summary>
    public DateTime DomingoSemanaProgramada { get; set; }

    /// <summary>
    /// Campo que indica el nombre correcto del archivo
    /// </summary>
    public string NombreCorrectoArchivo { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de BrigadaMuestreo
    /// </summary>
    public long BrigadaMuestreoId { get; set; }

    public virtual BrigadaMuestreo BrigadaMuestreo { get; set; } = null!;

    public virtual ICollection<Muestreo> Muestreo { get; set; } = new List<Muestreo>();

    public virtual ProgramaSitio ProgramaSitio { get; set; } = null!;
}
