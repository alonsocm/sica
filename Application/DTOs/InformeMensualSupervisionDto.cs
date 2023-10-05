namespace Application.DTOs
{
    public class InformeMensualSupervisionDto
    {
        public string Oficio { get; set; }
        public string Lugar { get; set; }
        public string Fecha { get; set; }
        public string DireccionTecnica { get; set; }
        public string GerenteCalidadAgua { get; set; }
        public string MesReporte { get; set; }
        public List<string> Atencion { get; set; }
        public string Contrato { get; set; }
        public string DenominacionContrato { get; set; }
        public string NumeroSitios { get; set; }
        public string Indicaciones { get; set; }
        public List<ResultadoInformeDto> Resultados { get; set; }

        public string DireccionOC { get; set; }
        public string TelefonoOC { get; set; }

        public InformeMensualSupervisionDto()
        {
            this.Oficio = string.Empty;
            this.Lugar = string.Empty;
            this.Fecha = string.Empty;
            this.DireccionTecnica = string.Empty;
            this.GerenteCalidadAgua = string.Empty;
            this.MesReporte = string.Empty;
            this.Atencion = new List<string>();
            this.Contrato = string.Empty;
            this.DenominacionContrato = string.Empty;
            this.NumeroSitios = string.Empty;
            this.Indicaciones = string.Empty;
            this.Resultados = new List<ResultadoInformeDto>();
            this.DireccionOC = string.Empty;
            this.TelefonoOC = string.Empty;
        }
    }

}
