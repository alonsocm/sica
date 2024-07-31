namespace Application.DTOs.Users
{
    public class ResumenDTO
    {
        public IEnumerable<ResumenMuestreoDTO> ResumenMuestreo { get; set; }
        public IEnumerable<ResumenResultadosDTO> ResumenResultado { get; set; }
    }

    public class ResumenResultadosDTO
    {
        public string Grupo { get; set; }
        public int Cantidad { get; set; }
    }

    public class ResumenMuestreoDTO
    {
        public string Tipo { get; set; }
        public IEnumerable<CriterioDTO> Criterios { get; set; }
    }

    public class CriterioDTO
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
    }
}
