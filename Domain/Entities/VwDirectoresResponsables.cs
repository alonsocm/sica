using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwDirectoresResponsables
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Puesto { get; set; } = null!;

    public string OcDl { get; set; } = null!;

    public string Anio { get; set; } = null!;
}
