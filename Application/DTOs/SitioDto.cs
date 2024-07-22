using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SitioDto
    {
        public int Id { get; set; }
        public string NombreSitio { get; set; }
        public string ClaveSitio { get; set; }

        public string Cuenca { get; set; }
        public string? DireccionLocal { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string CuerpoAgua { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string subtipoCuerpoAgua { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Observaciones { get; set; }

    }
}
