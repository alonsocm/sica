using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EvidenciasMuestreosRechazados
    {
        public string ClaveMuestreo { get; set; }
        public string ClaveSitio { get; set; }
        public string Laboratorio { get; set; }
        public string FechaValidacion { get; set; }
    }
}
