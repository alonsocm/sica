using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReplicasResultadosReglasValidacion
{
    public long Id { get; set; }

    public long ResultadoMuestreoId { get; set; }

    public bool? AceptaRechazo { get; set; }

    public string ResultadoReplica { get; set; } = null!;

    public bool? MismoResultado { get; set; }

    public string ObservacionLaboratorio { get; set; } = null!;

    public DateTime? FechaReplicaLaboratorio { get; set; }

    public string? ObservacionSrenameca { get; set; }

    public bool? EsDatoCorrectoSrenameca { get; set; }

    public DateTime? FechaObservacionSrenameca { get; set; }

    public string? ObservacionesReglasReplica { get; set; }

    public bool? ApruebaResultadoReplica { get; set; }

    public DateTime FechaEstatusFinal { get; set; }

    public long UsuarioIdReviso { get; set; }

    public virtual ICollection<EvidenciasReplicasResultadoReglasValidacion> EvidenciasReplicasResultadoReglasValidacion { get; set; } = new List<EvidenciasReplicasResultadoReglasValidacion>();

    public virtual ResultadoMuestreo ResultadoMuestreo { get; set; } = null!;
}
