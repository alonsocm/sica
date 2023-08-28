using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ClasificacionCriterio
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<CriteriosSupervisionMuestreo> CriteriosSupervisionMuestreo { get; set; } = new List<CriteriosSupervisionMuestreo>();
}
