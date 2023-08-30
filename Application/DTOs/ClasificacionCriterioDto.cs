namespace Application.DTOs
{
    public class ClasificacionCriterioDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<CriterioDto> Criterios { get; set; }
    }
}
