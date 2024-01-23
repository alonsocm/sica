using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Acuifero
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Clave { get; set; }

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();
}
