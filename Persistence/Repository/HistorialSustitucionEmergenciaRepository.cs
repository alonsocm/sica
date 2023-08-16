using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class HistorialSustitucionEmergenciaRepository : Repository<HistorialSustitucionEmergencia>, IHistorialSusticionEmergenciaRepository
    {
        public HistorialSustitucionEmergenciaRepository(SicaContext dbContext) : base(dbContext)
        {
        }
    }
}