using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ParametrosGrupo
    {
        public ParametrosGrupo()
        {
            ResultadoMuestreo = new HashSet<ResultadoMuestreo>();
        }

        public long Id { get; set; }
        public string ClaveParametro { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public long IdSubgrupo { get; set; }
        public long? IdUnidadMedida { get; set; }
        public long? Orden { get; set; }

        public virtual SubgrupoAnalitico IdSubgrupoNavigation { get; set; } = null!;
        public virtual UnidadMedida? IdUnidadMedidaNavigation { get; set; }
        public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; }
    }
}
