using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Jpeg;
using OfficeOpenXml;

namespace Shared.Utilities.Services
{
    public class MetadataExtractorService : IMetadataExtractorService
    {
        public static int StartColumn = 1;

        public ImageInformationDto GetMetadaFromImage(Stream stream)
        {
            var directories = ImageMetadataReader.ReadMetadata(stream);
            ImageInformationDto imageInformation = new();

            var gpsDirectory = directories.OfType<GpsDirectory>().FirstOrDefault();
            var exifDirectory = directories.OfType<ExifDirectoryBase>().FirstOrDefault();
            var jpegDirectory = directories.OfType<JpegDirectory>().FirstOrDefault();

            if (gpsDirectory != null)
            {
                var geolocalization = gpsDirectory.GetGeoLocation();
                imageInformation.Latitude = geolocalization?.Latitude;
                imageInformation.Longitude = geolocalization?.Longitude;
                imageInformation.Altitude = gpsDirectory.ContainsTag(GpsDirectory.TagAltitude) ? gpsDirectory.GetDouble(GpsDirectory.TagAltitude) : null;
                imageInformation.Direction = gpsDirectory.ContainsTag(GpsDirectory.TagImgDirection) ? gpsDirectory.GetDouble(GpsDirectory.TagImgDirection) : null;
            }

            if (exifDirectory != null)
            {
                imageInformation.Make = exifDirectory.GetDescription(ExifDirectoryBase.TagMake);
                imageInformation.Model = exifDirectory.GetDescription(ExifDirectoryBase.TagModel);
                imageInformation.Aperture = exifDirectory.GetDescription(ExifDirectoryBase.TagAperture);
                imageInformation.FocalLength = exifDirectory.GetDescription(ExifDirectoryBase.TagFocalLength);
                imageInformation.Iso = exifDirectory.GetDescription(ExifDirectoryBase.TagIsoSpeed);
                imageInformation.Flash = exifDirectory.GetDescription(ExifDirectoryBase.TagFlash);
                imageInformation.Shutter = exifDirectory.GetDescription(ExifDirectoryBase.TagShutterSpeed);
                imageInformation.DateTime = exifDirectory.GetDescription(ExifDirectoryBase.TagDateTime) != null ? exifDirectory.GetDateTime(ExifDirectoryBase.TagDateTime) : null;
            }

            if (jpegDirectory != null)
            {
                imageInformation.Height = jpegDirectory.GetImageHeight();
                imageInformation.Width = jpegDirectory.GetImageWidth();
            }

            return imageInformation;
        }

        public InformacionEvidenciaExcelDto ObtenerDatosExcelCaudal(Stream fileCaudal)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage package = new(fileCaudal);
            string[] tiposMetodos = { "Sección-Velocidad", "Volumen-Tiempo", "Flotador", "EstaciónHidrométrica",
                                      "PARSHALL", "Sección-Velocidad(Tablas)","Sección-Velocidad(Angulo)" };
            bool respuestaAforo = false;
            InformacionEvidenciaExcelDto informacionEvidencia = new();

            foreach (var item in tiposMetodos)
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[item];

                if (worksheet != null && !respuestaAforo)
                {
                    informacionEvidencia.LatitudAforo = (worksheet.Cells["O5"].Value == null) ? string.Empty : worksheet.Cells["O5"].Value.ToString();
                    informacionEvidencia.LongitudAforo = (worksheet.Cells["O6"].Value == null) ? string.Empty : worksheet.Cells["O6"].Value.ToString();
                    respuestaAforo = informacionEvidencia.LatitudAforo != string.Empty && informacionEvidencia.LongitudAforo != string.Empty;
                }
            }

            return informacionEvidencia;
        }

        public InformacionEvidenciaExcelDto ObtenerDatosExcelTrack(Stream fileTrack)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage package = new(fileTrack);
            InformacionEvidenciaExcelDto informacionEvidencia = new();
            ExcelWorksheet worksheetTrack = package.Workbook.Worksheets[0];

            informacionEvidencia.Placas = (worksheetTrack.Cells["B2"].Value == null) ? string.Empty : worksheetTrack.Cells["B2"].Value.ToString();
            informacionEvidencia.FechaInicio = (worksheetTrack.Cells["B3"].Value == null) ? string.Empty : worksheetTrack.Cells["B3"].Value.ToString().Trim().Split(' ')[0];
            informacionEvidencia.HoraInicio = (worksheetTrack.Cells["B4"].Value == null) ? string.Empty : worksheetTrack.Cells["B4"].Value.ToString().Trim().Split(' ')[1];
            informacionEvidencia.FechaFinal = (worksheetTrack.Cells["B5"].Value == null) ? string.Empty : worksheetTrack.Cells["B5"].Value.ToString().Trim().Split(' ')[0];
            informacionEvidencia.HoraFinal = (worksheetTrack.Cells["B6"].Value == null) ? string.Empty : worksheetTrack.Cells["B6"].Value.ToString().Trim().Split(' ')[1];
            informacionEvidencia.ClaveMuestreo = (worksheetTrack.Cells["D4"].Value == null) ? string.Empty : worksheetTrack.Cells["D4"].Value.ToString();

            string identificacionGeocerca = (informacionEvidencia.ClaveMuestreo != string.Empty) ? informacionEvidencia.ClaveMuestreo.Split('-')[0].ToString() : string.Empty;
            var registros = ExcelService.ImportarDatosRango<InformacionTrackDto>(11, 1, worksheetTrack).Where(x => x.IdentificacionGeocerca == identificacionGeocerca && x.Fecha == worksheetTrack.Cells["B3"].Value.ToString().Trim() && x.Hora.Trim() == worksheetTrack.Cells["B4"].Value.ToString().Trim()).FirstOrDefault();

            if (registros != null)
            {
                informacionEvidencia.LatitudAforo = registros.LatitudGeocercaCercana ?? string.Empty;
                informacionEvidencia.LongitudAforo = registros.LongitudGeocercaCercana ?? string.Empty;
            }

            return informacionEvidencia;
        }
    }
}
