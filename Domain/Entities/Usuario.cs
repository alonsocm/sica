using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Usuario
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Activo { get; set; }

    public long? CuencaId { get; set; }

    public long? DireccionLocalId { get; set; }

    public long PerfilId { get; set; }

    public virtual ICollection<AprobacionResultadoMuestreo> AprobacionResultadoMuestreo { get; set; } = new List<AprobacionResultadoMuestreo>();

    public virtual OrganismoCuenca? Cuenca { get; set; }

    public virtual DireccionLocal? DireccionLocal { get; set; }

    public virtual ICollection<HistorialSustitucionEmergencia> HistorialSustitucionEmergencia { get; set; } = new List<HistorialSustitucionEmergencia>();

    public virtual ICollection<HistorialSustitucionLimites> HistorialSustitucionLimites { get; set; } = new List<HistorialSustitucionLimites>();

    public virtual ICollection<Muestreo> MuestreoUsuarioRevisionOcdl { get; set; } = new List<Muestreo>();

    public virtual ICollection<Muestreo> MuestreoUsuarioRevisionSecaia { get; set; } = new List<Muestreo>();

    public virtual Perfil Perfil { get; set; } = null!;
}
