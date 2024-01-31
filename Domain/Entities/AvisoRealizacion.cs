using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class AvisoRealizacion
{
    public long Id { get; set; }

    public string ClaveMuestreo { get; set; } = null!;

    public string ClaveSitio { get; set; } = null!;

    public long TipoSitioId { get; set; }

    public long LaboratorioId { get; set; }

    public bool ConEventualidades { get; set; }

    public DateTime FechaProgramada { get; set; }

    public DateTime FechaRealVisita { get; set; }

    public long BrigadaMuestreoId { get; set; }

    public bool ConQcmuestreo { get; set; }

    public string? FolioEventualidad { get; set; }

    public DateTime? FechaAprobacionEventualidad { get; set; }

    public int TipoSupervisionId { get; set; }

    public string? DocumentoEventualidad { get; set; }

    public string? TipoEventualidad { get; set; }

    public DateTime? FechaReprogramacion { get; set; }

    public virtual BrigadaMuestreo BrigadaMuestreo { get; set; } = null!;

    public virtual Laboratorios Laboratorio { get; set; } = null!;

    public virtual TipoSitio TipoSitio { get; set; } = null!;

    public virtual TipoSupervision TipoSupervision { get; set; } = null!;

    public virtual ICollection<ValidacionEvidencia> ValidacionEvidencia { get; set; } = new List<ValidacionEvidencia>();
}
