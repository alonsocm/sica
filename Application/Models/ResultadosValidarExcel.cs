namespace Application.Models
{
    public class ResultadosValidarExcel
    {
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string NombreSitio { get; set; }
        public string FechaRealizacion { get; set; }
        public string FechaProgramada { get; set; }
        public int DiferenciaDias { get; set; }
        public string FechaEntregaTeorica { get; set; }
        public string LaboratorioRealizoMuestreo { get; set; }
        public string CuerpoAgua { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string SubTipoCuerpoAgua { get; set; }
        public int NumParametrosEsperados { get; set; }
        public int NumParametrosCargados { get; set; }
        public string MuestreoCompletoPorResultados { get; set; }
        public string CumpleReglasCond { get; set; }
        public string Observaciones { get; set; }
        public string CumpleFechaEntrega { get; set; }
        public string CumpleTodosCriteriosAplicarReglas { get; set; }
        public string AutorizacionIncompleto { get; set; }
        public string AutorizacionFechaEntrega { get; set; }
        public string CorreReglaValidacion { get; set; }

        public ResultadosValidarExcel()
        {
            this.ClaveSitio = string.Empty;
            this.ClaveMonitoreo = string.Empty;
            this.NombreSitio = string.Empty;
            this.FechaRealizacion = string.Empty;
            this.FechaProgramada = string.Empty;
            this.DiferenciaDias = 0;
            this.FechaEntregaTeorica = string.Empty;
            this.LaboratorioRealizoMuestreo = string.Empty;
            this.CuerpoAgua = string.Empty;
            this.TipoCuerpoAgua = string.Empty;
            this.SubTipoCuerpoAgua = string.Empty;
            this.NumParametrosEsperados = 0;
            this.NumParametrosCargados = 0;
            this.MuestreoCompletoPorResultados = string.Empty;
            this.CumpleReglasCond = string.Empty;
            this.Observaciones = string.Empty;
            this.CumpleFechaEntrega = string.Empty;
            this.CumpleTodosCriteriosAplicarReglas = string.Empty;
            this.AutorizacionIncompleto = string.Empty;
            this.AutorizacionFechaEntrega = string.Empty;
            this.CorreReglaValidacion = string.Empty;
        }
    }
}
