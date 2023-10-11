namespace Application.DTOs.EvidenciasMuestreo
{
    public class ImageInformationDto
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Aperture { get; set; }
        public string? FocalLength { get; set; }
        public string? Iso { get; set; }
        public string? Flash { get; set; }
        public string? Shutter { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Height { get; set; }
        public string? Width { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public double? Direction { get; set; }
    }
}
