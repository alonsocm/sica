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
    internal class ParametroRepository : Repository<ParametrosGrupo>, IParametroRepository
    {
        public ParametroRepository(SICAContext dbContext) : base(dbContext)
        {
        }
    }
}
