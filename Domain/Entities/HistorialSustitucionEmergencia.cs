using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class HistorialSustitucionEmergencia
{
    public long Id { get; set; }

    public long MuestreoEmergenciaId { get; set; }

    public long UsuarioId { get; set; }

    public long Anio { get; set; }

    public DateTime Fecha { get; set; }

    public virtual MuestreoEmergencia MuestreoEmergencia { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
