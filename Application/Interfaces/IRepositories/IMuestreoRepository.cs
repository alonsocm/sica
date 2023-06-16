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
    public interface IMuestreoRepository : IRepository<Muestreo>
    {
        Task<IEnumerable<MuestreoDto>> GetResumenMuestreosAsync(List<long> estatus);
        List<Muestreo> ConvertToMuestreosList(List<CargaMuestreoDto> cargaMuestreoDtoList, bool validado);
        Task<IEnumerable<ResumenResultadosDto>> GetResumenResultados(List<int> muestreos);
        List<Muestreo> ConvertMuestreosParamsList(List<UpdateMuestreoExcelDto> updateMuestreoExcelDtoList);
        string GetTipoCuerpoAguaHomologado(string claveMuestreo);
        Task<List<int?>> GetListAniosConRegistro();
    }
}
