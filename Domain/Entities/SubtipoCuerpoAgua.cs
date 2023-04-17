using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class SubtipoCuerpoAgua
    {
        public SubtipoCuerpoAgua()
        {
            CuerpoTipoSubtipoAgua = new HashSet<CuerpoTipoSubtipoAgua>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual ICollection<CuerpoTipoSubtipoAgua> CuerpoTipoSubtipoAgua { get; set; }
    }
}
