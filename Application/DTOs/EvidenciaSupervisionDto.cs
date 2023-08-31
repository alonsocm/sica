using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EvidenciaSupervisionDto:EvidenciaDto
    {
        public long SupervisionMuestreoId { get; set; }
        public EvidenciaSupervisionDto()
        {
            this.SupervisionMuestreoId = 0;
        }
    }
}
