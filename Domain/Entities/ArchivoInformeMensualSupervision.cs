using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ArchivoInformeMensualSupervision
{
    public long Id { get; set; }

    public long InformeMensualSupervisionId { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public DateTime FechaCarga { get; set; }

    public long UsuarioCargaId { get; set; }

    public virtual InformeMensualSupervision InformeMensualSupervision { get; set; } = null!;

    public virtual Usuario UsuarioCarga { get; set; } = null!;
}
