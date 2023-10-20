namespace Application.DTOs.EvidenciasMuestreo
{
    public class InformacionEvidenciaExcelDto
    {
        public string LatitudAforo { get; set; }
        public string LongitudAforo { get; set; }
        public string Placas { get; set; }
        public string Laboratorio { get; set; }
        public string LaboratorioNomenclatura { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFinal { get; set; }
        public string ClaveMuestreo { get; set; }

        public InformacionEvidenciaExcelDto()
        {
            this.LatitudAforo = string.Empty;
            this.LongitudAforo = string.Empty;
            this.Placas = string.Empty;
            this.Laboratorio = string.Empty;
            this.LaboratorioNomenclatura = string.Empty;
            this.FechaInicio = string.Empty;
            this.FechaFinal = string.Empty;
            this.HoraInicio = string.Empty;
            this.HoraFinal = string.Empty;
            this.ClaveMuestreo = string.Empty;
        }
    }
}
