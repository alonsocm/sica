using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ResultadoParametroReglasDto
    {
        public long IdMuestreo { get; set; }
        public long IdResultado { get; set; }
        public long IdParametro { get; set; }
        public long IdLaboratorio { get; set; }
        public string ClaveParametro { get; set; }
        public string Valor { get; set; }
        public string? ResultadoReglas { get; set; }
        public bool Validado { get; set; }
    }
}
