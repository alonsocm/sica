using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReplicasResultadosReglasValidacion
{
    /// <summary>
    /// Identificador de llave primaria para la tabla de ReplicasResultadosReglasValidacion
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foranea que hace referencia a la tabla de ResuladosMuestreo, este campo describe el resultado del cual es la replica
    /// </summary>
    public long ResultadoMuestreoId { get; set; }

    /// <summary>
    /// Campo que describe si la replica acepa el rechazo
    /// </summary>
    public bool? AceptaRechazo { get; set; }

    /// <summary>
    /// Campo que describe el resulado de la replica
    /// </summary>
    public string ResultadoReplica { get; set; } = null!;

    /// <summary>
    /// Campo que describe si es el mismo resultado de la replica vs con el resultado anterior del parámetro de muestreo correspondiente
    /// </summary>
    public bool? MismoResultado { get; set; }

    /// <summary>
    /// Campo que describe las observaciones que realizó el laboratorio
    /// </summary>
    public string ObservacionLaboratorio { get; set; } = null!;

    /// <summary>
    /// Campo que describe la fecha en la que se elaboro la replica por parte del laboratorio
    /// </summary>
    public DateTime? FechaReplicaLaboratorio { get; set; }

    /// <summary>
    /// Campo que describe las obervaciones por parte de SRENAMECA
    /// </summary>
    public string? ObservacionSrenameca { get; set; }

    /// <summary>
    /// Campo que indica si el dato es correcto por parte del area de SRENAMECA
    /// </summary>
    public bool? EsDatoCorrectoSrenameca { get; set; }

    /// <summary>
    /// Campo que describe la fecha en la que se realizó las observaciones por parte del área de SRENAMECA
    /// </summary>
    public DateTime? FechaObservacionSrenameca { get; set; }

    /// <summary>
    /// Campo que describe las observaciones al aplicar la regla en la replica en el módulo de Resumen de resultados
    /// </summary>
    public string? ObservacionesReglasReplica { get; set; }

    /// <summary>
    /// Campo que indica si se aprueba el resultado de la replica
    /// </summary>
    public bool? ApruebaResultadoReplica { get; set; }

    /// <summary>
    /// Campo que indica la fecha del estatus final
    /// </summary>
    public DateTime FechaEstatusFinal { get; set; }

    /// <summary>
    /// Llave foranea que hace relación a la tabla de Usuarios indicando el personal que realizó la revisión
    /// </summary>
    public long UsuarioIdReviso { get; set; }

    public virtual ICollection<RelacionEvidenciasReplicaResultadosReglas> RelacionEvidenciasReplicaResultadosReglas { get; set; } = new List<RelacionEvidenciasReplicaResultadosReglas>();

    public virtual ResultadoMuestreo ResultadoMuestreo { get; set; } = null!;
}
