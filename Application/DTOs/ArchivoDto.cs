using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ArchivoDto
    {
        public string NombreArchivo { get; set; }
        public byte[] Archivo { get; set; }
    }
}
