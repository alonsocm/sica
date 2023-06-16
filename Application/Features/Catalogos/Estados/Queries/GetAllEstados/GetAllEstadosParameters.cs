using Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Estados.Queries.GetAllEstados
{
    public class GetAllEstadosParameters : RequestParameter
    {
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
    }
}
