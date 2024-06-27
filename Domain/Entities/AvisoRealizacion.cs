using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class AvisoRealizacion
{
    /// <summary>
    /// Identificador de la tabla AvisoRealizacion
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la clave muestreo
    /// </summary>
    public string ClaveMuestreo { get; set; } = null!;

    /// <summary>
    /// Campo que describe la clave sitio
    /// </summary>
    public string ClaveSitio { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace relación al catálogo tipo de sitio
    /// </summary>
    public long TipoSitioId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Laboratorios
    /// </summary>
    public long LaboratorioId { get; set; }

    /// <summary>
    /// Campo que describe si cuenta con eventualidades
    /// </summary>
    public bool ConEventualidades { get; set; }

    /// <summary>
    /// Campo que describe la fecha programada
    /// </summary>
    public DateTime FechaProgramada { get; set; }

    /// <summary>
    /// Campo que describe la fecha real de visita
    /// </summary>
    public DateTime FechaRealVisita { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Brigada de muestreo
    /// </summary>
    public long BrigadaMuestreoId { get; set; }

    /// <summary>
    /// Campo que describe si cuenta con QC el muestreo
    /// </summary>
    public bool ConQcmuestreo { get; set; }

    /// <summary>
    /// Campo que describe el folio de la eventualidad
    /// </summary>
    public string? FolioEventualidad { get; set; }

    /// <summary>
    /// Campo que describe la fecha de aprobación de la eventualidad
    /// </summary>
    public DateTime? FechaAprobacionEventualidad { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Tipo de supervisión
    /// </summary>
    public int TipoSupervisionId { get; set; }

    /// <summary>
    /// Campo que describe el documento de la eventualidad
    /// </summary>
    public string? DocumentoEventualidad { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Tipo de eventualidad
    /// </summary>
    public string? TipoEventualidad { get; set; }

    /// <summary>
    /// Campo que describe la fecha de reprogramación
    /// </summary>
    public DateTime? FechaReprogramacion { get; set; }

    public virtual BrigadaMuestreo BrigadaMuestreo { get; set; } = null!;

    public virtual Laboratorios Laboratorio { get; set; } = null!;

    public virtual TipoSitio TipoSitio { get; set; } = null!;

    public virtual TipoSupervision TipoSupervision { get; set; } = null!;

    public virtual ICollection<ValidacionEvidencia> ValidacionEvidencia { get; set; } = new List<ValidacionEvidencia>();
}
