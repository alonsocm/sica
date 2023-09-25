using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CopiaInformeMensualSupervision
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Puesto { get; set; } = null!;

    public long InformeMensualSupervisionId { get; set; }

    public virtual InformeMensualSupervision InformeMensualSupervision { get; set; } = null!;
}
