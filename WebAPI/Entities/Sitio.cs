using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Sitio
{
    /// <summary>
    /// Identificador principal de la tabla Sitio
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la clave sitio
    /// </summary>
    public string ClaveSitio { get; set; } = null!;

    /// <summary>
    /// Campo que describe el nombre del sitio
    /// </summary>
    public string NombreSitio { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de CuencaDireccionLocal
    /// </summary>
    public long CuencaDireccionesLocalesId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de Estado
    /// </summary>
    public long EstadoId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de Municipio
    /// </summary>
    public long MunicipioId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de CuerpoTipoSubtipoAgua
    /// </summary>
    public long CuerpoTipoSubtipoAguaId { get; set; }

    /// <summary>
    /// Campo que describe la latitud del sitio
    /// </summary>
    public double Latitud { get; set; }

    /// <summary>
    /// Campo que describe la longitud del sitio
    /// </summary>
    public double Longitud { get; set; }

    /// <summary>
    /// Campo que describe las observaciones
    /// </summary>
    public string Observaciones { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de OrganismosCuenca
    /// </summary>
    public long? CuencaRevisionId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catalogo de DireccionLocal
    /// </summary>
    public long? DireccionLrevisionId { get; set; }

    /// <summary>
    /// Llave foránea no obligatoria que hace relación al catálogo de Acuifero
    /// </summary>
    public int? AcuiferoId { get; set; }

    public virtual Acuifero? Acuifero { get; set; }

    public virtual CuencaDireccionesLocales CuencaDireccionesLocales { get; set; } = null!;

    public virtual OrganismoCuenca? CuencaRevision { get; set; }

    public virtual CuerpoTipoSubtipoAgua CuerpoTipoSubtipoAgua { get; set; } = null!;

    public virtual DireccionLocal? DireccionLrevision { get; set; }

    public virtual Estado Estado { get; set; } = null!;

    public virtual Municipio Municipio { get; set; } = null!;

    public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; } = new List<ProgramaSitio>();

    public virtual ICollection<SupervisionMuestreo> SupervisionMuestreo { get; set; } = new List<SupervisionMuestreo>();
}
