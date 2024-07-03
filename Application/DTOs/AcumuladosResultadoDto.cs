namespace Application.DTOs
{
    public class AcumuladosResultadoDto : MuestreoDto
    {
        public string ClaveUnica { get; set; }
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
        public string ValidacionFinal { get; set; }
        public string ObservacionFinal { get; set; }

        public AcumuladosResultadoDto()
        {
            ClaveUnica = string.Empty;
            SubTipoCuerpoAgua = string.Empty;
            LaboratorioRealizoMuestreo = string.Empty;
            SubGrupo = string.Empty;
            ClaveParametro = string.Empty;
            Parametro = string.Empty;
            UnidadMedida = string.Empty;
            Resultado = string.Empty;
            ZonaEstrategica = string.Empty;
            GrupoParametro = string.Empty;
            IdResultadoLaboratorio = 0;
            FechaEntrega = string.Empty;
            NuevoResultadoReplica = string.Empty;
            Replica = false;
            CambioResultado = false;
            DiferenciaDias = 0;
            FechaEntregaTeorica = string.Empty;
            NumParametrosEsperados = 0;
            NumParametrosCargados = 0;
            MuestreoCompletoPorResultados = string.Empty;
            ValidadoReglas = false;
            CostoParametro = 0;
            AnioOperacion = 0;
            CumpleReglasCondic = string.Empty;
            TipoCuerpoAguaId = 0;
            TipoSitioId = 0;
            CumpleFechaEntrega = string.Empty;
            ResultadoReglas = string.Empty;
            LstParametros = new List<ParametrosDto>();
            ResultadoMuestreoId = 0;
            CumpleTodosCriterios = false;
            AutorizacionFechaEntrega = false;
            ValidacionFinal = string.Empty;
            ObservacionFinal = string.Empty;
        }
    }
}
