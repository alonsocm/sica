using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DireccionLocal
{
    /// <summary>
    /// Identificador principal de tabla Dirección Local
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Descripción de Dirección Local
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que indica la clave de la Dirección Local
    /// </summary>
    public string Clave { get; set; } = null!;

    public virtual ICollection<CuencaDireccionesLocales> CuencaDireccionesLocales { get; set; } = new List<CuencaDireccionesLocales>();

    public virtual ICollection<Directorio> Directorio { get; set; } = new List<Directorio>();

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
