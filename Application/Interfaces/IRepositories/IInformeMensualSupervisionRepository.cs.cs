using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IInformeMensualSupervisionRepository : IRepository<InformeMensualSupervision>
    {
        Task<InformeMensualSupervisionDto> GetInformeMensualPorAnioMes(string anioReporte, string? anioRegistro, int? mes);
    }
}
