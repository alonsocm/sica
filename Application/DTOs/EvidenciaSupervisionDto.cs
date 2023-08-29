using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EvidenciaSupervisionDto:EvidenciaDto
    {
        public long SupervisionEvidenciaId { get; set; }
        public EvidenciaSupervisionDto()
        {
            this.SupervisionEvidenciaId = 0;
        }
    }
}
