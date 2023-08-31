using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class ArchivosSupervisionDto
    {
        public int SupervisionId { get; set; }
        public IFormFile ArchivoSupervision { get; set; }
        public IFormFileCollection Evidencias { get; set; }
    }
}
