using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Acuifero
{
    /// <summary>
    /// Identificador principal del catálogo Acuífero
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el Acuífero
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que describe la clave del acuífero
    /// </summary>
    public int Clave { get; set; }

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();
}
