using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class LimiteParametroLaboratorioRepository : Repository<LimiteParametroLaboratorio>, ILimiteParametroLaboratorioRepository
    {
        public LimiteParametroLaboratorioRepository(SicaContext dbContext) : base(dbContext)
        {
        }
    }
}
