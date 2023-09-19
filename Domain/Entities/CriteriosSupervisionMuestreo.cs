using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CriteriosSupervisionMuestreo
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Obligatorio { get; set; }

    public decimal Valor { get; set; }

    public int ClasificacionCriterioId { get; set; }

    public bool EsExcepcionNoAplica { get; set; }

    public virtual ClasificacionCriterio ClasificacionCriterio { get; set; } = null!;
}
