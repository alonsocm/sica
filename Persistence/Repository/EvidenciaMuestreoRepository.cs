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
    public class EvidenciaMuestreoRepository : Repository<EvidenciaMuestreo>, IEvidenciaMuestreoRepository
    {
        public EvidenciaMuestreoRepository(SICAContext dbContext) : base(dbContext)
        {
        }

        public bool EliminarEvidenciasMuestreo(long idMuestreo)
        {
            var evidencias = _dbContext.EvidenciaMuestreo.Where(x => x.MuestreoId == idMuestreo).Select(x => new EvidenciaMuestreo { Id = x.Id }).ToList();

            if (evidencias.Any())
            {
                _dbContext.EvidenciaMuestreo.RemoveRange(evidencias);
            }

            return true;
        }
    }
}
