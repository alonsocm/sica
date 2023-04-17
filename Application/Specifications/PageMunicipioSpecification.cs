using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class PageMunicipioSpecification: Specification<Municipio>
    {
        public PageMunicipioSpecification()
        {
            Query.Include(x => x.Estado).OrderBy(x => x.Estado.Nombre).ThenBy(x => x.Nombre);
         
        }
    }
}
