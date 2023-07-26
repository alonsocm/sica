using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class VwLimiteMaximoComunRepository : Repository<VwLimiteMaximoComun>, IVwLimiteMaximoComunRepository
    {
        public VwLimiteMaximoComunRepository(SicaContext context) : base(context)
        {
        }
    }
}
