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
    public class VwEstatusMuestreosAdministracionRepository: Repository<VwEstatusMuestreosAdministracionRepository>, IVwEstatusMuestreosAdministracionRepository
    {
        public VwEstatusMuestreosAdministracionRepository(SicaContext context) : base(context)
        {

        }

        public List<VwValidacionEvidenciaTotales> ObtenerResultadosValidacion()
        {

            return _dbContext.VwValidacionEvidenciaTotales.ToList();
        }
    }
}
