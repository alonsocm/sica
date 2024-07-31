using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CuencaDireccionesLocalesDto
    {
        public long Id { get; set; }
        public string OrganismoCuenca { get; set; }
        public string? DieccionLocal { get; set; }
        public long OCuencaId { get; set; }
        public long? DLocalId { get; set; }
    }
}
