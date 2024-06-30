using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoArchivoInformeMensualSupervision
{
    /// <summary>
    /// Identificador principal del catálogo TipoArchivoInformeMensualSupervision
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el tipo de archivo de informe mensual de supervisión
    /// </summary>
    public string Descripcion { get; set; } = null!;
}
