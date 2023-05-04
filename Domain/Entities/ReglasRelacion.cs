using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ReglasRelacion
    {
        public long Id { get; set; }
        public string ClaveRegla { get; set; } = null!;
        public long ClasificacionReglaId { get; set; }
        public long TipoReglaId { get; set; }
        public long ClaveParametro1Id { get; set; }
        public long ClaveParametro2Id { get; set; }
        public long ClaveParametro3Id { get; set; }
        public long ClaveParametro4Id { get; set; }
        public string RelacionRegla { get; set; } = null!;
    }
}
