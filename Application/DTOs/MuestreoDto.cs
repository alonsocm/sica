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
        public string NumeroCarga { get; set; }
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
        public string TipoCuerpoAguaHomologado { get; set; }
        public string? SubTipoCuerpoAgua { get; set; }
        public string DireccionLocal { get; set; }
        public string OrganismoCuenca { get; set; }

        public List<EvidenciaDto> Evidencias { get; set; }

        public long ProgramaMuestreoId { get; set; }
        public int EstatusId { get; set; }
        public string? FechaEntregaMuestreo { get; set; }
        public string FechaCargaEvidencias { get; set; }

        public bool AutorizacionIncompleto { get; set; }
        public bool AutorizacionFechaEntrega { get; set; }
        public string CorreReglaValidacion { get; set; }

        public string? UsuarioValido { get; set; }
        public string PorcentajePago { get; set; }
        public int EvidenciasEsperadas { get; set; }
        public string CumpleNumeroEvidencias { get; set; }

        public MuestreoDto()
        {
            DiaProgramado = string.Empty;
            Evidencias = new List<EvidenciaDto>();
            OCDL = string.Empty;
            ClaveSitioOriginal = string.Empty;
            CuerpoAgua = string.Empty;
            Estado = string.Empty;
            FechaCarga = string.Empty;
            FechaLimiteRevision = string.Empty;
            HoraCargaEvidencias = string.Empty;
            NumeroCargaEvidencias = string.Empty;
            NumeroEntrega = string.Empty;
            NumeroCarga = string.Empty;
            Observaciones = string.Empty;
            DireccionLocal = string.Empty;
            Estatus = string.Empty;
            HoraInicio = string.Empty;
            HoraFin = string.Empty;
            Laboratorio = string.Empty;
            LaboratorioSubrogado = string.Empty;
            OrganismoCuenca = string.Empty;
            ProgramaAnual = string.Empty;
            TipoSitio = string.Empty;
            TipoCuerpoAguaOriginal = string.Empty;
            TipoCuerpoAguaHomologado = string.Empty;
            ProgramaMuestreoId = 0;
            EstatusId = 0;
            MuestreoId = 0;
            SubTipoCuerpoAgua = string.Empty;
            FechaEntregaMuestreo = string.Empty;
            FechaCargaEvidencias = string.Empty;
            AutorizacionIncompleto = false;
            AutorizacionFechaEntrega=false;
            CorreReglaValidacion = string.Empty;
            FechaProgramada = string.Empty;
            TipoCuerpoAgua = string.Empty;
            ClaveMonitoreo = string.Empty;
            NombreSitio = string.Empty;
            ClaveSitio = string.Empty;
            FechaRealizacion = string.Empty;
            PorcentajePago = string.Empty;
        }
    }
}
