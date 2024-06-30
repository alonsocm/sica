using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoSitio
{
    /// <summary>
    /// Identificador principal de la tabla TipoSitio
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el tipo sitio
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<AvisoRealizacion> AvisoRealizacion { get; set; } = new List<AvisoRealizacion>();

    public virtual ICollection<ParametrosSitioTipoCuerpoAgua> ParametrosSitioTipoCuerpoAgua { get; set; } = new List<ParametrosSitioTipoCuerpoAgua>();

    public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; } = new List<ProgramaSitio>();
}
