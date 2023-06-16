using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class PageEstadoSpecification : Specification<Estado>
    {
        //public PageEstadoSpecification(int pageSize, int pageNumber, string nombre, string abreviatura)
        //{
        //    Query.Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize);

        //    if (!string.IsNullOrEmpty(nombre))
        //        Query.Search(x => x.Nombre, "%" + nombre + "%");

        //    if (!string.IsNullOrEmpty(abreviatura))
        //        Query.Search(x => x.Abreviatura, "%" + abreviatura + "%");
        //}

        public PageEstadoSpecification()
        {
            Query.OrderBy(x => x.Nombre);
        }



    }
}
