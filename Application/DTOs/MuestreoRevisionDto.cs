using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MuestreoRevisionDto
    {
        public string ClaveMonitoreo { get; set; }
        public string? FechaRevision { get; set; }
        public long MuestreoId { get; set; }
    }
}
