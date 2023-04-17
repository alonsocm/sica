using Application.DTOs;
using Application.DTOs.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IResumenResRepository : IRepository<ResultadoMuestreo>
    {       
        Task<IEnumerable<ResultadoMuestreoDto>> GetResumenResultadosMuestreoAsync(int estatusId, int userId, bool isOCDL);       
        Task<IEnumerable<ResultadoMuestreoDto>> GetResumenResultados(int userId, bool isOCDL);
        List<ResultadoMuestreo> ConvertMuestreosParamsList(List<UpdateMuestreoExcelDto> updateMuestreoExcelDtoList);
        List<ResultadoMuestreo> ConvertMuestreosParamsListSECAIA(List<UpdateMuestreoSECAIAExcelDto> updateMuestreoExcelDtoList);
        Task<IEnumerable<ResultadoMuestreoDto>> GetResumenResultadosTemp(int userId, int cuerpAgua, int estatusId,int anio);
    }
}
