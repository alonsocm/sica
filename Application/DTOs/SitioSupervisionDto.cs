namespace Application.DTOs
{
    public class SitioSupervisionDto
    {
        public List<string> ClavesMuestreo { get; set; }
        public string ClaveSitio { get; set; }
        public long SitioId { get; set; }
        public string Nombre { get; set; }
        public long CuencaDireccionLocalId { get; set; }
        public String Latitud { get; set; }
        public string Longitud { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public SitioSupervisionDto()
        {
            this.ClavesMuestreo = new List<string>();
            this.ClaveSitio = string.Empty;
            this.SitioId = 0;
            this.Nombre = string.Empty;
            this.CuencaDireccionLocalId = 0;
            this.Latitud = string.Empty;
            this.Longitud = string.Empty;
            this.TipoCuerpoAgua = string.Empty;
        }
    }
}
