using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class AccionLaboratorio
{
    /// <summary>
    /// Identificador principal del catálogo AccionLaboratorio 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe si subroga
    /// </summary>
    public string LoSubroga { get; set; } = null!;

    /// <summary>
    /// Campo que describe el significado para las opciones NA y NRL
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorioLoSubroga { get; set; } = new List<LimiteParametroLaboratorio>();

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorioRealizaLaboratorioMuestreo { get; set; } = new List<LimiteParametroLaboratorio>();
}
