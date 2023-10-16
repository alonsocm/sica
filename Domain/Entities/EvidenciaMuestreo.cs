using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EvidenciaMuestreo
{
    public long Id { get; set; }

    public long MuestreoId { get; set; }

    public long TipoEvidenciaMuestreoId { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public decimal? Altitud { get; set; }

    public string? MarcaCamara { get; set; }

    public string? ModeloCamara { get; set; }

    public string? Iso { get; set; }

    public string? Apertura { get; set; }

    public string? Obturador { get; set; }

    public string? DistanciaFocal { get; set; }

    public string? Flash { get; set; }

    public string? Tamano { get; set; }

    public string? Direccion { get; set; }

    public virtual Muestreo Muestreo { get; set; } = null!;

    public virtual TipoEvidenciaMuestreo TipoEvidenciaMuestreo { get; set; } = null!;
}
