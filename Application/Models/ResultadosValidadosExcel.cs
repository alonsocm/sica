namespace Application.Models
{
    public class ResultadosValidadosExcel
    {
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string NombreSitio { get; set; }
        public string FechaRealizacion { get; set; }
        public string FechaProgramada { get; set; }
        public string LaboratorioRealizoMuestreo { get; set; }
        public string CuerpoAgua { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string SubTipoCuerpoAgua { get; set; }
        public string MuestreoCompletoPorResultados { get; set; }

        public ResultadosValidadosExcel()
        {
            ClaveSitio = string.Empty;
            ClaveMonitoreo = string.Empty;
            NombreSitio = string.Empty;
            FechaRealizacion = string.Empty;
            FechaProgramada = string.Empty;
            LaboratorioRealizoMuestreo = string.Empty;
            CuerpoAgua = string.Empty;
            TipoCuerpoAgua = string.Empty;
            SubTipoCuerpoAgua = string.Empty;
            MuestreoCompletoPorResultados = string.Empty;
        }
    }
}
