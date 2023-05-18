using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ReglasLaboratorioLdmLpc
    {
        public long Id { get; set; }
        public string ClaveUnicaLabParametro { get; set; } = null!;
        public long LaboratorioId { get; set; }
        public long ParametroId { get; set; }
        public string Ldm { get; set; } = null!;
        public string Lpc { get; set; } = null!;

        public virtual Laboratorios Laboratorio { get; set; } = null!;
        public virtual ParametrosGrupo Parametro { get; set; } = null!;
    }
}
