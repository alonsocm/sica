using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{

    public class ParametrosSustitucionLimitesDto
    {
        public int Usuario { get; set; }
        public int OrigenLimites { get; set; }
        public string Periodo { get; set; }
        public IFormFile? Archivo { get; set; }
        public List<LimiteMaximoComunDto>? LimitesComunes { get; set; }
    }
}
