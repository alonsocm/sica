using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EstatusMuestreo
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Muestreo> MuestreoEstatus { get; set; } = new List<Muestreo>();

    public virtual ICollection<Muestreo> MuestreoEstatusOcdlNavigation { get; set; } = new List<Muestreo>();

    public virtual ICollection<Muestreo> MuestreoEstatusSecaiaNavigation { get; set; } = new List<Muestreo>();

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; } = new List<ResultadoMuestreo>();
}
