using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EvidenciasReplicasResultadoReglasValidacion
{
    /// <summary>
    /// Identificador principal de la tabla EvidenciasReplicasResultadoReglasValidacion
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que indica el nombre del archivo de la evidencia
    /// </summary>
    public string NombreArchivo { get; set; } = null!;

    /// <summary>
    /// Campo que indica si ha sido cargado el archivo
    /// </summary>
    public bool Cargado { get; set; }

    /// <summary>
    /// Campo que describe la fecha en la que fue cargado el archivo
    /// </summary>
    public DateTime FechaCarga { get; set; }

    public virtual ICollection<RelacionEvidenciasReplicaResultadosReglas> RelacionEvidenciasReplicaResultadosReglas { get; set; } = new List<RelacionEvidenciasReplicaResultadosReglas>();
}
