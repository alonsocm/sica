using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class OrganismoCuenca
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public virtual ICollection<CuencaDireccionesLocales> CuencaDireccionesLocales { get; set; } = new List<CuencaDireccionesLocales>();

    public virtual ICollection<Directorio> Directorio { get; set; } = new List<Directorio>();

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();

    public virtual ICollection<SupervisionMuestreo> SupervisionMuestreo { get; set; } = new List<SupervisionMuestreo>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
