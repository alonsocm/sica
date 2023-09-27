using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class InformeMensualSupervisionRepository : Repository<InformeMensualSupervision>, IInformeMensualSupervisionRepository
    {
        public InformeMensualSupervisionRepository(SicaContext dbContext) : base(dbContext)
        {
        }
    }
}