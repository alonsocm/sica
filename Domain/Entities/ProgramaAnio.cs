using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProgramaAnio
{
    /// <summary>
    /// Identificador del catálogo ProgramaAnio
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el año
    /// </summary>
    public string Anio { get; set; } = null!;

    public virtual ICollection<Directorio> Directorio { get; set; } = new List<Directorio>();

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorio { get; set; } = new List<LimiteParametroLaboratorio>();

    public virtual ICollection<ParametrosCostos> ParametrosCostos { get; set; } = new List<ParametrosCostos>();

    public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; } = new List<ProgramaSitio>();
}
