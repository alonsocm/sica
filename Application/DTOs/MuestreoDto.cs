namespace Application.DTOs
{
    public class MuestreoDto
    {
        public long MuestreoId { get; set; }
        public string OCDL { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string Estado { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string Laboratorio { get; set; }
        public string FechaRealizacion { get; set; }
        public string FechaLimiteRevision { get; set; }
        public string NumeroEntrega { get; set; }
        public string Estatus { get; set; }
        public string? TipoCargaResultados { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string DiaProgramado { get; set; }
        public string ProgramaAnual { get; set; }
        public string FechaProgramada { get; set; }
        public string NombreSitio { get; set; }
        public string FechaCarga { get; set; }
        public string CuerpoAgua { get; set; }
        public string TipoSitio { get; set; }
        public string Observaciones { get; set; }
        public string LaboratorioSubrogado { get; set; }
        public string ClaveSitioOriginal { get; set; }
        public string HoraCargaEvidencias { get; set; }
        public string NumeroCargaEvidencias { get; set; }
        public string TipoCuerpoAguaOriginal { get; set; }
        public string? SubTipoCuerpoAgua { get; set; }
        public string DireccionLocal { get; set; }
        public string OrganismoCuenca { get; set; }

        public List<EvidenciaDto> Evidencias { get; set; }

        public long ProgramaMuestreoId { get; set; }
        public int estatusId { get; set; }
        public string? FechaEntregaMuestreo { get; set; }

        public MuestreoDto()
        {
            this.DiaProgramado = string.Empty;
            this.Evidencias = new List<EvidenciaDto>();
            this.OCDL = string.Empty;
            this.ClaveSitioOriginal = string.Empty;
            this.CuerpoAgua = string.Empty;
            this.Estado = string.Empty;
            this.FechaCarga = string.Empty;
            this.FechaLimiteRevision = string.Empty;
            this.HoraCargaEvidencias = string.Empty;
            this.NumeroCargaEvidencias = string.Empty;
            this.NumeroEntrega = string.Empty;
            this.Observaciones = string.Empty;
            this.DireccionLocal = string.Empty;
            this.Estatus = string.Empty;
            this.HoraInicio = string.Empty;
            this.HoraFin = string.Empty;
            this.Laboratorio = string.Empty;
            this.LaboratorioSubrogado = string.Empty;
            this.OrganismoCuenca = string.Empty;
            this.ProgramaAnual = string.Empty;
            this.TipoSitio = string.Empty;
            this.TipoCuerpoAguaOriginal = string.Empty;
            this.ProgramaMuestreoId = 0;
            this.estatusId = 0;
            this.MuestreoId = 0;
            this.SubTipoCuerpoAgua = string.Empty;
            this.FechaEntregaMuestreo = string.Empty;

        }
    }
}
