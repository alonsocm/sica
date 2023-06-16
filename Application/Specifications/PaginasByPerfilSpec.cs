using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    internal class PaginasByPerfilSpec : Specification<PerfilPagina>
    {
        public PaginasByPerfilSpec(string idPerfil)
        {
            Query.Where(c => c.IdPerfilNavigation.Nombre == idPerfil && c.Estatus == true)
                .Include(x => x.IdPaginaNavigation).OrderBy(x=> x.IdPaginaNavigation.Orden).ThenBy(x => x.IdPaginaNavigation.IdPaginaPadre).ThenBy(x => x.IdPaginaNavigation.Id);
        }
    }
}
