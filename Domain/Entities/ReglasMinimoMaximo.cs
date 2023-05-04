using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ReglasMinimoMaximo
    {
        public long Id { get; set; }
        public string ClaveRegla { get; set; } = null!;
        public long ClasificacionReglaId { get; set; }
        public long TipoReglaId { get; set; }
        public long ParametroId { get; set; }
        public bool Aplica { get; set; }
        public string MinimoMaximo { get; set; } = null!;

        public virtual ClasificacionRegla ClasificacionRegla { get; set; } = null!;
        public virtual ParametrosGrupo Parametro { get; set; } = null!;
        public virtual TipoRegla TipoRegla { get; set; } = null!;
    }
}
