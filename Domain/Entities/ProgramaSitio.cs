using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProgramaSitio
{
    public long Id { get; set; }

    public long TipoSitioId { get; set; }

    public long SitioId { get; set; }

    public long? LaboratorioId { get; set; }

    public int NumMuestreosRealizarAnio { get; set; }

    public string? Observaciones { get; set; }

    public long ProgramaAnioId { get; set; }

    public virtual Laboratorios? Laboratorio { get; set; }

    public virtual ProgramaAnio ProgramaAnio { get; set; } = null!;

    public virtual ICollection<ProgramaMuestreo> ProgramaMuestreo { get; set; } = new List<ProgramaMuestreo>();

    public virtual Sitio Sitio { get; set; } = null!;

    public virtual TipoSitio TipoSitio { get; set; } = null!;
}
