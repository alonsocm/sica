using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class HistorialSustitucionLimitesRepository : Repository<HistorialSustitucionLimites>, IHistorialSusticionLimiteRepository
    {
        public HistorialSustitucionLimitesRepository(SicaContext dbContext) : base(dbContext)
        {
        }
    }
}