using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CuencaDireccionesLocales
{
    /// <summary>
    /// Identificador principal de catálogo CuencaDireccionesLocales donde describe la relación entre las Cuencas y Direcciones Locales
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Organismos de Cuenca
    /// </summary>
    public long OcuencaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Direccones Locales
    /// </summary>
    public long? DlocalId { get; set; }

    /// <summary>
    /// Campo que indica si se encuentra activo el registro
    /// </summary>
    public bool Activo { get; set; }

    public virtual DireccionLocal? Dlocal { get; set; }

    public virtual OrganismoCuenca Ocuenca { get; set; } = null!;

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();

    public virtual ICollection<SupervisionMuestreo> SupervisionMuestreo { get; set; } = new List<SupervisionMuestreo>();
}
