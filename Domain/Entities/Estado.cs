using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Estado
{
    /// <summary>
    /// Identificador de Estado
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Nombre de Estado
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Abreviatura del Estado
    /// </summary>
    public string Abreviatura { get; set; } = null!;

    public virtual ICollection<Localidad> Localidad { get; set; } = new List<Localidad>();

    public virtual ICollection<Municipio> Municipio { get; set; } = new List<Municipio>();

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();
}
