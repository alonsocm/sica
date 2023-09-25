using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Mes
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<InformeMensualSupervision> InformeMensualSupervision { get; set; } = new List<InformeMensualSupervision>();
}
