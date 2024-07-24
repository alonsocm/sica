using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class TipoCuerpoAguaRepository : Repository<TipoCuerpoAgua>, ITipoCuerpoAguaRepository
    {
        public TipoCuerpoAguaRepository(SicaContext context) : base(context)
        {
        }
    }
}
