using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    internal class PageLocalidadSpecification : Specification<Localidad>
    {
        public PageLocalidadSpecification()
        {
            Query.Include(x => x.Estado).Include(y => y.Municipio).OrderBy(x => x.Estado.Nombre).ThenBy(y => y.Municipio.Nombre).ThenBy(x => x.Nombre);
        }
    }
}
