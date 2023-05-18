using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ReglasRelacionParametro
    {
        public long Id { get; set; }
        public long ReglasRelacionId { get; set; }
        public long ParametroId { get; set; }

        public virtual ParametrosGrupo Parametro { get; set; } = null!;
        public virtual ReglasRelacion ReglasRelacion { get; set; } = null!;
    }
}
