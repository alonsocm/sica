using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Muestreadores
{
    public int Id { get; set; }

    public long LaboratorioId { get; set; }

    public long BrigadaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Iniciales { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<SupervisionMuestreo> SupervisionMuestreoResponsableMediciones { get; set; } = new List<SupervisionMuestreo>();

    public virtual ICollection<SupervisionMuestreo> SupervisionMuestreoResponsableToma { get; set; } = new List<SupervisionMuestreo>();
}
