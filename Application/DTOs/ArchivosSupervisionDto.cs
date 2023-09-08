using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class ArchivosSupervisionDto
    {
        public int SupervisionId { get; set; }
        public IFormFileCollection Archivos { get; set; }
        public string ClaveMuestreo { get; set; }
    }
}