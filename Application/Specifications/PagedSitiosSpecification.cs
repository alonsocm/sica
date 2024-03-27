using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class PagedSitiosSpecification : Specification<Sitio>
    {
        public PagedSitiosSpecification(string nombre, string clave)
        {
            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.NombreSitio, "%" + nombre + "%");

            if (!string.IsNullOrEmpty(clave))
                Query.Search(x => x.ClaveSitio, "%" + clave + "%");
        }
    }
}
