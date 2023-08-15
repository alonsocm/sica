using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IMuestreoEmergenciasRepository : IRepository<MuestreoEmergencia>
    {
        List<MuestreoEmergencia> ConvertToMuestreosList(List<CargaMuestreoEmergenciaDto> cargaMuestreoDtoList);
        Task<IEnumerable<ResultadoParaSustitucionLimitesDto>> ObtenerResultadosParaSustitucion();
        List<ResultadoParaSustitucionLimitesDto> ActualizarResultadoSustituidoPorLimite(List<ResultadoParaSustitucionLimitesDto> resultadosDto);
        Task<bool> ExisteCargaPreviaAsync(string nombreEmergencia, string nombreSitio);
    }
}
