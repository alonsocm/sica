using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Puestos
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Directorio> Directorio { get; set; } = new List<Directorio>();
}
