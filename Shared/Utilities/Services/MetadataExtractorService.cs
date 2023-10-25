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
                imageInformation.Altitude = gpsDirectory.GetDouble(GpsDirectory.TagAltitude);
                imageInformation.Direction = gpsDirectory.GetDouble(GpsDirectory.TagImgDirection);
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
                                      "PARSHALL", "Sección-Velocidad(Tablas)","Sección-Velocidad (Angulo)" };
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
            informacionEvidencia.Placas = (worksheetTrack.Cells["A3"].Value == null) ? string.Empty : worksheetTrack.Cells["A3"].Value.ToString();
            informacionEvidencia.Placas = informacionEvidencia.Placas.Replace("PLACAS: ", "").Trim();
            if (worksheetTrack.Cells["A2"].Value != null)
            {
                string fechaReporte = worksheetTrack.Cells["A2"].Value.ToString() ?? string.Empty;
                string[] datosFechaReporte = fechaReporte.Split(' ');
                informacionEvidencia.FechaInicio = datosFechaReporte[1];
                informacionEvidencia.HoraInicio = datosFechaReporte[2];
                informacionEvidencia.FechaFinal = datosFechaReporte[4];
                informacionEvidencia.HoraFinal = datosFechaReporte[5];
                informacionEvidencia.ClaveMuestreo = (worksheetTrack.Cells["D2"].Value?.ToString()) ?? worksheetTrack.Cells["E2"].Value?.ToString() ?? string.Empty;

            }
            return informacionEvidencia;
        }
    }
}
