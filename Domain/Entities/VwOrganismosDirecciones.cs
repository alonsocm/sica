using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VwOrganismosDirecciones
{
    public long Id { get; set; }

    public string OrganismoCuencaDireccionLocal { get; set; } = null!;

    public long OrganismoCuencaId { get; set; }

    public string NombreOrganismoCuenca { get; set; } = null!;
}
