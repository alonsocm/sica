using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ILaboratorioRepository : IRepository<Laboratorios>
    {
        List<Laboratorios> ObtenerLaboratoriosMuestradores();
    }
}
