using Application.DTOs.EvidenciasMuestreo;

namespace Application.Interfaces
{
    public interface IMetadataExtractorService
    {
        public ImageInformationDto GetMetadaFromImage(Stream stream);

        public InformacionEvidenciaExcelDto ObtenerDatosExcelCaudal(Stream fileCaudal);

        public InformacionEvidenciaExcelDto ObtenerDatosExcelTrack(Stream fileTrack);
    }
}
