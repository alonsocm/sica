using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EstatusMuestreo1
{
    /// <summary>
    /// Identificador de Estatus Muestreo
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el estatus del muestreo
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; } = new List<ResultadoMuestreo>();
}
