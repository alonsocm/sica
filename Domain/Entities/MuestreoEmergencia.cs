using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MuestreoEmergencia
{
    /// <summary>
    /// Identificador principal de la tabla de MuestreoEmergencia
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que indica el número de muestreo de emergencia
    /// </summary>
    public string? Numero { get; set; }

    /// <summary>
    /// Campo que indica el nombre de la emergencia
    /// </summary>
    public string NombreEmergencia { get; set; } = null!;

    /// <summary>
    /// Campo que indica la clave única
    /// </summary>
    public string ClaveUnica { get; set; } = null!;

    /// <summary>
    /// Campo que indica el IdLaboratorio, dato de laboratorio
    /// </summary>
    public string? IdLaboratorio { get; set; }

    /// <summary>
    /// Campo que indica el sitio del muestreo de emergencia
    /// </summary>
    public string Sitio { get; set; } = null!;

    /// <summary>
    /// Campo que indica la fecha programada del muestreo de emergencia
    /// </summary>
    public DateTime FechaProgramada { get; set; }

    /// <summary>
    /// Campo que indica la fecha real visita del muestreo de emergencia
    /// </summary>
    public DateTime FechaRealVisita { get; set; }

    /// <summary>
    /// Campo que indica la hora del muestreo de emergencia
    /// </summary>
    public string? HoraMuestreo { get; set; }

    /// <summary>
    /// Campo que indica el tipo de cuerpo de agua del muestreo de emergencia
    /// </summary>
    public string TipoCuerpoAgua { get; set; } = null!;

    /// <summary>
    /// Campo que indica el subtipo de cuerpo de agua del muestreo de emergencia
    /// </summary>
    public string? SubtipoCuerpoAgua { get; set; }

    /// <summary>
    /// Campo que indica el laboratorio que realizo el muestreo de emergencia
    /// </summary>
    public string LaboratorioRealizoMuestreo { get; set; } = null!;

    /// <summary>
    /// Campo que indica el laboratorio subrogado del muestreo de emergencia
    /// </summary>
    public string? LaboratorioSubrogado { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Parámetros indicando el parámetro del muestreo de emergencia
    /// </summary>
    public long ParametroId { get; set; }

    /// <summary>
    /// Campo que indica el resultado
    /// </summary>
    public string Resultado { get; set; } = null!;

    /// <summary>
    /// Campo que indica el resultado sustituido por el limite
    /// </summary>
    public string? ResultadoSustituidoPorLimite { get; set; }

    public virtual ICollection<HistorialSustitucionEmergencia> HistorialSustitucionEmergencia { get; set; } = new List<HistorialSustitucionEmergencia>();

    public virtual ParametrosGrupo Parametro { get; set; } = null!;
}
