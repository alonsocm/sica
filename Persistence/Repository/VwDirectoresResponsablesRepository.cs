using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class VwDirectoresResponsablesRepository : Repository<VwDirectoresResponsables>, IVwDirectoresResponsablesRepository
    {
        public VwDirectoresResponsablesRepository(SicaContext context) : base(context)
        {
        }
    }
}
