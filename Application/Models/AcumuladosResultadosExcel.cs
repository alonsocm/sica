namespace Application.Models
{
    public class AcumuladosResultadosExcel
    {
        public string ClaveUnica { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string ClaveSitio { get; set; }
        public string NombreSitio { get; set; }
        public string FechaProgramada { get; set; }
        public string FechaRealizacion { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string TipoSitio { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string SubTipoCuerpoAgua { get; set; }
        public string Laboratorio { get; set; }
        public string LaboratorioRealizoMuestreo { get; set; }
        public string LaboratorioSubrogado { get; set; }
        public string GrupoParametro { get; set; }
        public string SubGrupo { get; set; }
        public string ClaveParametro { get; set; }
        public string Parametro { get; set; }
        public string UnidadMedida { get; set; }
        public string Resultado { get; set; }
        public string NuevoResultadoReplica { get; set; }
        public string ProgramaAnual { get; set; }
        public long IdResultadoLaboratorio { get; set; }
        public string FechaEntrega { get; set; }
        public string Replica { get; set; }
        public string CambioResultado { get; set; }
        public AcumuladosResultadosExcel()
        {
            ClaveUnica = string.Empty;
            ClaveMonitoreo = string.Empty;
            ClaveSitio = string.Empty;
            NombreSitio = string.Empty;
            FechaProgramada = string.Empty;
            FechaRealizacion = string.Empty;
            HoraInicio = string.Empty;
            HoraFin = string.Empty;
            TipoSitio = string.Empty;
            TipoCuerpoAgua = string.Empty;
            SubTipoCuerpoAgua = string.Empty;
            Laboratorio = string.Empty;
            LaboratorioRealizoMuestreo = string.Empty;
            LaboratorioSubrogado = string.Empty;
            GrupoParametro = string.Empty;
            SubGrupo = string.Empty;
            ClaveParametro = string.Empty;
            Parametro = string.Empty;
            UnidadMedida = string.Empty;
            Resultado = string.Empty;
            NuevoResultadoReplica = string.Empty;
            ProgramaAnual = string.Empty;
            IdResultadoLaboratorio = 0;
            FechaEntrega = string.Empty;
            Replica = string.Empty;
            CambioResultado = string.Empty;
        }
    }
}
