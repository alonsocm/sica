using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ObservacionesDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }    
    }
}
