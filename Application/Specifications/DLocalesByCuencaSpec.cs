using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    internal class DLocalesByCuencaSpec : Specification<CuencaDireccionesLocales>
    {
        public DLocalesByCuencaSpec(Int64 Id)
        {
            Query.Where(c => c.OcuencaId == Id && c.DlocalId != null).Include(x => x.Dlocal).OrderBy(x => x.Id);
     
        }

    }
}
