using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class IntervalosPuntajeSupervision
{
    /// <summary>
    /// Identificador principal del catalogo de IntervalosPuntajeSupervision
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el intervalo
    /// </summary>
    public string Descripcion { get; set; } = null!;
}
