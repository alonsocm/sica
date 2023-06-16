using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Observaciones
    {
        public Observaciones()
        {
            ResultadoMuestreoObservacionSrenamecaNavigation = new HashSet<ResultadoMuestreo>();
            ResultadoMuestreoObservacionesOcdlNavigation = new HashSet<ResultadoMuestreo>();
            ResultadoMuestreoObservacionesSecaiaNavigation = new HashSet<ResultadoMuestreo>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual ICollection<ResultadoMuestreo> ResultadoMuestreoObservacionSrenamecaNavigation { get; set; }
        public virtual ICollection<ResultadoMuestreo> ResultadoMuestreoObservacionesOcdlNavigation { get; set; }
        public virtual ICollection<ResultadoMuestreo> ResultadoMuestreoObservacionesSecaiaNavigation { get; set; }
    }
}
