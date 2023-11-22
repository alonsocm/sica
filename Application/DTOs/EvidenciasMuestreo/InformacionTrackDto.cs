namespace Application.DTOs.EvidenciasMuestreo
{
    public class InformacionTrackDto
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string EstatusUnidad { get; set; }
        public string LatitudReporte { get; set; }
        public string LongitudReporte { get; set; }
        public string DistanciaAGeocerca { get; set; }
        public string Ubicacion { get; set; }
        public string EstatusRangoGeocerca { get; set; }
        public string IdentificacionGeocerca { get; set; }
        public string LatitudGeocercaCercana { get; set; }
        public string LongitudGeocercaCercana { get; set; }
    }
}
