using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ParametrosGrupoDto
    {
        public long Id { get; set; }
        public string ClaveParametro { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public long IdSubgrupo { get; set; }
        public long? IdUnidadMedida { get; set; }
        public long? Orden { get; set; }
    }
}
