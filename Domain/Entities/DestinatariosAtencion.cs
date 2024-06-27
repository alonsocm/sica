using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DestinatariosAtencion
{
    /// <summary>
    /// Identificador principal del catálogo DestinatariosAtencion
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el destinatario de atención
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que describe si se encuentra activo el destinatario
    /// </summary>
    public bool Activo { get; set; }
}
