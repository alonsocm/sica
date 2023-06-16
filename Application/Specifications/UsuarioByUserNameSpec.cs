using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class UsuarioByUserNameSpec : Specification<Usuario>
    {
        public UsuarioByUserNameSpec(string userName)
        {
            Query.Where(c => c.UserName == userName).Include(x => x.Perfil);
        }
    }
    public class UsuariosPerfilesSpec : Specification<Usuario>
    {
        public UsuariosPerfilesSpec()
        {
            Query.Include(x => x.Perfil).IsChainDiscarded = true;
        }
    }
}
