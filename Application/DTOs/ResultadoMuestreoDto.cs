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
        public string NoEntregaOCDL { get; set; } = string.Empty;
        public string? ClaveUnica { get; set; }
        public string ClaveSitio { get; set; } = string.Empty;
        public string ClaveMonitoreo { get; set; } = string.Empty;
        public string NombreSitio { get; set; } = string.Empty;
        public string? ClaveParametro { get; set; }
        public string Laboratorio { get; set; } = string.Empty;
        //public string? TipoCuerpoAgua{ get; set; }
        public string? TipoCuerpoAguaOriginal { get; set; }
        public string TipoAprobacion { get; set; } = string.Empty;
        public string? EsCorrectoResultado { get; set; }
        public string? FechaRevision { get; set; }
        public string? FechaLimiteRevision { get; set; }
        public DateTime? fechaLimiteRevisionVencidos { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string EstatusResultado { get; set; } = string.Empty;
        public int EstatusId { get; set; }
        public long CuerpoTipoSubtipo { get; set; }
        public string OrganismoCuenca { get; set; } = string.Empty;
        public string DireccionLocal { get; set; } = string.Empty;
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
            EstatusOCDL = null;
            EstatusSECAIA = null;
            ObservacionSECAIAId = null;
            TipoSitio = string.Empty;
            TipoSitioId = 0;
            CuerpoAguaId = null;
            CuerpoAgua = null;
            TipoCuerpoAgua = null;
            TipoCuerpoAguaId = null;
            TipoHomologadoId = 0;
            TipoHomologado = string.Empty;
            ClaveSitioOriginal = string.Empty;
            fechaLimiteRevisionVencidos = null;
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
            noEntregaOCDL = string.Empty;
            ocdl = string.Empty;
            nombreSitio = string.Empty;
            claveMonitoreo = string.Empty;
            fechaRealizacion = string.Empty;
            laboratorio = string.Empty;
            CuerpoAgua = string.Empty;
            tipoCuerpoAgua = string.Empty;
            tipoHomologado = string.Empty;
            tipoSitio = string.Empty;
            claveSitio = string.Empty;
            claveSitioOriginal = string.Empty;
            lstParametros = new List<ParametrosDto>();
            lstParametrosOrden = new List<ColumnaDto>();
        }
    }

    public class ColumnaDto
    {
        public string nombre { get; set; }
        public string etiqueta { get; set; }
        public long orden { get; set; }
        public ColumnaDto()
        {
            nombre = string.Empty;
            etiqueta = string.Empty;
            orden = 0;
        }
    }
}