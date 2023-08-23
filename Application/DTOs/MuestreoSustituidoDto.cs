namespace Application.DTOs
{
    public class MuestreoSustituidoDto
    {
        private string noEntrega;

        public long MuestreoId { get; set; }
        public string NoEntrega { get => $"{noEntrega}a {Anio}"; set => noEntrega=value; }
        public string TipoSitio { get; set; }
        public string ClaveSitio { get; set; }
        public string NombreSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string FechaRealizacion { get; set; }
        public string Laboratorio { get; set; }
        public string CuerpoAgua { get; set; }
        public string TipoCuerpoAguaOriginal { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string Anio { get; set; }
        public List<ResultadoSustituidoDto> Resultados { get; set; }
    }
}