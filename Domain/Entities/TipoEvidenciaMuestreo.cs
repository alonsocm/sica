using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoEvidenciaMuestreo
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Sufijo { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public virtual ICollection<EvidenciaMuestreo> EvidenciaMuestreo { get; set; } = new List<EvidenciaMuestreo>();

    public virtual ICollection<EvidenciaSupervisionMuestreo> EvidenciaSupervisionMuestreo { get; set; } = new List<EvidenciaSupervisionMuestreo>();
}
