using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ResultadoCargaMuestreo
    {
        public bool Correcto { get; set; }
        public bool ExisteCarga { get; set; }
        public int NumeroEntrega { get; set; }
        public int Anio { get; set; }
    }
}
