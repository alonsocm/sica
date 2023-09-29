using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class VwDirectoresResponsablesRepository : Repository<VwDirectoresResponsablesOc>, IVwDirectoresResponsablesRepository
    {
        public VwDirectoresResponsablesRepository(SicaContext context) : base(context)
        {
        }
    }
}
