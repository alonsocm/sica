using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class EstatusMuestreo
    {
        public EstatusMuestreo()
        {
            MuestreoEstatus = new HashSet<Muestreo>();
            MuestreoEstatusOcdlNavigation = new HashSet<Muestreo>();
            MuestreoEstatusSecaiaNavigation = new HashSet<Muestreo>();
            ResultadoMuestreo = new HashSet<ResultadoMuestreo>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Muestreo> MuestreoEstatus { get; set; }
        public virtual ICollection<Muestreo> MuestreoEstatusOcdlNavigation { get; set; }
        public virtual ICollection<Muestreo> MuestreoEstatusSecaiaNavigation { get; set; }
        public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; }
    }
}
