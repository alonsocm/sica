namespace Application.DTOs
{
    public class AvisoRealizacionDto
    {
        public long Id { get; set; }
        public string ClaveMuestreo { get; set; }
        public string ClaveSitio { get; set; }
        public string TipoSitio { get; set; }
        public string Laboratorio { get; set; }
        public bool ConEventualidades { get; set; }
        public string FechaProgramada { get; set; }
        public string FechaRealVisita { get; set; }
        public string BrigadaMuestreo { get; set; }
        public bool ConQCMuestreos { get; set; }
        public string? FolioEventualidad { get; set; }
        public string? FechaAprobacionEventualidad { get; set; }
        public string TipoSupervision { get; set; }
        public string? DocumentoEventualidad { get; set; }
        public string? TipoEventualidad { get; set; }
        public string? FechaReprogramacion { get; set; }
        public int Linea { get; set; }

        public AvisoRealizacionDto()
        {
            this.Id = 0;
            this.ClaveMuestreo = string.Empty;
            this.ClaveSitio = string.Empty;
            this.TipoSitio = string.Empty;
            this.Laboratorio = string.Empty;
            this.ConEventualidades = false;
            this.FechaProgramada = string.Empty;
            this.FechaRealVisita = string.Empty;
            this.BrigadaMuestreo = string.Empty;
            this.ConQCMuestreos = false;
            this.TipoSupervision = string.Empty;

            this.FolioEventualidad = null;
            this.FechaAprobacionEventualidad = null;
            this.DocumentoEventualidad = null;
            this.TipoEventualidad = null;
            this.FechaReprogramacion = null;
            this.Linea = 0;




        }
    }
}
