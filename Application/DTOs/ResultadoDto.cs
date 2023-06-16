using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ResultadoDto
    {
        public List<int> MuestreoId { get; set; } = new List<int>();
        public int EstatusId { get; set; }

        public int? EstatusSECAIAId { get; set; }
        public int? EstatusOCDLId { get; set; }
        public long IdUsuario { get; set; }


    }
}
