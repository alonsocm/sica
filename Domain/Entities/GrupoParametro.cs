using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class GrupoParametro
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ParametrosGrupo> ParametrosGrupo { get; set; } = new List<ParametrosGrupo>();
}
