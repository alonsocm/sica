using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DestinatariosAtencion
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }
}
