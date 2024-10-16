using Application.DTOs;
using Application.DTOs.RevisionOCDL;
using Application.DTOs.LiberacionResultados;
using Application.DTOs.Users;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IResultado : IRepository<ResultadoMuestreo>
    {
        Task<IEnumerable<ReplicaResultadoDto>> ObtenerReplicasResultados();
        Task<List<ResultadoValidacionReglasDto>> ObtenerResultadosValidacion(List<long> muestreosId);
        Task<IEnumerable<ResultadoParametroReglasDto>> ObtenerResultadosParaReglas(long muestreoId);
        void ActualizarResultadosValidadosPorReglas(List<ResultadoParametroReglasDto> resultadosDto);
        (List<CargaMuestreoDto>, List<CargaMuestreoDto>) ActualizarValorResultado(List<CargaMuestreoDto> muestreosDto);
        Task<IEnumerable<ResultadoParaSustitucionLimitesDto>> ObtenerResultadosParaSustitucionPorPeriodo(int periodo);
        List<ResultadoParaSustitucionLimitesDto> ActualizarResultadoSustituidoPorLimite(List<ResultadoParaSustitucionLimitesDto> resultadosDto);
        Task<List<MuestreoSustituidoDto>> ObtenerResultadosSustituidos(List<long>? muestreosId);
        Task<IEnumerable<ResultadoParaSustitucionLimitesDto>> ObtenerResultadosParaSustitucionPorAnios(List<int> anios);
        Task<int> EnviarResultadoAIncidencias(IEnumerable<long> resultados);
        Task<int> LiberarResultados(IEnumerable<long> resultados);
        Task<PagedResponse<List<ResultadoLiberacionDTO>>> GetResultadosLiberacion(List<Filter> filters, int pageNumber, int pageSize);
        Task<IEnumerable<ResultadosValidadosPorOCDLDTO>> GetResultadosValidadosPorOCDLAsync();

        Task<IEnumerable<object>> GetDistinctResultadosLiberacionPropertyAsync(List<Filter> filters, string selector);
    }
}
