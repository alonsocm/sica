using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class UnidadMedida
    {
        public UnidadMedida()
        {
            ParametrosGrupo = new HashSet<ParametrosGrupo>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<ParametrosGrupo> ParametrosGrupo { get; set; }
    }
}
