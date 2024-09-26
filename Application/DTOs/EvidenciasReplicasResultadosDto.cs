using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EvidenciasReplicasResultadosDto: EvidenciaDto
    {
        public long ReplicasResultadoReglasValidacionId { get; set; }

        public EvidenciasReplicasResultadosDto()
        {
            ReplicasResultadoReglasValidacionId = 0;
        }
    }
}
