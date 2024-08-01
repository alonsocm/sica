using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Catalogos
{
    public class CuerpoTipoSubtipoAguaDto
    {
        public long Id { get; set; }
        public string CuerpoAgua { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string SubtipoCuerpoAgua { get; set; }
        public string? TipoHomologado { get; set; }
        public long CuerpoAguaId { get; set; }
        public long TipoCuerpoAguaId { get; set; }
        public long SubtipoCuerpoAguaId { get; set; }
        public long? TipoHomologadoId { get; set; }
    }
}
