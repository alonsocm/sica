using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ISupervisionMuestreoRepository : IRepository<SupervisionMuestreo>
    {
        SupervisionMuestreo ConvertirSupervisionMuestreo(SupervisionMuestreoDto supervisionMuestreo, long usuarioRegistroId);
        Task<IEnumerable<ClasificacionCriterioDto>> ObtenerCriterios();
        Task<bool> EliminarSupervision(long supervisionId);
    }
}
