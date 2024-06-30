using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class OrganismoCuenca
{
    /// <summary>
    /// Identifiacador principal del catálogo OrganismoCuenca
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el Organismo de la Cuenca
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que describe la clave del Organismo de Cuenca
    /// </summary>
    public string Clave { get; set; } = null!;

    /// <summary>
    /// Campo que describe la dirección del Organismo de Cuenca
    /// </summary>
    public string Direccion { get; set; } = null!;

    /// <summary>
    /// Campoq ue indica el teléfono del Organismo de Cuenca
    /// </summary>
    public string Telefono { get; set; } = null!;

    public virtual ICollection<CuencaDireccionesLocales> CuencaDireccionesLocales { get; set; } = new List<CuencaDireccionesLocales>();

    public virtual ICollection<Directorio> Directorio { get; set; } = new List<Directorio>();

    public virtual ICollection<Sitio> Sitio { get; set; } = new List<Sitio>();

    public virtual ICollection<SupervisionMuestreo> SupervisionMuestreo { get; set; } = new List<SupervisionMuestreo>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
