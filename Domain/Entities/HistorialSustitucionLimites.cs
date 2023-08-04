using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class HistorialSustitucionLimites
{
    public long Id { get; set; }

    public long MuestreoId { get; set; }

    public int TipoSustitucionId { get; set; }

    public long UsuarioId { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Muestreo Muestreo { get; set; } = null!;

    public virtual TipoSustitucion TipoSustitucion { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
