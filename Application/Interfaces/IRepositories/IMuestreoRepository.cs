﻿using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.DTOs.Users;
using Domain.Entities;
using System.Linq.Expressions;

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
        Task<List<int?>> GetListNumeroEntrega();
        Task<IEnumerable<AcumuladosResultadoDto>> GetResultadosMuestreoEstatusMuestreoAsync(int estatusId);
        Task<IEnumerable<AcumuladosResultadoDto>> GetResultadosporMuestreoAsync(List<int> anios, List<int> numeroCarga, int estatusId);
        Task<bool> ExisteSustitucionPrevia(int periodo);
        public Task<IEnumerable<PuntosMuestreoDto>> GetPuntoPR_PMAsync(string claveMuestreo);
        IEnumerable<object> GetDistinctValuesFromColumn(string column, IEnumerable<MuestreoDto> data);
        Expression<Func<MuestreoDto, object>> GetProperty(string column);
        Expression<Func<MuestreoDto, bool>> GetExpression(string column, string value);
        Expression<Func<MuestreoDto, bool>> GetContainsExpression(string column, List<string> value);
    }
}
