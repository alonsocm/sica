using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Laboratorios
    {
        public Laboratorios()
        {
            ProgramaSitio = new HashSet<ProgramaSitio>();
            ReglasLaboratorioLdmLpc = new HashSet<ReglasLaboratorioLdmLpc>();
            ResultadoMuestreo = new HashSet<ResultadoMuestreo>();
        }

        public long Id { get; set; }
        public string? Descripcion { get; set; }
        public string? Nomenclatura { get; set; }

        public virtual ICollection<ProgramaSitio> ProgramaSitio { get; set; }
        public virtual ICollection<ReglasLaboratorioLdmLpc> ReglasLaboratorioLdmLpc { get; set; }
        public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; }
    }
}
