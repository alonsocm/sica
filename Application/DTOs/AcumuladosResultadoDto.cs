namespace Application.DTOs
{
    public class AcumuladosResultadoDto : MuestreoDto
    {

        public string ClaveUnica { get; set; }
        public string SubTipoCuerpoAgua { get; set; }
        public string LaboratorioRealizoMuestreo { get; set; }
        public string GrupoParametro { get; set; }
        public string SubGrupo { get; set; }
        public string ClaveParametro { get; set; }
        public string Parametro { get; set; }
        public string? UnidadMedida { get; set; }
        public string Resultado { get; set; }
        public string ZonaEstrategica { get; set; }
        public long IdResultadoLaboratorio { get; set; }
        public string FechaEntrega { get; set; }
        public string NuevoResultadoReplica { get; set; }
        public bool Replica { get; set; }
        public bool CambioResultado { get; set; }
        public int DiferenciaDias { get; set; }
        public string FechaEntregaTeorica { get; set; }
        public int NumParametrosEsperados { get; set; }
        public int NumParametrosCargados { get; set; }
        public string MuestreoCompletoPorResultados { get; set; }
        public bool ValidadoReglas { get; set; }
        public decimal CostoParametro { get; set; }
        public int AnioOperacion { get; set; }
        public string CumpleReglasCondic { get; set; }
        public long TipoCuerpoAguaId { get; set; }
        public long TipoSitioId { get; set; }
        public string CumpleFechaEntrega { get; set; }
        public string ResultadoReglas { get; set; }
        public List<ParametrosDto> LstParametros { get; set; }
        public long ResultadoMuestreoId { get; set; }
        public bool CumpleTodosCriterios { get; set; }
        public bool AutorizacionFechaEntrega { get; set; }
        public string ValidacionFinal { get; set; }
        public string ObservacionFinal { get; set; }

        public AcumuladosResultadoDto()
        {
            this.ClaveUnica = string.Empty;
            this.SubTipoCuerpoAgua = string.Empty;
            this.LaboratorioRealizoMuestreo = string.Empty;
            this.SubGrupo = string.Empty;
            this.ClaveParametro = string.Empty;
            this.Parametro = string.Empty;
            this.UnidadMedida = string.Empty;
            this.Resultado = string.Empty;
            this.ZonaEstrategica = string.Empty;
            this.GrupoParametro = string.Empty;
            this.IdResultadoLaboratorio = 0;
            this.FechaEntrega = string.Empty;
            this.NuevoResultadoReplica = string.Empty;
            this.Replica = false;
            this.CambioResultado = false;
            this.DiferenciaDias = 0;
            this.FechaEntregaTeorica = string.Empty;
            this.NumParametrosEsperados = 0;
            this.NumParametrosCargados = 0;
            this.MuestreoCompletoPorResultados = string.Empty;
            this.ValidadoReglas = false;
            this.CostoParametro = 0;
            this.AnioOperacion = 0;
            this.CumpleReglasCondic = string.Empty;

            this.TipoCuerpoAguaId = 0;
            this.TipoSitioId = 0;

            this.CumpleFechaEntrega = string.Empty;
            this.ResultadoReglas = string.Empty;

            this.LstParametros = new List<ParametrosDto>();
            this.ResultadoMuestreoId = 0;
            this.CumpleTodosCriterios = false;
            this.AutorizacionFechaEntrega = false;
            this.ValidacionFinal = string.Empty;
            this.ObservacionFinal = string.Empty;

        }
    }
}
