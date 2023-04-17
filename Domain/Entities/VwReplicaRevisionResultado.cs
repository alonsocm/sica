using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class VwReplicaRevisionResultado
    {
        public long MuestreoId { get; set; }
        public long ParametroId { get; set; }
        public long? UsuarioRevisionId { get; set; }
        public long ResultadoMuestreoId { get; set; }
        public int? EstatusId { get; set; }
        public int EstatusMuestreoId { get; set; }
        public string NumeroEntrega { get; set; } = null!;
        public string ClaveUnica { get; set; } = null!;
        public string ClaveSitio { get; set; } = null!;
        public string ClaveMonitoreo { get; set; } = null!;
        public string NombreSitio { get; set; } = null!;
        public string ClaveParametro { get; set; } = null!;
        public string? Laboratorio { get; set; }
        public string TipoCuerpoAgua { get; set; } = null!;
        public string TipoCuerpoAguaOriginal { get; set; } = null!;
        public string Resultado { get; set; } = null!;
        public bool? EsCorrectoOcdl { get; set; }
        public string? ObservacionesOcdl { get; set; }
        public bool? EsCorrectoSecaia { get; set; }
        public string? ObservacionesSecaia { get; set; }
        public string? ClasificacionObservacion { get; set; }
        public bool? ApruebaResultado { get; set; }
        public string? ComentariosAprobacionResultados { get; set; }
        public DateTime? FechaAprobRechazo { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string? Estatus { get; set; }
        public int? EstatusSecaia { get; set; }
        public string? ResultadoActualizadoReplica { get; set; }
        public string? ObservacionSrenameca { get; set; }
        public string? ComentariosReplicaDiferente { get; set; }
        public DateTime? FechaObservacionSrenameca { get; set; }
        public bool? SeApruebaResultadoReplica { get; set; }
        public DateTime? FechaEstatusFinal { get; set; }
    }
}
