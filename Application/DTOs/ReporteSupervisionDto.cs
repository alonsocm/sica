namespace Application.DTOs
{
    public class ReporteSupervisionDto
    {
        public string Memorando { get; set; }
        public string Lugar { get; set; }
        public string Fecha { get; set; }
        public int DestinatarioAtencionesId { get; set; }
        public string DirectoroResponsable { get; set; }
        public string Puesto { get; set; }
        public string InicialesInvolucrados { get; set; }
        public List<string> lstCopiasCorreos { get; set; }
        public string Mes { get; set; }
        public ReporteSupervisionDto()
        {
            this.Memorando = string.Empty;
            this.Lugar = string.Empty;
            this.Fecha = string.Empty;
            this.DestinatarioAtencionesId = 0;
            this.DirectoroResponsable = string.Empty;
            this.Puesto = string.Empty;
            this.InicialesInvolucrados = string.Empty;
            this.lstCopiasCorreos = new List<string>();
            this.Mes = string.Empty;
        }
    }
}
