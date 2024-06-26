using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class FormaReporteEspecifica
{
    /// <summary>
    /// Identificador del catalogo de FormaReporteEspecifica
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea haciendo referencia al catalogo ParametroGrupo
    /// </summary>
    public long ParametroId { get; set; }

    /// <summary>
    /// Descripción de la forma reporte especifica
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ParametrosGrupo Parametro { get; set; } = null!;
}
