using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class CuencaDireccionLocalSpecification: Specification<CuencaDireccionesLocales>
    {
        public CuencaDireccionLocalSpecification()
        {
            Query.Include(x => x.Ocuenca).Include(y => y.Dlocal);
        }
    }
}
