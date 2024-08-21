using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EstatusOcdlSecaia
{
    public int Id { get; set; }

    /// <summary>
    /// Campo que indica en que estatus se encuntra la revision de OC/DL o SECAIA
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campo que india el nombre del estatus como lo solicita el usuario describiendo en que etapa se encuntra la revsión de OC/DL o SECAIA
    /// </summary>
    public string Etiqueta { get; set; } = null!;

    public virtual ICollection<Muestreo> MuestreoEstatusOcdlNavigation { get; set; } = new List<Muestreo>();

    public virtual ICollection<Muestreo> MuestreoEstatusSecaiaNavigation { get; set; } = new List<Muestreo>();
}
