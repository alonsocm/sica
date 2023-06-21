using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class AprobacionResultadoMuestreo
{
    public long Id { get; set; }

    public bool ApruebaResultado { get; set; }

    public string ComentariosAprobacionResultados { get; set; } = null!;

    public DateTime FechaAprobRechazo { get; set; }

    public long UsuarioRevisionId { get; set; }

    public long ResultadoMuestreoId { get; set; }

    public virtual ResultadoMuestreo ResultadoMuestreo { get; set; } = null!;

    public virtual Usuario UsuarioRevision { get; set; } = null!;
}
