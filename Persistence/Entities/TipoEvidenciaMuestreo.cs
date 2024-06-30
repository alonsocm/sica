using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoEvidenciaMuestreo
{
    /// <summary>
    /// Identificador principal de la tabla TipoEvidenciaMuestreo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el tipo de evidencia del muestreo
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que describe el sufijo 
    /// </summary>
    public string Sufijo { get; set; } = null!;

    /// <summary>
    /// Campo que describe la extensión de la evidencia
    /// </summary>
    public string Extension { get; set; } = null!;

    public virtual ICollection<EvidenciaMuestreo> EvidenciaMuestreo { get; set; } = new List<EvidenciaMuestreo>();
}
