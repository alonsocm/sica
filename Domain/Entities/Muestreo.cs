using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Muestreo
{
    public long Id { get; set; }

    public long ProgramaMuestreoId { get; set; }

    public DateTime FechaCarga { get; set; }

    public DateTime? FechaRealVisita { get; set; }

    public TimeSpan? HoraInicio { get; set; }

    public TimeSpan? HoraFin { get; set; }

    public DateTime? FechaLimiteRevision { get; set; }

    public int? AnioOperacion { get; set; }

    public int? NumeroEntrega { get; set; }

    public int EstatusId { get; set; }

    public DateTime? FechaRevisionOcdl { get; set; }

    public long? UsuarioRevisionOcdlid { get; set; }

    public long? TipoAprobacionId { get; set; }

    public int? EstatusOcdl { get; set; }

    public int? EstatusSecaia { get; set; }

    public long? UsuarioRevisionSecaiaid { get; set; }

    public DateTime? FechaRevisionSecaia { get; set; }

    public DateTime? FechaCargaEvidencias { get; set; }

    public virtual EstatusMuestreo Estatus { get; set; } = null!;

    public virtual EstatusMuestreo? EstatusOcdlNavigation { get; set; }

    public virtual EstatusMuestreo? EstatusSecaiaNavigation { get; set; }

    public virtual ICollection<EvidenciaMuestreo> EvidenciaMuestreo { get; set; } = new List<EvidenciaMuestreo>();

    public virtual ProgramaMuestreo ProgramaMuestreo { get; set; } = null!;

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; } = new List<ResultadoMuestreo>();

    public virtual TipoAprobacion? TipoAprobacion { get; set; }

    public virtual Usuario? UsuarioRevisionOcdl { get; set; }

    public virtual Usuario? UsuarioRevisionSecaia { get; set; }
}
