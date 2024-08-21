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
    /// Llave foranea que hace referencia a la tabla de ReplicasResultadoReglasValidacion
    /// </summary>
    public long ReplicasResultadoReglasValidacionId { get; set; }

    /// <summary>
    /// Campo que indica el nombre del archivo de la evidencia
    /// </summary>
    public string NombreArchivo { get; set; } = null!;

    public virtual ReplicasResultadosReglasValidacion ReplicasResultadoReglasValidacion { get; set; } = null!;
}
