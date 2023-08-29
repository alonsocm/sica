using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ValoresSupervisionDto
    {
        public long Id { get; set; }
        public int CriterioSupervisionId { get; set; }
        public bool Cumple { get; set; }
        public bool NoAplica { get; set; }
        public string? ObservacionesCriterio { get; set; }
        public long SupervisionMuestreoId { get; set; }
    }
}
