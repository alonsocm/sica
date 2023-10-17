using Microsoft.AspNetCore.Http;

namespace Application.DTOs.InformeMensualSupervisionCampo
{
    public class InformeMensualDto
    {
        public IFormFile Archivo { get; set; }
        public string Oficio { get; set; }
        public string Lugar { get; set; }
        public string Fecha { get; set; }
        public int ResponsableId { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string PersonasInvolucradas { get; set; }
        public int Usuario { get; set; }
        public List<Copia> Copias { get; set; }
    }
}
