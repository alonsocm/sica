﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ClasificacionRegla
    {
        public ClasificacionRegla()
        {
            ReglasMinimoMaximo = new HashSet<ReglasMinimoMaximo>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; }
    }
}
