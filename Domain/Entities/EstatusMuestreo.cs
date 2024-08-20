﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EstatusMuestreo
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Etiqueta { get; set; } = null!;

    public virtual ICollection<Muestreo> Muestreo { get; set; } = new List<Muestreo>();
}
