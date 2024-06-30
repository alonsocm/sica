using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Muestreo
{
    /// <summary>
    /// Identificador  de Muestreo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la tabla tabla de ProgramaMuestreo
    /// </summary>
    public long ProgramaMuestreoId { get; set; }

    /// <summary>
    /// Campo que describe la fecha en la que se cargo el muesreo a traves del archivo ebaseca
    /// </summary>
    public DateTime FechaCarga { get; set; }

    /// <summary>
    /// Campo que describe la fecha real de visita proviene de ebaseca
    /// </summary>
    public DateTime? FechaRealVisita { get; set; }

    /// <summary>
    /// Campo que indica la hora de inicio proviene de ebaseca
    /// </summary>
    public TimeSpan? HoraInicio { get; set; }

    /// <summary>
    /// Campo que describe la hora fin, proviene de ebseca
    /// </summary>
    public TimeSpan? HoraFin { get; set; }

    /// <summary>
    /// Campo que describe la fecha limite de revisión
    /// </summary>
    public DateTime? FechaLimiteRevision { get; set; }

    /// <summary>
    /// Campo que indica el año de operación
    /// </summary>
    public int? AnioOperacion { get; set; }

    /// <summary>
    /// Campo que indica el número de entrega
    /// </summary>
    public int? NumeroEntrega { get; set; }
    public int? NumeroCarga { get; set; }


    

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Estatus, indicando el estatus del muestreo
    /// </summary>
    public int EstatusId { get; set; }

    /// <summary>
    /// Campo que indca la fecha de revisión por OCDL
    /// </summary>
    public DateTime? FechaRevisionOcdl { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la tabla de Usuario indicando quien fue el que reviso a nivel OCDL
    /// </summary>
    public long? UsuarioRevisionOcdlid { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de TipoAprobacion
    /// </summary>
    public long? TipoAprobacionId { get; set; }

    /// <summary>
    /// Campo que indica el estatus referente a revisión de OCDL
    /// </summary>
    public int? EstatusOcdl { get; set; }

    /// <summary>
    /// Campo que indica el estatus referente a revisión de SECAIA
    /// </summary>
    public int? EstatusSecaia { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la tabla de Usuario indicando quien fue el que reviso a nivel SECAIA
    /// </summary>
    public long? UsuarioRevisionSecaiaid { get; set; }

    /// <summary>
    /// Campo que indca la fecha de revisión por SECAIA
    /// </summary>
    public DateTime? FechaRevisionSecaia { get; set; }

    /// <summary>
    /// Campo que indica la fecha en la que se realizo la carga de las evidencias de muestreo
    /// </summary>
    public DateTime? FechaCargaEvidencias { get; set; }

    /// <summary>
    /// Campo que indica si se envia a la etapa de validación de evidencias
    /// </summary>
    public bool ValidacionEvidencias { get; set; }

    public int TipoCargaId { get; set; }

    /// <summary>
    /// Campo que indica si fue autorizado el muestreo estando incompletos los resultados del muestreo
    /// </summary>
    public bool AutorizacionIncompleto { get; set; }

    /// <summary>
    /// Campo que indica si se autorizo ya que la fecha de entrega no se cumplio
    /// </summary>
    public bool AutorizacionFechaEntrega { get; set; }

    public virtual EstatusMuestreo Estatus { get; set; } = null!;

    public virtual EstatusMuestreo? EstatusOcdlNavigation { get; set; }

    public virtual EstatusMuestreo? EstatusSecaiaNavigation { get; set; }

    public virtual ICollection<EvidenciaMuestreo> EvidenciaMuestreo { get; set; } = new List<EvidenciaMuestreo>();

    public virtual ICollection<HistorialSustitucionLimites> HistorialSustitucionLimites { get; set; } = new List<HistorialSustitucionLimites>();

    public virtual ProgramaMuestreo ProgramaMuestreo { get; set; } = null!;

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; } = new List<ResultadoMuestreo>();

    public virtual TipoAprobacion? TipoAprobacion { get; set; }

    public virtual TipoCarga TipoCarga { get; set; } = null!;

    public virtual Usuario? UsuarioRevisionOcdl { get; set; }

    public virtual Usuario? UsuarioRevisionSecaia { get; set; }

    public virtual ICollection<ValidacionEvidencia> ValidacionEvidencia { get; set; } = new List<ValidacionEvidencia>();
}
