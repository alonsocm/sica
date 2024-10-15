using Application.DTOs;
using Application.DTOs.LiberacionResultados;
using Application.DTOs.Users;
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
        Task<IEnumerable<ResultadoLiberacionDTO>> GetResultadosLiberacion();
    }
}
