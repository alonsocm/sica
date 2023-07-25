using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Laboratorios
{
    public long Id { get; set; }

    public string? Descripcion { get; set; }

    public string? Nomenclatura { get; set; }

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorioLaboratorio { get; set; } = new List<LimiteParametroLaboratorio>();

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorioLaboratorioMuestreo { get; set; } = new List<LimiteParametroLaboratorio>();

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorioLaboratorioSubroga { get; set; } = new List<LimiteParametroLaboratorio>();

    public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; } = new List<ProgramaSitio>();

    public virtual ICollection<ReglasLaboratorioLdmLpc> ReglasLaboratorioLdmLpc { get; set; } = new List<ReglasLaboratorioLdmLpc>();

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; } = new List<ResultadoMuestreo>();
}
