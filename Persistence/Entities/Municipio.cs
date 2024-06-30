using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Municipio
{
    /// <summary>
    /// Identificador del catálogo de Municipio
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Estado indicando a que estado pertenece el municipio
    /// </summary>
    public long EstadoId { get; set; }

    /// <summary>
    /// Campo que describe el municipio
    /// </summary>
    public string Nombre { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Localidad> Localidad { get; set; } = new List<Localidad>();

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();
}
