using Domain.Entities;

namespace Application.DTOs
{
    public class ResultadoMuestreoDto
    {
        public long Id { get; set; }
        public long MuestreoId { get; set; }
        public long ParametroId { get; set; }
        public string? Resultado { get; set; }
        //Observaciones se utiliza para observaciones de ocdl
        public string? Observaciones { get; set; }
        public string NoEntregaOCDL { get; set; }
        public string? ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string NombreSitio { get; set; }
        public string? ClaveParametro { get; set; }
        public string Laboratorio { get; set; }
        //public string? TipoCuerpoAgua{ get; set; }
        public string? TipoCuerpoAguaOriginal { get; set; }
        public string TipoAprobacion { get; set; }
        public string? EsCorrectoResultado { get; set; }
        public string? FechaRevision { get; set; }
        public string? FechaLimiteRevision { get; set; }
        public DateTime? fechaLimiteRevisionVencidos { get; set; }
        public string NombreUsuario { get; set; }
        public string EstatusResultado { get; set; }
        public int EstatusId { get; set; }
        public long CuerpoTipoSubtipo { get; set; }
        public string OrganismoCuenca { get; set; }
        public string DireccionLocal { get; set; }
        public string? FechaRealizacion { get; set; }

        //se agrego para observaciones secaia
        public string? ObservacionSECAIA { get; set; }
        public long? ObservacionSECAIAId { get; set; }

        public List<ParametrosDto> lstParametros { get; set; }
        public List<EvidenciaDto> lstEvidencias { get; set; }
        public List<string> lstParametrosTotalResultado { get; set; }

        public int? EstatusOCDL { get; set; }
        public int? EstatusSECAIA { get; set; }

        public long TipoSitioId { get; set; }
        public string TipoSitio { get; set; }

        public long? CuerpoAguaId { get; set; }
        public string? CuerpoAgua { get; set; }
        public string? TipoCuerpoAgua { get; set; }
        public long? TipoCuerpoAguaId { get; set; }
        public long TipoHomologadoId { get; set; }
        public string? TipoHomologado { get; set; }
        public string ClaveSitioOriginal { get; set; }

        public ResultadoMuestreoDto()
        {
            lstParametros = new List<ParametrosDto>();
            lstEvidencias = new List<EvidenciaDto>();
            lstParametrosTotalResultado = new List<string>();
            this.EstatusOCDL = null;
            this.EstatusSECAIA = null;
            this.ObservacionSECAIAId = null;
            this.TipoSitio = string.Empty;
            this.TipoSitioId = 0;
            this.CuerpoAguaId = null;
            this.CuerpoAgua = null;
            this.TipoCuerpoAgua = null;
            this.TipoCuerpoAguaId = null;
            this.TipoHomologadoId = 0;
            this.TipoHomologado = string.Empty;
            this.ClaveSitioOriginal = string.Empty;
            this.fechaLimiteRevisionVencidos = null;
        }
    }

    public class ResultadoDescargaDto
    {
        public string noEntregaOCDL { get; set; }
        public string ocdl { get; set; }
        public string nombreSitio { get; set; }
        public string claveMonitoreo { get; set; }
        public string fechaRealizacion { get; set; }
        public string laboratorio { get; set; }
        public string CuerpoAgua { get; set; }
        public string tipoCuerpoAgua { get; set; }
        public string tipoHomologado { get; set; }
        public string tipoSitio { get; set; }
        public string claveSitio { get; set; }
        public string claveSitioOriginal { get; set; }
        public List<ParametrosDto> lstParametros { get; set; }
        public List<ColumnaDto> lstParametrosOrden { get; set; }

        public ResultadoDescargaDto()
        {
            this.noEntregaOCDL = string.Empty;
            this.ocdl = string.Empty;
            this.nombreSitio = string.Empty;
            this.claveMonitoreo = string.Empty;
            this.fechaRealizacion = string.Empty;
            this.laboratorio = string.Empty;
            this.CuerpoAgua = string.Empty;
            this.tipoCuerpoAgua = string.Empty;
            this.tipoHomologado = string.Empty;
            this.tipoSitio = string.Empty;
            this.claveSitio = string.Empty;
            this.claveSitioOriginal = string.Empty;
            this.lstParametros = new List<ParametrosDto>();
            this.lstParametrosOrden = new List<ColumnaDto>();
        }
    }

    public class ColumnaDto
    {
        public string nombre { get; set; }
        public string etiqueta { get; set; }
        public long orden { get; set; }
        public ColumnaDto()
        {
            this.nombre = string.Empty;
            this.etiqueta = string.Empty;
            this.orden = 0;
        }
    }
}