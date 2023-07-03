using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CargaMuestreos
    {
        public IFormFile Archivo { get; set; }
        public bool Validado { get; set; }
        public bool Reemplazar { get; set; }
    }
}
