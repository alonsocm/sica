namespace Application.DTOs
{
    public class SupervisionMuestreoDto
    {
        public long Id { get; set; }
        public string FechaMuestreo { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public string HoraTomaMuestra { get; set; }
        public decimal PuntajeObtenido { get; set; }
        public long OrganismosDireccionesRealizaId { get; set; }
        public string OrganismosDireccionesRealiza { get; set; }
        public long OrganismoCuencaReportaId { get; set; }
        public string OrganismoCuencaReporta { get; set; }
        public string SupervisorConagua { get; set; }
        public long SitioId { get; set; }
        public string ClaveSitio { get; set; }
        public string? NombreSitio { get; set; }
        public string? LatitudSitio { get; set; }
        public string? LongitudSitio { get; set; }
        public string? TipoCuerpoAgua { get; set; }
        public string ClaveMuestreo { get; set; }
        public float LatitudToma { get; set; }
        public float LongitudToma { get; set; }
        public long LaboratorioRealizaId { get; set; }
        public string LaboratorioRealiza { get; set; }
        public int ResponsableTomaId { get; set; }
        public string ResponsableToma { get; set; }
        public int ResponsableMedicionesId { get; set; }
        public string ResponsableMediciones { get; set; }
        public string? ObservacionesMuestreo { get; set; }
        public List<ClasificacionCriterioDto> Clasificaciones { get; set; }
        public List<EvidenciaSupervisionDto> Archivos { get; set; }

        public SupervisionMuestreoDto()
        {
            this.FechaMuestreo = string.Empty;
            this.HoraInicio = string.Empty;
            this.HoraTermino = string.Empty;
            this.HoraTomaMuestra = string.Empty;
            this.PuntajeObtenido = 0;
            this.OrganismoCuencaReportaId = 0;
            this.SupervisorConagua = string.Empty;
            this.SitioId = 0;
            this.ClaveMuestreo = string.Empty;
            this.LatitudToma = 0;
            this.LongitudToma = 0;
            this.LaboratorioRealizaId = 0;
            this.ResponsableTomaId = 0;
            this.ResponsableMedicionesId = 0;
            this.Archivos = new List<EvidenciaSupervisionDto>();
            this.Clasificaciones = new List<ClasificacionCriterioDto>();
            this.OrganismosDireccionesRealiza = string.Empty;
            this.OrganismoCuencaReporta = string.Empty;
            this.LaboratorioRealiza = string.Empty;
            this.ResponsableToma = string.Empty;
            this.ResponsableMediciones = string.Empty;
        }
    }
}
