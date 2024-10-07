using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class RelacionEvidenciasReplicaResultadosReglas
{
    /// <summary>
    /// Llave primaria de la tabla de relación que existe entre las evidencias y las replicas de resultados de reglas de validación
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foranea que hace referencia a la tabla de EvidenciaReplicaResultadosReglas
    /// </summary>
    public long EvidenciasReplicasResultadoReglasValidacionId { get; set; }

    /// <summary>
    /// Llave foranea que hace referencia a la tabla de ReplicaResultadosReglasValidación describe la replica que ocupa dicha evidencia
    /// </summary>
    public long ReplicasResultadosReglasValidacionId { get; set; }

    public virtual EvidenciasReplicasResultadoReglasValidacion EvidenciasReplicasResultadoReglasValidacion { get; set; } = null!;

    public virtual ReplicasResultadosReglasValidacion ReplicasResultadosReglasValidacion { get; set; } = null!;
}
