using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class SubgrupoAnalitico
    {
        public SubgrupoAnalitico()
        {
            ParametrosGrupo = new HashSet<ParametrosGrupo>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<ParametrosGrupo> ParametrosGrupo { get; set; }
    }
}
