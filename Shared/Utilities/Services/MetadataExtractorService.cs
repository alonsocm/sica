using Application.DTOs.EvidenciasMuestreo;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Jpeg;

namespace Shared.Utilities.Services
{
    public class MetadataExtractorService
    {
        public static ImageInformationDto GetMetadaFromImage(Stream stream)
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
                imageInformation.Height = jpegDirectory.GetDescription(JpegDirectory.TagImageHeight);
                imageInformation.Width = jpegDirectory.GetDescription(JpegDirectory.TagImageWidth);
            }

            return imageInformation;
        }
    }
}
