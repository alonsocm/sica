namespace Application.DTOs
{
    public class ResultadoMuestreoEmergenciaDto
    {
        public string NombreEmergencia { get; set; }
        public string Sitio { get; set; }
        public string FechaRealizacion { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string Laboratorio { get; set; }
        public List<ResultadoSustituidoDto> Resultados { get; set; }
    }
}
