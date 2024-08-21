using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EstatusResultado
{
    public int Id { get; set; }

    /// <summary>
    /// Campo que indica el estatus del resultado del muestreo
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que indica el estatus del resultado del muestreo pero nombrada como lo requiere ver el usuario
    /// </summary>
    public string Etiqueta { get; set; } = null!;

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; } = new List<ResultadoMuestreo>();
}
