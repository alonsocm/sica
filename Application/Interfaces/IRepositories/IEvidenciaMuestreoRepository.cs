using Application.DTOs.EvidenciasMuestreo;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IEvidenciaMuestreoRepository : IRepository<EvidenciaMuestreo>
    {
        public Task<IEnumerable<InformacionEvidenciaDto>> GetInformacionEvidenciasAsync();
        public bool EliminarEvidenciasMuestreo(long idMuestreo);

        
    }
}
