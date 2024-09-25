using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EvidenciasReplicasResultado
    {
        public string ClaveUnica { get; set; }
        public List<IFormFile> Archivos { get; set; } = new List<IFormFile>();
    }
}
