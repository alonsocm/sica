using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EstatusMuestreo
{
    public int Id { get; set; }

    /// <summary>
    /// Campo que indica en que etapa se encuentra el muestreo respecto al flujo
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que india el nombre del estatus como lo solicita el usuario que se muestre
    /// </summary>
    public string Etiqueta { get; set; } = null!;

    public virtual ICollection<Muestreo> Muestreo { get; set; } = new List<Muestreo>();
}
