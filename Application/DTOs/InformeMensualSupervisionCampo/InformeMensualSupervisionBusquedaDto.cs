namespace Application.DTOs.InformeMensualSupervisionCampo
{
    public class InformeMensualSupervisionBusquedaDto
    {
        public long Id { get; set; }
        public string? Oficio { get; set; }
        public string? Lugar { get; set; }
        public string? DireccionTecnica { get; set; }
        public string? MesReporte { get; set; }
        public string? Contrato { get; set; }
        public string? NombreFirma { get; set; }
        public string? PuestoFirma { get; set; }
        public string? FechaRegistro { get; set; }
        public string? FechaRegistroFin { get; set; }
        public string? Iniciales { get; set; }
        public bool ExisteInformeFirmado { get; set; }
        public InformeMensualSupervisionBusquedaDto()
        {
            this.Id = 0;
        }

    }
}
