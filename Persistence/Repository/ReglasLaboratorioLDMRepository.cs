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
    public class ReglasLaboratorioLDMRepository : Repository<ReglasLaboratorioLdmLpc>, IReglasLaboratorioLDMRepository
    {
        public ReglasLaboratorioLDMRepository(SicaContext dbContext) : base(dbContext)
        {
        }
    }
}
