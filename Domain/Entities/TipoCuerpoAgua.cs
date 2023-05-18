using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TipoCuerpoAgua
    {
        public TipoCuerpoAgua()
        {
            CuerpoTipoSubtipoAgua = new HashSet<CuerpoTipoSubtipoAgua>();
            ReglaReporteResultadoTca = new HashSet<ReglaReporteResultadoTca>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public long? TipoHomologadoId { get; set; }
        public bool Activo { get; set; }

        public virtual TipoHomologado? TipoHomologado { get; set; }
        public virtual ICollection<CuerpoTipoSubtipoAgua> CuerpoTipoSubtipoAgua { get; set; }
        public virtual ICollection<ReglaReporteResultadoTca> ReglaReporteResultadoTca { get; set; }
    }
}
