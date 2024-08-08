using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class LimiteParametrosLaboratorioExcel
    {
        public string ClaveParametro { get; set; }
        public string NombreParametro { get; set; }
        public string Laboratorio { get; set; }
        public string RealizaLaboratorioMuestreo { get; set; }
        public string? LaboratorioMuestreo { get; set; } = null;
        public string Mes { get; set; }
        public string LDMaCumplir { get; set; }
        public string LPCaCumplir { get; set; }
        public string LoMuestra { get; set; }
        public string? LoSubroga { get; set; }
        public string? LaboratorioSubrogado { get; set; }
        public string MetodoAnalitico { get; set; }
        public string LDM { get; set; }
        public string LPC { get; set; }
        public string Anio { get; set; }



    }
}
