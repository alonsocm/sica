using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MunicipioDto
    {
        public long Id { get; set; }
        public long EstadoId { get; set; }
        public string Nombre { get; set; }

        public virtual EstadoDto Estado { get; set; } 
        public virtual ICollection<SitioDto> Sitio { get; set; }
    }
}
