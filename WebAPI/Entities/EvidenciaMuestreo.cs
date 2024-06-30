using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EvidenciaMuestreo
{
    /// <summary>
    /// Identificador principal de EvidenciaMuestreo
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la tabla de Muestreo
    /// </summary>
    public long MuestreoId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de TipoEvidenciaMuestreo
    /// </summary>
    public long TipoEvidenciaMuestreoId { get; set; }

    /// <summary>
    /// Campo que describe el nombre del archivo
    /// </summary>
    public string NombreArchivo { get; set; } = null!;

    /// <summary>
    /// Campo que describe la latitud de la evidencia
    /// </summary>
    public decimal? Latitud { get; set; }

    /// <summary>
    /// Campo que describe la longitud de la evidencia
    /// </summary>
    public decimal? Longitud { get; set; }

    /// <summary>
    /// Campo que describe la altitud de la evidencia
    /// </summary>
    public decimal? Altitud { get; set; }

    /// <summary>
    /// Campo que describe la marca de la cámara de la evidencia
    /// </summary>
    public string? MarcaCamara { get; set; }

    /// <summary>
    /// Campo que describe el Modelo de la cámara de la toma de la evidencia
    /// </summary>
    public string? ModeloCamara { get; set; }

    /// <summary>
    /// Campo  que describe el iso
    /// </summary>
    public string? Iso { get; set; }

    /// <summary>
    /// Campo que describe la apertura
    /// </summary>
    public string? Apertura { get; set; }

    /// <summary>
    /// Campo que describe el obturador
    /// </summary>
    public string? Obturador { get; set; }

    /// <summary>
    /// Campo que describe la distancia focal
    /// </summary>
    public string? DistanciaFocal { get; set; }

    /// <summary>
    /// Campo que describe el flash
    /// </summary>
    public string? Flash { get; set; }

    /// <summary>
    /// Campo que describe el tamaño de la evidencia
    /// </summary>
    public string? Tamano { get; set; }

    /// <summary>
    /// Campo que describe la dirección
    /// </summary>
    public string? Direccion { get; set; }

    /// <summary>
    /// Campo que indica las placas
    /// </summary>
    public string? Placas { get; set; }

    /// <summary>
    /// Campo que india el laboratorio
    /// </summary>
    public string? Laboratorio { get; set; }

    /// <summary>
    /// Campo que describe la fecha de inicio
    /// </summary>
    public string? FechaInicio { get; set; }

    /// <summary>
    /// Campo que describe la fecha fin
    /// </summary>
    public string? FechaFin { get; set; }

    /// <summary>
    /// Campo que describe la hora inicio
    /// </summary>
    public string? HoraInicio { get; set; }

    /// <summary>
    /// Campo que describe la hora fin
    /// </summary>
    public string? HoraFin { get; set; }

    /// <summary>
    /// Campo que describe la fecha de creación de la evidencia
    /// </summary>
    public DateTime? FechaCreacion { get; set; }

    public virtual Muestreo Muestreo { get; set; } = null!;

    public virtual TipoEvidenciaMuestreo TipoEvidenciaMuestreo { get; set; } = null!;
}
