using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EventualidadesMuestreoAprobados
    {
        public string Laboratorio { get; set; }
        public string ClaveMuestreo { get; set; }
        public string ClaveSitio { get; set; }
        public string ConEventualidad { get; set; } 
        public string Observaciones { get; set; } 
        public string PorcentajePago { get; set; }

        public EventualidadesMuestreoAprobados()
        {
            this.ConEventualidad = "SI";
            this.Observaciones = "ACEPTADA";
        }
    }
}
