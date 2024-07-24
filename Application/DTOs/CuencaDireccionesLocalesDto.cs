using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CuencaDireccionesLocalesDto
    {
        public string OrganismoCuenca { get; set; }
        public string? DieccionLocal { get; set; }
        public long OrganismoCuencaId { get; set; }
        public long? DieccionLocalId { get; set; }
    }
}
