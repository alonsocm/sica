using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProgramaSitio
    {
        public ProgramaSitio()
        {
            ProgramaMuestreo = new HashSet<ProgramaMuestreo>();
        }

        public long Id { get; set; }
        public long TipoSitioId { get; set; }
        public long SitioId { get; set; }
        public long LaboratorioId { get; set; }
        public int NumMuestreosRealizarAnio { get; set; }
        public string? Observaciones { get; set; }

        public virtual Laboratorios Laboratorio { get; set; } = null!;
        public virtual Sitio Sitio { get; set; } = null!;
        public virtual TipoSitio TipoSitio { get; set; } = null!;
        public virtual ICollection<ProgramaMuestreo> ProgramaMuestreo { get; set; }
    }
}
