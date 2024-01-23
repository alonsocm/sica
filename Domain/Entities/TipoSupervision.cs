using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoSupervision
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<AvisoRealizacion> AvisoRealizacion { get; set; } = new List<AvisoRealizacion>();
}
