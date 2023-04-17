using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using System.Security.Cryptography.X509Certificates;

namespace Application.Specifications
{
    internal class ResumenRevResultadosSpec : Specification<ResultadoMuestreo>
    {
        public ResumenRevResultadosSpec(int idMuestreo)
        {
            Query.Where(x => x.MuestreoId == idMuestreo)
                .Include(z => z.Muestreo).OrderBy(z => z.Id);
        }
    }
}
