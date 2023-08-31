using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DireccionLocal
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public virtual ICollection<CuencaDireccionesLocales> CuencaDireccionesLocales { get; set; } = new List<CuencaDireccionesLocales>();

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
