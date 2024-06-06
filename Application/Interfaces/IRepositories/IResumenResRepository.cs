using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IResumenResRepository : IRepository<ResultadoMuestreo>
    {
        Task<IEnumerable<ResultadoMuestreoDto>> GetResumenResultadosMuestreoAsync(int estatusId, int userId, bool isOCDL);
        Task<IEnumerable<ResultadoMuestreoDto>> GetResumenResultados(int userId, bool isOCDL);
        List<ResultadoMuestreo> ConvertMuestreosParamsList(List<UpdateMuestreoExcelDto> updateMuestreoExcelDtoList);
        List<ResultadoMuestreo> ConvertMuestreosParamsListSECAIA(List<UpdateMuestreoSECAIAExcelDto> updateMuestreoExcelDtoList);
        Task<IEnumerable<RegistroOriginalDto>> GetResumenResultadosTemp(int userId, int estatusId, int anio);


        Task<IEnumerable<ResultadoMuestreoDto>> GetResultadosParametrosEstatus(long userId, long estatusId);
    }
}
