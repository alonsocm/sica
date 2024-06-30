using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProgramaSitio
{
    /// <summary>
    /// Identificador principal de la tabla Programa sitio
    /// 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de TipoSitio
    /// </summary>
    public long TipoSitioId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Sitio
    /// </summary>
    public long SitioId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Laboratorio
    /// </summary>
    public long? LaboratorioId { get; set; }

    /// <summary>
    /// Campo que indica el número de muestreos a realizar al año
    /// </summary>
    public int NumMuestreosRealizarAnio { get; set; }

    /// <summary>
    /// Campo que describe las observaciones
    /// </summary>
    public string? Observaciones { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la tabla de ProgramaAnio
    /// </summary>
    public long ProgramaAnioId { get; set; }

    public virtual Laboratorios? Laboratorio { get; set; }

    public virtual ProgramaAnio ProgramaAnio { get; set; } = null!;

    public virtual ICollection<ProgramaMuestreo> ProgramaMuestreo { get; set; } = new List<ProgramaMuestreo>();

    public virtual Sitio Sitio { get; set; } = null!;

    public virtual TipoSitio TipoSitio { get; set; } = null!;
}
