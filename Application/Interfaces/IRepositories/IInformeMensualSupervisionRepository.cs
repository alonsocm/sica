using Application.DTOs;
using Application.DTOs.InformeMensualSupervisionCampo;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IInformeMensualSupervisionRepository : IRepository<InformeMensualSupervision>
    {
        InformeMensualSupervisionDto GetInformeMensualPorAnioMes(string anioReporte, string? anioRegistro, int? mes, long? ocId);
        bool UpdateInformeMensual(InformeMensualDto informe, long informeId, byte[] archivo);
        public bool UpdateInformeMensualArchivoFirmado(long informeId, string nombreArchivo, byte[] archivo, long usuarioId);
        List<InformeMensualSupervisionBusquedaDto> GetBusquedaInformeMensual(InformeMensualSupervisionBusquedaDto busqueda);
        Task<List<string>> GetLugaresInformeMensual();
        Task<List<string>> GetMemorandoInformeMensual();
        Task<ArchivoInformeMensualSupervision> GetArchivoInformeMensual(long informeId, int tipo = 1);
        Task<bool> DeleteInformeMensualAsync(long informeId);
    }
}
