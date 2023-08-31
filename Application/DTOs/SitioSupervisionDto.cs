using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SitioSupervisionDto
    {
        public string ClaveMuestreo { get; set; }
        public string ClaveSitio { get; set; }
        public long SitioId { get; set; }
        public string NombreSito { get; set; }
        public long CuencaDireccionLocalId { get; set; }
        public String Latitud { get; set; }
        public string Longitud { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public SitioSupervisionDto()
        {
            this.ClaveMuestreo = string.Empty;
            this.ClaveSitio = string.Empty;
            this.SitioId = 0;
            this.NombreSito = string.Empty;
            this.CuencaDireccionLocalId = 0;
            this.Latitud = string.Empty;
            this.Longitud = string.Empty;
            this.TipoCuerpoAgua = string.Empty;
        }
    }
}
