namespace Domain.Entities;

public partial class TipoCuerpoAgua
{
    public string TipoHomologadoDescripcion;

    /// <summary>
    /// Identificador principal de la tabla TipoCuerpoAgua
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el tipo de cuerpo de agua 
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace relación a la tabla de TipoHomologado
    /// </summary>
    public long? TipoHomologadoId { get; set; }

    /// <summary>
    /// Campo que describe si se encuentra activo el tipo cuerpo de agua
    /// </summary>
    public bool Activo { get; set; } = true;

    /// <summary>
    /// Campo que describe la frecuencia del tipo de cuerpo de agua para el muestreo
    /// </summary>
    public string? Frecuencia { get; set; }

    /// <summary>
    /// Campo que describe las evidencias esperadas conforme al tipo de cuerpo de agua
    /// </summary>
    public int EvidenciasEsperadas { get; set; }

    /// <summary>
    /// Campo que describe el tiempo mínimo del muestreo en minutos
    /// </summary>
    public int TiempoMinimoMuestreo { get; set; }



    public virtual ICollection<CuerpoTipoSubtipoAgua> CuerpoTipoSubtipoAgua { get; set; } = new List<CuerpoTipoSubtipoAgua>();

    public virtual ICollection<ParametrosSitioTipoCuerpoAgua> ParametrosSitioTipoCuerpoAgua { get; set; } = new List<ParametrosSitioTipoCuerpoAgua>();

    public virtual ICollection<ReglaReporteResultadoTca> ReglaReporteResultadoTca { get; set; } = new List<ReglaReporteResultadoTca>();

    public virtual TipoHomologado? TipoHomologado { get; set; }


}
