using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Localidad
{
    /// <summary>
    /// Identificador prncipal del catálogo Localidad
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Estado indicand el estado de la localidad
    /// </summary>
    public long EstadoId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Municipio indicando el municipio de la localidad
    /// </summary>
    public long MunicipioId { get; set; }

    /// <summary>
    /// Campo que describe la Localidad
    /// </summary>
    public string Nombre { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual Municipio Municipio { get; set; } = null!;
}
