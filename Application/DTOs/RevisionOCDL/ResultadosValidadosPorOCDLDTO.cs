using Domain.Entities;

namespace Application.DTOs.RevisionOCDL
{
    public class ResultadosValidadosPorOCDLDTO
    {
        public string NoEntregaOCDL { get; set; } = string.Empty;
        public string? ClaveUnica { get; set; }
        public string ClaveSitio { get; set; } = string.Empty;
        public string ClaveMonitoreo { get; set; } = string.Empty;
        public string NombreSitio { get; set; } = string.Empty;
        public string? ClaveParametro { get; set; }
        public string Laboratorio { get; set; } = string.Empty;        
        public string? TipoCuerpoAgua{ get; set; }
        public string? TipoCuerpoAguaOriginal { get; set; }
        public string? Resultado { get; set; }
        public string TipoAprobacion { get; set; } = string.Empty;
        public string? EsCorrectoResultado { get; set; }
        public string? Observaciones { get; set; }
        public string? FechaLimiteRevision { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string? FechaRealizacion { get; set; }
        public string EstatusResultado { get; set; } = string.Empty;
        public int EstatusId { get; set; }
        public int? EstatusOCDL { get; set; }

    }    
}
