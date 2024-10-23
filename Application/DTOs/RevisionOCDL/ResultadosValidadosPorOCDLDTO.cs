namespace Application.DTOs.RevisionOCDL
{
    public class ResultadosValidadosPorOCDLDTO
    {
        public long Id { get; set; }

        public long MuestreoId { get; set; }
        public string NoEntregaOCDL { get; set; } = string.Empty;
        public string ClaveUnica { get; set; } = string.Empty;
        public string ClaveSitio { get; set; } = string.Empty;
        public string ClaveMonitoreo { get; set; } = string.Empty;
        public string NombreSitio { get; set; } = string.Empty;
        public string ClaveParametro { get; set; } = string.Empty;
        public string Laboratorio { get; set; } = string.Empty;
        public string TipoCuerpoAgua { get; set; } = string.Empty;
        public string TipoCuerpoAguaOriginal { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
        public string TipoAprobacion { get; set; } = string.Empty;
        public string EsCorrectoResultado { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public string FechaLimiteRevision { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string FechaRealizacion { get; set; } = string.Empty;
        public string EstatusResultado { get; set; } = string.Empty;
        public int EstatusId { get; set; }
        public int? EstatusOCDL { get; set; }
        public int? EstatusSECAIA { get; set; }

    }
}
