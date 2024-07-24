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


        public PagedSitiosSpecification()
        {
            Query.Include(x => x.CuencaDireccionesLocales).Include(a => a.CuencaDireccionesLocales.Ocuenca).Include(a => a.CuencaDireccionesLocales.Dlocal)
                .Include(y => y.Estado).Include(z => z.Municipio).Include(c => c.CuerpoTipoSubtipoAgua).Include(d => d.CuerpoTipoSubtipoAgua.CuerpoAgua)
                .Include(e => e.CuerpoTipoSubtipoAgua.TipoCuerpoAgua).Include(f => f.CuerpoTipoSubtipoAgua.SubtipoCuerpoAgua).Include(g => g.Acuifero);
        }

    }
}
