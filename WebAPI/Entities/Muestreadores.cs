using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Muestreadores
{
    /// <summary>
    /// Identificador principal de tabla de la tabla Muestradores
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Laboratorios
    /// </summary>
    public long LaboratorioId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de BrigadaMuestreo
    /// </summary>
    public long BrigadaId { get; set; }

    /// <summary>
    /// Campo que describe el nombre del muestrador
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Campo que describe el apellido paterno del muestrador
    /// </summary>
    public string ApellidoPaterno { get; set; } = null!;

    /// <summary>
    /// Campo que describe el apellido materno del muestrador
    /// </summary>
    public string ApellidoMaterno { get; set; } = null!;

    /// <summary>
    /// Campo que describe las iniciales del usuario
    /// </summary>
    public string Iniciales { get; set; } = null!;

    /// <summary>
    /// Campo que describe si es activo el muestrador
    /// </summary>
    public bool Activo { get; set; }

    public virtual ICollection<SupervisionMuestreo> SupervisionMuestreoResponsableMediciones { get; set; } = new List<SupervisionMuestreo>();

    public virtual ICollection<SupervisionMuestreo> SupervisionMuestreoResponsableToma { get; set; } = new List<SupervisionMuestreo>();
}
