using Application.DTOs.EvidenciasMuestreo;

namespace Application.Interfaces
{
    public interface IMetadataExtractorService
    {
        public ImageInformationDto GetMetadaFromImage(Stream stream);
    }
}
