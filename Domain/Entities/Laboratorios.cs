using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Laboratorios
    {
        public Laboratorios()
        {
            ProgramaSitio = new HashSet<ProgramaSitio>();
        }

        public long Id { get; set; }
        public string? Descripcion { get; set; }
        public string? Nomenclatura { get; set; }

        public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; }
    }
}
