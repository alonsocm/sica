using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IVwDatosGeneralesSupervisionRepository : IRepository<VwDatosGeneralesSupervision>
    {
        List<VwDatosGeneralesSupervision> ObtenerBusqueda(CriteriosBusquedaSupervisionDto busqueda);
    }
}
