using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Municipio
{
    public long Id { get; set; }

    public long EstadoId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Localidad> Localidad { get; set; } = new List<Localidad>();

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();
}
