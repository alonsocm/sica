using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoArchivoInformeMensualSupervision
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;
}
