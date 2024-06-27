using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Emergencia
{
    /// <summary>
    /// Identificador de la tabla de Emergencias
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que indica el año de la emergencia
    /// </summary>
    public int Anio { get; set; }

    /// <summary>
    /// Campo que indica el nombre de la emergencia
    /// </summary>
    public string NombreEmergencia { get; set; } = null!;

    /// <summary>
    /// Campo que indica la clave sitio
    /// </summary>
    public string? ClaveSitio { get; set; }

    /// <summary>
    /// Campo que indica el nombre del sitio
    /// </summary>
    public string NombreSitio { get; set; } = null!;

    /// <summary>
    /// Campo que indica el nombre de la cuenca
    /// </summary>
    public string? Cuenca { get; set; }

    /// <summary>
    /// Campo que indica el organismo de cuenca
    /// </summary>
    public string? OrganismoCuenca { get; set; }

    /// <summary>
    /// Campo que indica el estado de la emergencia
    /// </summary>
    public string? Estado { get; set; }

    /// <summary>
    /// Campo que indica la clave del municipio
    /// </summary>
    public string? ClaveMunicipio { get; set; }

    /// <summary>
    /// Campo que indica el municipio
    /// </summary>
    public string? Municipio { get; set; }

    /// <summary>
    /// Campo que indica el cuerpo de agua 
    /// </summary>
    public string? CuerpoAgua { get; set; }

    /// <summary>
    /// Campo que indica el tipo de cuerpo de agua
    /// </summary>
    public string? TipoCuerpoAgua { get; set; }

    /// <summary>
    /// Campo que indica el subtipo del cuerpo de agua
    /// </summary>
    public string? SubTipoCuerpoAgua { get; set; }

    /// <summary>
    /// Campo que indica la latitud
    /// </summary>
    public string Latitud { get; set; } = null!;

    /// <summary>
    /// Campo que indica la longitud
    /// </summary>
    public string Longitud { get; set; } = null!;

    /// <summary>
    /// Campo que indica la fecha de realización
    /// </summary>
    public DateTime? FechaRealizacion { get; set; }
}
