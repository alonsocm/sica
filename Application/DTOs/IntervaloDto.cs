namespace Application.DTOs
{

    public class IntervaloDto
    {
        public string Calificacion { get; set; }
        public string NumeroSitios { get; set; }
        public string Porcentaje { get; set; }
        public IntervaloDto()
        {
            this.Calificacion = string.Empty;
            this.NumeroSitios = string.Empty;
            this.Porcentaje = string.Empty;
        }
    }
}
