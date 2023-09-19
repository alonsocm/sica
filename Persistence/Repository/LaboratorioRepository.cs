using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class LaboratorioRepository : Repository<Laboratorios>, ILaboratorioRepository
    {
        public LaboratorioRepository(SicaContext dbContext) : base(dbContext)
        {
        }

        public List<Laboratorios> ObtenerLaboratoriosMuestradores()
        {
            var resultados = (from cm in _dbContext.Laboratorios
                              join vcm in _dbContext.Muestreadores on cm.Id equals vcm.LaboratorioId

                              select new Laboratorios
                              {
                                  Id = cm.Id,
                                  Descripcion = cm.Descripcion,
                                  Nomenclatura = cm.Nomenclatura

                              }).ToList().DistinctBy(x => x.Id);

            return resultados.ToList();
        }
    }
}
