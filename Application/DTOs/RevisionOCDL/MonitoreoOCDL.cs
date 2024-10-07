namespace Application.DTOs.RevisionOCDL
{
    public class MonitoreoOCDL
    {
        public long MonitoreoId { get; set; }
        public string NumeroEntrega { get; set; } = string.Empty;
        public string OCDL { get; set; } = string.Empty;
        public string ClaveSitio { get; set; } = string.Empty;
        public string ClaveMonitoreo { get; set; } = string.Empty;
        public string Sitio { get; set; } = string.Empty;
        public string FechaRealizacion { get; set; } = string.Empty;
        public string Laboratorio { get; set; } = string.Empty;
        public string TipoCuerpoAguaOriginal { get; set; } = string.Empty;
        public string TipoCuerpoAgua { get; set; } = string.Empty;
        public string ObservacionLaboratorio { get; set; } = string.Empty;
        public string FechaLimiteRevison { get; set; } = string.Empty;
    }
}
