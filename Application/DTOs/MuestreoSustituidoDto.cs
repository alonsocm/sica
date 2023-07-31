namespace Application.DTOs
{
    public class MuestreoSustituidoDto
    {
        public long MuestreoId { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string NombreSitio { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string FechaRealizacion { get; set; }
        public int Anio { get; set; }
        public List<ResultadoSustituidoDto> Resultados { get; set; }
    }
}