using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProgramaAnio
    {
        public ProgramaAnio()
        {
            ProgramaSitio = new HashSet<ProgramaSitio>();
        }

        public long Id { get; set; }
        public string Anio { get; set; } = null!;

        public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; }
    }
}
