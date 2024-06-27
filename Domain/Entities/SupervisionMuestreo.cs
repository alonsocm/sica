using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class SupervisionMuestreo
{
    /// <summary>
    /// Identificador principal se la tabla SupervisionMuestreo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la fecha muestreo
    /// </summary>
    public DateTime FehaMuestreo { get; set; }

    /// <summary>
    /// Campo que indica la hora de inicio
    /// </summary>
    public TimeSpan HoraInicio { get; set; }

    /// <summary>
    /// Campo que indica la hora de termino
    /// </summary>
    public TimeSpan HoraTermino { get; set; }

    /// <summary>
    /// Campo que indica la hora de la toma de muestra
    /// </summary>
    public TimeSpan HoraTomaMuestra { get; set; }

    /// <summary>
    /// Campo que indica el puntaje
    /// </summary>
    public decimal PuntajeObtenido { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de CuencasDireccionesLocales describiendo el organismo dirección que reporta el muestreo
    /// </summary>
    public long OrganismosDireccionesRealizaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de OrganismosCuenca
    /// </summary>
    public long OrganismoCuencaReportaId { get; set; }

    /// <summary>
    /// Campo que describe el supervisor de Conagua
    /// </summary>
    public string SupervisorConagua { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de Sitio
    /// </summary>
    public long SitioId { get; set; }

    /// <summary>
    /// Campo que describe la clave de muestreo
    /// </summary>
    public string ClaveMuestreo { get; set; } = null!;

    /// <summary>
    /// Campo que describe la latitud de toma del muestreo
    /// </summary>
    public double LatitudToma { get; set; }

    /// <summary>
    /// Campo que describe la longitud de toma del muestreo
    /// </summary>
    public double LongitudToma { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo Laboratorios describiendo el laboratorio que realizo la supervisión
    /// </summary>
    public long LaboratorioRealizaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Muestradores indicando el responsable de la toma
    /// </summary>
    public int ResponsableTomaId { get; set; }

    /// <summary>
    /// Llave foránea que indica el responsable de las mediciones del muestreo
    /// </summary>
    public int ResponsableMedicionesId { get; set; }

    /// <summary>
    /// Campo que describe las observaciones del muestreo
    /// </summary>
    public string? ObservacionesMuestreo { get; set; }

    /// <summary>
    /// Campo que describe la fecha en la que se realizo el registro
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la tabla de Usuario describiendo el usuario que registro la supervisión
    /// </summary>
    public long UsuarioRegistroId { get; set; }

    public virtual ICollection<EvidenciaSupervisionMuestreo> EvidenciaSupervisionMuestreo { get; set; } = new List<EvidenciaSupervisionMuestreo>();

    public virtual Laboratorios LaboratorioRealiza { get; set; } = null!;

    public virtual OrganismoCuenca OrganismoCuencaReporta { get; set; } = null!;

    public virtual CuencaDireccionesLocales OrganismosDireccionesRealiza { get; set; } = null!;

    public virtual Muestreadores ResponsableMediciones { get; set; } = null!;

    public virtual Muestreadores ResponsableToma { get; set; } = null!;

    public virtual Sitio Sitio { get; set; } = null!;

    public virtual Usuario UsuarioRegistro { get; set; } = null!;

    public virtual ICollection<ValoresSupervisionMuestreo> ValoresSupervisionMuestreo { get; set; } = new List<ValoresSupervisionMuestreo>();
}
