using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class LimiteParametroLaboratorio
{
    public long Id { get; set; }

    public long ParametroId { get; set; }

    public long LaboratorioId { get; set; }

    public int? RealizaLaboratorioMuestreoId { get; set; }

    public long? LaboratorioMuestreoId { get; set; }

    public int? Periodo { get; set; }

    public bool Activo { get; set; }

    public string? LdmaCumplir { get; set; }

    public string? LpcaCumplir { get; set; }

    public bool? LoMuestra { get; set; }

    public int? LoSubrogaId { get; set; }

    public long? LaboratorioSubrogaId { get; set; }

    public string? MetodoAnalitico { get; set; }

    public string? Ldm { get; set; }

    public string? Lpc { get; set; }

    public long AnioId { get; set; }

    public virtual ProgramaAnio Anio { get; set; } = null!;

    public virtual Laboratorios Laboratorio { get; set; } = null!;

    public virtual Laboratorios? LaboratorioMuestreo { get; set; }

    public virtual Laboratorios? LaboratorioSubroga { get; set; }

    public virtual AccionLaboratorio? LoSubroga { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual AccionLaboratorio? RealizaLaboratorioMuestreo { get; set; }
}
