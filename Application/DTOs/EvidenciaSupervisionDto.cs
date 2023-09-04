namespace Application.DTOs
{
    public class EvidenciaSupervisionDto : EvidenciaDto
    {
        public long SupervisionMuestreoId { get; set; }
        public EvidenciaSupervisionDto()
        {
            this.SupervisionMuestreoId = 0;
        }
    }
}
