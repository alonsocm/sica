using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProgramaMuestreo
    {
        public ProgramaMuestreo()
        {
            Muestreo = new HashSet<Muestreo>();
        }

        public long Id { get; set; }
        public long ProgramaSitioId { get; set; }
        public DateTime DiaProgramado { get; set; }
        public int SemanaProgramada { get; set; }
        public DateTime DomingoSemanaProgramada { get; set; }
        public string NombreCorrectoArchivo { get; set; } = null!;
        public long BrigadaMuestreoId { get; set; }

        public virtual ProgramaSitio ProgramaSitio { get; set; } = null!;
        public virtual ICollection<Muestreo> Muestreo { get; set; }
    }
}
