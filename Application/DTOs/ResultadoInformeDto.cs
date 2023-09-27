namespace Application.DTOs
{
    public class ResultadoInformeDto
    {
        public string OcDl { get; set; }
        public string TotalSitios { get; set; }
        public List<IntervaloDto> Intervalos { get; set; }
        public ResultadoInformeDto()
        {
            this.OcDl = string.Empty;
            this.TotalSitios = string.Empty;
            this.Intervalos = new List<IntervaloDto>();
        }
    }
}
