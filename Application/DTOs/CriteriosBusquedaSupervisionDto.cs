namespace Application.DTOs
{
    public class CriteriosBusquedaSupervisionDto
    {
        public long? OrganismosDireccionesRealizaId { get; set; }
        public long? SitioId { get; set; }
        public string? FechaMuestreo { get; set; }
        public string? FechaMuestreoFin { get; set; }
        public string? PuntajeObtenido { get; set; }
        public long? LaboratorioRealizaId { get; set; }
        public string? ClaveMuestreo { get; set; }
        public long? TipoCuerpoAguaId { get; set; }


    }
}
