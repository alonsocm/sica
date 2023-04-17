using Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RevisionLNRForm
    {
        public IFormFile Archivo { get; set; }
        public string IdUsuario { get; set; }
    }
    public class RevisionLNRDto
    {
        public string NoEntrega { get; set; }
        public string ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string ObservacionSRENAMECA { get; set; }
        public string Comentarios { get; set; }
        public int Linea { get; set; }
    }
}
