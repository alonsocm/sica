using Application.DTOs;
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
    public class EvidenciaSupervisionMuestreoRepository : Repository<EvidenciaSupervisionMuestreo>, IEvidenciaSupervisionMuestreoRepository
    {
        public EvidenciaSupervisionMuestreoRepository(SicaContext dbContext) : base(dbContext)
        {        }
     
    }
}
