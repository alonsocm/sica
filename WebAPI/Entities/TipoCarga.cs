using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoCarga
{
    /// <summary>
    /// Identificador principal del catalogo TipoCarga
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descripcion del tipo de carga de archivo ebaseca
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Muestreo> Muestreo { get; set; } = new List<Muestreo>();
}
