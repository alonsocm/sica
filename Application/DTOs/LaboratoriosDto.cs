using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LaboratoriosDto
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public string Nomenclatura { get; set; }
        public LaboratoriosDto()
        {
            this.Id = 0;
            this.Descripcion = string.Empty;
            this.Nomenclatura = string.Empty;
        }
    }
}
