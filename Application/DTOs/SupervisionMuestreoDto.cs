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
        public long OrganismoCuencaReporta { get; set; }
        public string SupervisorConagua { get; set; }
        public long SitioId { get; set; }
        public string ClaveMuestreo { get; set; }
        public float LatitudToma { get; set; }
        public float LongitudToma { get; set; }
        public long LaboratorioRealiza { get; set; }
        public int ResponsableToma { get; set; }
        public int ResponsableMediciones { get; set; }
        public string? ObservacionesMuestreo { get; set; }
        public List<ClasificacionCriterioDto> Clasificaciones { get; set; }
        public List<EvidenciaSupervisionDto> LstEvidencia { get; set; }

        public SupervisionMuestreoDto()
        {
            this.FechaMuestreo = string.Empty;
            this.HoraInicio = string.Empty;
            this.HoraTermino = string.Empty;
            this.HoraTomaMuestra = string.Empty;
            this.PuntajeObtenido = 0;
            this.OrganismoCuencaReporta = 0;
            this.SupervisorConagua = string.Empty;
            this.SitioId = 0;
            this.ClaveMuestreo = string.Empty;
            this.LatitudToma = 0;
            this.LongitudToma = 0;
            this.LaboratorioRealiza = 0;
            this.ResponsableToma = 0;
            this.ResponsableMediciones = 0;
            this.LstEvidencia = new List<EvidenciaSupervisionDto>();
        }
    }
}
