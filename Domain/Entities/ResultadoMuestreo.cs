using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ResultadoMuestreo
{
    /// <summary>
    /// Identificador principal de la tabla ResultadoMuestreo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la Tabla de muestreo
    /// </summary>
    public long MuestreoId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catalogo de ParametroGrupo indicando el parámetro
    /// 
    /// </summary>
    public long ParametroId { get; set; }

    /// <summary>
    /// Campo que describe el resultado del parametro del muestreo, cargado mediante ebaseca
    /// </summary>
    public string Resultado { get; set; } = null!;

    /// <summary>
    /// Campo que indica las observaciones de OCDL
    /// </summary>
    public string? ObservacionesOcdl { get; set; }

    /// <summary>
    /// Campo que indica si es correcto por parte de OCDL
    /// </summary>
    public bool? EsCorrectoOcdl { get; set; }

    /// <summary>
    /// Llave foránea hace referencia al catalogo de observaciones indicando las observaciones de OCDL
    /// </summary>
    public long? ObservacionesOcdlid { get; set; }

    /// <summary>
    /// Llave foránea hace referencia al catalogo de observaciones indicando las observaciones de SECAIA
    /// </summary>
    public long? ObservacionesSecaiaid { get; set; }

    /// <summary>
    /// Campo que indica si es correcto por parte de SECAIA
    /// </summary>
    public bool? EsCorrectoSecaia { get; set; }

    /// <summary>
    /// Campo que describe las observaciones por parte de SECAIA
    /// </summary>
    public string? ObservacionesSecaia { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Estatus descibiendo el estatus del resultado
    /// </summary>
    public int? EstatusResultado { get; set; }

    /// <summary>
    /// Llave foranea que hace referencia al catalogo de EstatusResultado indicando el estatus en el que se encuentra el resultado del muestreo
    /// </summary>
    public int? EstatusResultadoId { get; set; }

    /// <summary>
    /// Llave foránea hace referencia al catalogo de observaciones indicando las observaciones de SRENAMECA
    /// </summary>
    public long? ObservacionSrenamecaid { get; set; }

    /// <summary>
    /// Campo que describe las observaciones por parte de SRENAMECA
    /// </summary>
    public string? ObservacionSrenameca { get; set; }

    /// <summary>
    /// Campoq eu indica la fecha en la que se realizaron las observaciones por parte de SRENAMECA
    /// </summary>
    public DateTime? FechaObservacionSrenameca { get; set; }

    /// <summary>
    /// Campo que describe si se aprueba el resultado de replica
    /// </summary>
    public bool? SeApruebaResultadoReplica { get; set; }

    /// <summary>
    /// Campo que indica la fecha del estatus final
    /// </summary>
    public DateTime? FechaEstatusFinal { get; set; }

    /// <summary>
    /// Campo que indica el resultado actualizado de replica
    /// </summary>
    public string? ResultadoActualizadoReplica { get; set; }

    /// <summary>
    /// Campo que indica las causas de rechazo
    /// </summary>
    public string? CausaRechazo { get; set; }

    /// <summary>
    /// Campo que describe si se acepta el rechazo
    /// </summary>
    public bool? SeAceptaRechazoSiNo { get; set; }

    /// <summary>
    /// Campo que describe el resultado de la replica
    /// </summary>
    public string? ResultadoReplica { get; set; }

    /// <summary>
    /// Campo que indica si es el mismo resultado
    /// </summary>
    public bool? EsMismoResultado { get; set; }

    /// <summary>
    /// Campoq ue describe las observaciones por parte del laboratorio
    /// </summary>
    public string? ObservacionLaboratorio { get; set; }

    /// <summary>
    /// Campo que describe la fecha de replica de laboratorio
    /// </summary>
    public DateTime? FechaReplicaLaboratorio { get; set; }

    /// <summary>
    /// Camp que describe los comentarios
    /// </summary>
    public string? Comentarios { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de Laboratorio
    /// </summary>
    public long LaboratorioId { get; set; }

    /// <summary>
    /// Campo que describe la fecha de entrega
    /// </summary>
    public DateTime FechaEntrega { get; set; }

    /// <summary>
    /// Campo que describe el id resultado de laboratorio es carado mediante ebaseca
    /// </summary>
    public long IdResultadoLaboratorio { get; set; }

    /// <summary>
    /// Campo que describe el resultado de las reglas
    /// </summary>
    public string? ResultadoReglas { get; set; }

    /// <summary>
    /// Campo que describe el resultado sustituido por limite
    /// </summary>
    public string? ResultadoSustituidoPorLimite { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de LaboratorioSubrogado
    /// </summary>
    public long? LaboratorioSubrogadoId { get; set; }

    /// <summary>
    /// Campo que describe el resultado sustituido por laboratorio
    /// </summary>
    public string? ResultadoSustituidoPorLaboratorio { get; set; }

    /// <summary>
    /// Campo que describe si aprueba la validación final que se realiza despues de haber aplicado las reglas
    /// </summary>
    public bool? ValidacionFinal { get; set; }

    /// <summary>
    /// Campo que describe las observaciones de validación final que se realiza despues de haber aplicado las reglas
    /// </summary>
    public string? ObservacionFinal { get; set; }

    public virtual ICollection<AprobacionResultadoMuestreo> AprobacionResultadoMuestreo { get; set; } = new List<AprobacionResultadoMuestreo>();

    public virtual EstatusResultado? EstatusResultado1 { get; set; }

    public virtual EstatusMuestreo1? EstatusResultadoNavigation { get; set; }

    public virtual ICollection<EvidenciaReplica> EvidenciaReplica { get; set; } = new List<EvidenciaReplica>();

    public virtual Laboratorios Laboratorio { get; set; } = null!;

    public virtual Laboratorios? LaboratorioSubrogado { get; set; }

    public virtual Muestreo Muestreo { get; set; } = null!;

    public virtual Observaciones? ObservacionSrenamecaNavigation { get; set; }

    public virtual Observaciones? ObservacionesOcdlNavigation { get; set; }

    public virtual Observaciones? ObservacionesSecaiaNavigation { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual ICollection<ReplicasResultadosReglasValidacion> ReplicasResultadosReglasValidacion { get; set; } = new List<ReplicasResultadosReglasValidacion>();
}
