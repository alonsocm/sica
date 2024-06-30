using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwDirectoresResponsablesOc
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Puesto { get; set; } = null!;

    public long Ocid { get; set; }

    public string Oc { get; set; } = null!;

    public string Anio { get; set; } = null!;
}
