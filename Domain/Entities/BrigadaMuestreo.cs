using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BrigadaMuestreo
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public long? LaboratorioId { get; set; }

    public string? Lider { get; set; }

    public string? Placas { get; set; }

    public virtual ICollection<AvisoRealizacion> AvisoRealizacion { get; set; } = new List<AvisoRealizacion>();

    public virtual Laboratorios? Laboratorio { get; set; }

    public virtual ICollection<ProgramaMuestreo> ProgramaMuestreo { get; set; } = new List<ProgramaMuestreo>();
}
