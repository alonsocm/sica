using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Usuario
{
    /// <summary>
    /// Identificador principal de la tabla Usuario
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Camp que describe el username del usuario
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Campo que describe la fecha registro del usuario
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    /// <summary>
    /// Campo que describe el nombre del usuario
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Campo que describe el apellido paterno del usuario
    /// </summary>
    public string ApellidoPaterno { get; set; } = null!;

    /// <summary>
    /// Campo que describe el apellido materno del usuario
    /// </summary>
    public string ApellidoMaterno { get; set; } = null!;

    /// <summary>
    /// Campo que describe el correo electrónico del usuario
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Campoq ue describe si se encuentra activo el usuario
    /// </summary>
    public bool Activo { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de OrganismosCuenca
    /// </summary>
    public long? CuencaId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de DireccionesLocales
    /// </summary>
    public long? DireccionLocalId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Perfil
    /// </summary>
    public long PerfilId { get; set; }

    public virtual ICollection<AprobacionResultadoMuestreo> AprobacionResultadoMuestreo { get; set; } = new List<AprobacionResultadoMuestreo>();

    public virtual ICollection<ArchivoInformeMensualSupervision> ArchivoInformeMensualSupervision { get; set; } = new List<ArchivoInformeMensualSupervision>();

    public virtual OrganismoCuenca? Cuenca { get; set; }

    public virtual DireccionLocal? DireccionLocal { get; set; }

    public virtual ICollection<HistorialSustitucionEmergencia> HistorialSustitucionEmergencia { get; set; } = new List<HistorialSustitucionEmergencia>();

    public virtual ICollection<HistorialSustitucionLimites> HistorialSustitucionLimites { get; set; } = new List<HistorialSustitucionLimites>();

    public virtual ICollection<InformeMensualSupervision> InformeMensualSupervision { get; set; } = new List<InformeMensualSupervision>();

    public virtual ICollection<Muestreo> MuestreoUsuarioRevisionOcdl { get; set; } = new List<Muestreo>();

    public virtual ICollection<Muestreo> MuestreoUsuarioRevisionSecaia { get; set; } = new List<Muestreo>();

    public virtual Perfil Perfil { get; set; } = null!;

    public virtual ICollection<SupervisionMuestreo> SupervisionMuestreo { get; set; } = new List<SupervisionMuestreo>();

    public virtual ICollection<ValidacionEvidencia> ValidacionEvidencia { get; set; } = new List<ValidacionEvidencia>();
}
