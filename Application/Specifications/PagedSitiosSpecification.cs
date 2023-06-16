using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class PagedSitiosSpecification : Specification<Sitio>
    {
        public PagedSitiosSpecification(int pageSize, int pageNumber, string nombre, string clave)
        {
            Query.Skip((pageNumber -1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.NombreSitio, "%" + nombre + "%");

            if (!string.IsNullOrEmpty(clave))
                Query.Search(x => x.ClaveSitio, "%" + clave + "%");
        }
    }
}
