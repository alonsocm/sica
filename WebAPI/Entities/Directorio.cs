using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Directorio
{
    /// <summary>
    /// Identificador principal de la tabla Directorio
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el nombre del personal
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Campo que describe el sexo del personal
    /// </summary>
    public string Sexo { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Puestos
    /// </summary>
    public int PuestoId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de ProgramaAnio
    /// </summary>
    public long ProgramaAnioId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Organismos de cuenca
    /// </summary>
    public long? OrganismoCuencaId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Direcciones locales
    /// </summary>
    public long? DireccionLocalId { get; set; }

    /// <summary>
    /// Campo que describe si se encuentra activo el personal
    /// </summary>
    public bool Activo { get; set; }

    public virtual DireccionLocal? DireccionLocal { get; set; }

    public virtual ICollection<InformeMensualSupervision> InformeMensualSupervision { get; set; } = new List<InformeMensualSupervision>();

    public virtual OrganismoCuenca? OrganismoCuenca { get; set; }

    public virtual ProgramaAnio ProgramaAnio { get; set; } = null!;

    public virtual Puestos Puesto { get; set; } = null!;
}
