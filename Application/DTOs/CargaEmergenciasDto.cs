using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class CargaEmergenciasDto
    {
        public int Anio { get; set; }
        public IFormFile Archivo { get; set; }
        public bool? Reemplazar { get; set; }
    }
}
