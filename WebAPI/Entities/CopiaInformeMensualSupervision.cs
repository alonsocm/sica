using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CopiaInformeMensualSupervision
{
    /// <summary>
    /// Identificador principal de tabla de CopiaReporteInformeMensual
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el nombre de la persona que se enviara copia
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Campo que describe el puesto del usuario que se enviara copia
    /// </summary>
    public string Puesto { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace relación a la tabla de InformeMensualSupervision
    /// </summary>
    public long InformeMensualSupervisionId { get; set; }

    public virtual InformeMensualSupervision InformeMensualSupervision { get; set; } = null!;
}
