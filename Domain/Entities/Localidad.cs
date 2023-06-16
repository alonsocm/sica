using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Localidad
    {
        public long Id { get; set; }
        public long EstadoId { get; set; }
        public long MunicipioId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual Estado Estado { get; set; } = null!;
        public virtual Municipio Municipio { get; set; } = null!;
    }
}
