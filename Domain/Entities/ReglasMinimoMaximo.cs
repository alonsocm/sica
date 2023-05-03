using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReglasMinimoMaximo
    {
        public string ClaveRegla { get; set; } = string.Empty;
        public string Clasificacion { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string ClaveParametro { get; set; } = string.Empty;
        public string MinimoMaximoIncumple { get; set; } = string.Empty;
        public bool Aplica { get; set; }
    }
}
