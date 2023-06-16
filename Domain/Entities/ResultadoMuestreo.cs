using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ResultadoMuestreo
    {
        public ResultadoMuestreo()
        {
            AprobacionResultadoMuestreo = new HashSet<AprobacionResultadoMuestreo>();
            EvidenciaReplica = new HashSet<EvidenciaReplica>();
        }

        public long Id { get; set; }
        public long MuestreoId { get; set; }
        public long ParametroId { get; set; }
        public string Resultado { get; set; } = null!;
        public string? ObservacionesOcdl { get; set; }
        public bool? EsCorrectoOcdl { get; set; }
        public long? ObservacionesOcdlid { get; set; }
        public long? ObservacionesSecaiaid { get; set; }
        public bool? EsCorrectoSecaia { get; set; }
        public string? ObservacionesSecaia { get; set; }
        public int? EstatusResultado { get; set; }
        public long? ObservacionSrenamecaid { get; set; }
        public string? ObservacionSrenameca { get; set; }
        public DateTime? FechaObservacionSrenameca { get; set; }
        public bool? SeApruebaResultadoReplica { get; set; }
        public DateTime? FechaEstatusFinal { get; set; }
        public string? ResultadoActualizadoReplica { get; set; }
        public string? CausaRechazo { get; set; }
        public bool? SeAceptaRechazoSiNo { get; set; }
        public string? ResultadoReplica { get; set; }
        public bool? EsMismoResultado { get; set; }
        public string? ObservacionLaboratorio { get; set; }
        public DateTime? FechaReplicaLaboratorio { get; set; }
        public string? Comentarios { get; set; }
        public string? ResultadoReglas { get; set; }
        public long LaboratorioId { get; set; }

        public virtual EstatusMuestreo? EstatusResultadoNavigation { get; set; }
        public virtual Laboratorios Laboratorio { get; set; } = null!;
        public virtual Muestreo Muestreo { get; set; } = null!;
        public virtual Observaciones? ObservacionSrenamecaNavigation { get; set; }
        public virtual Observaciones? ObservacionesOcdlNavigation { get; set; }
        public virtual Observaciones? ObservacionesSecaiaNavigation { get; set; }
        public virtual ParametrosGrupo Parametro { get; set; } = null!;
        public virtual ICollection<AprobacionResultadoMuestreo> AprobacionResultadoMuestreo { get; set; }
        public virtual ICollection<EvidenciaReplica> EvidenciaReplica { get; set; }
    }
}
