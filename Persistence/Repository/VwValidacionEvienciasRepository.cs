using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class VwValidacionEvienciasRepository: Repository<VwValidacionEviencias>, IVwValidacionEvienciasRepository
    {
        public VwValidacionEvienciasRepository(SicaContext context) : base(context)
        {

        }

        public List<VwValidacionEviencias> ObtenerDatosGenerales() {

            return _dbContext.VwValidacionEviencias.ToList();
        }

    }
}
