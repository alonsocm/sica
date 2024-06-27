using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EstatusMuestreo
{
    /// <summary>
    /// Identificador de Estatus Muestreo
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el estatus del muestreo
    /// </summary>
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Muestreo> MuestreoEstatus { get; set; } = new List<Muestreo>();

    public virtual ICollection<Muestreo> MuestreoEstatusOcdlNavigation { get; set; } = new List<Muestreo>();

    public virtual ICollection<Muestreo> MuestreoEstatusSecaiaNavigation { get; set; } = new List<Muestreo>();

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; } = new List<ResultadoMuestreo>();
}
