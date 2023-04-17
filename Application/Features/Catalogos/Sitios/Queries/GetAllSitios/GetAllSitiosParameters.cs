using Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Sitios.Queries.GetAllSitios
{
    public class GetAllSitiosParameters : RequestParameter
    {
        public string? Nombre { get; set; }
        public string? Clave { get; set; }
    }
}
