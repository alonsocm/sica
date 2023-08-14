using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class EmergenciaRepository : Repository<Emergencia>, IEmergenciaRepository
    {
        public EmergenciaRepository(SicaContext context) : base(context)
        {
        }
    }
}
