using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Sitio
{
    public long Id { get; set; }

    public string ClaveSitio { get; set; } = null!;

    public string NombreSitio { get; set; } = null!;

    public long CuencaDireccionesLocalesId { get; set; }

    public long EstadoId { get; set; }

    public long MunicipioId { get; set; }

    public long CuerpoTipoSubtipoAguaId { get; set; }

    public double Latitud { get; set; }

    public double Longitud { get; set; }

    public string Observaciones { get; set; } = null!;

    public long? CuencaRevisionId { get; set; }

    public long? DireccionLrevisionId { get; set; }

    public virtual CuencaDireccionesLocales CuencaDireccionesLocales { get; set; } = null!;

    public virtual OrganismoCuenca? CuencaRevision { get; set; }

    public virtual CuerpoTipoSubtipoAgua CuerpoTipoSubtipoAgua { get; set; } = null!;

    public virtual DireccionLocal? DireccionLrevision { get; set; }

    public virtual Estado Estado { get; set; } = null!;

    public virtual Municipio Municipio { get; set; } = null!;

    public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; } = new List<ProgramaSitio>();
}
