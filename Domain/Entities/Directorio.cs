using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Directorio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public int PuestoId { get; set; }

    public long ProgramaAnioId { get; set; }

    public long? OrganismoCuencaId { get; set; }

    public long? DireccionLocalId { get; set; }

    public bool Activo { get; set; }

    public virtual DireccionLocal? DireccionLocal { get; set; }

    public virtual ICollection<InformeMensualSupervision> InformeMensualSupervision { get; set; } = new List<InformeMensualSupervision>();

    public virtual OrganismoCuenca? OrganismoCuenca { get; set; }

    public virtual ProgramaAnio ProgramaAnio { get; set; } = null!;

    public virtual Puestos Puesto { get; set; } = null!;
}
