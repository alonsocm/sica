using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class CuerpoTipoSubtipoAguaSpecification: Specification<CuerpoTipoSubtipoAgua>
    {
        public CuerpoTipoSubtipoAguaSpecification()
        {
            Query.Include(x => x.CuerpoAgua).Include(y => y.TipoCuerpoAgua).Include(z=> z.SubtipoCuerpoAgua);
        }
    }
}
