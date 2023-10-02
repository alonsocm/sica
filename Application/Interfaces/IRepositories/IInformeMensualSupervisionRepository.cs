using Application.DTOs;
using Application.DTOs.InformeMensualSupervisionCampo;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IInformeMensualSupervisionRepository : IRepository<InformeMensualSupervision>
    {
        Task<InformeMensualSupervisionDto> GetInformeMensualPorAnioMes(string anioReporte, string? anioRegistro, int? mes, long? ocId);
        bool UpdateInformeMensual(InformeMensualDto informe, long informeId);
        public bool UpdateInformeMensualArchivoFirmado(long informeId, string nombreArchivo, byte[] archivo, long usuarioId);
        Task<List<InformeMensualSupervisionBusquedaDto>> GetBusquedaInformeMensual(InformeMensualSupervisionBusquedaDto busqueda);
    }
}
