namespace Application.DTOs
{
    public class CriterioDto
    {
        public int Id { get; set; }
        public long? ValoresSupervisonMuestreoId { get; set; }
        public string Descripcion { get; set; }
        public bool Obligatorio { get; set; }
        public decimal Puntaje { get; set; }
        public string? Cumplimiento { get; set; }
        public string? Observacion { get; set; }
    }
}
