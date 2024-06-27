using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BrigadaMuestreo
{
    /// <summary>
    /// Identificador de tabla  BrigadaMuestreo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Descripción de la Brigada
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Estatus de activo
    /// </summary>
    public bool Activo { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Laboratorios para indicar que laboratorio tiene la brigada
    /// </summary>
    public long? LaboratorioId { get; set; }

    /// <summary>
    /// Campo que describe el nombre del lider
    /// </summary>
    public string? Lider { get; set; }

    /// <summary>
    /// Campo que describe las placas
    /// </summary>
    public string? Placas { get; set; }

    public virtual ICollection<AvisoRealizacion> AvisoRealizacion { get; set; } = new List<AvisoRealizacion>();

    public virtual Laboratorios? Laboratorio { get; set; }

    public virtual ICollection<ProgramaMuestreo> ProgramaMuestreo { get; set; } = new List<ProgramaMuestreo>();
}
