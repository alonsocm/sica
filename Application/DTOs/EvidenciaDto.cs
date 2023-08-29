using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EvidenciaDto
    {
        public string NombreArchivo { get; set; }
        public byte[] Archivo { get; set; }
        public long TipoEvidencia { get; set; }
        public string Sufijo { get; set; }        

        public EvidenciaDto()
        {
            this.NombreArchivo = string.Empty;
            this.TipoEvidencia = 0;
            this.Sufijo = string.Empty;
            this.Archivo = new byte[0];
        }
    }
   
}
