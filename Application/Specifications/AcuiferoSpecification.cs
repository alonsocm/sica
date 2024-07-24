using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class AcuiferoSpecification: Specification<Acuifero>
    {
        public AcuiferoSpecification()
        {
            Query.OrderBy(x => x.Descripcion);
        }
    }
}
