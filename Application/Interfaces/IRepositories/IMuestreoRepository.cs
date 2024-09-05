﻿using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.DTOs.EvidenciasMuestreo;
using Application.DTOs.Users;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IMuestreoRepository : IRepository<Muestreo>
    {
        Task<IEnumerable<MuestreoDto>> GetResumenMuestreosAsync(IEnumerable<int> estatus);
        List<Muestreo> ConvertToMuestreosList(List<CargaMuestreoDto> cargaMuestreoDtoList, bool validado, int tipocarga);

        List<ResultadoMuestreo> GenerarResultados(string claveMuestreo, List<CargaMuestreoDto> cargaMuestreoDto);
        List<ResultadoMuestreo> GenerarResultados(List<CargaMuestreoDto> cargaMuestreoDto);

        Task<IEnumerable<ResumenResultadosDTO>> GetResumenResultados(IEnumerable<long> muestreos);
        List<Muestreo> ConvertMuestreosParamsList(List<UpdateMuestreoExcelDto> updateMuestreoExcelDtoList);
        string GetTipoCuerpoAguaHomologado(string claveMuestreo);
        Task<List<int?>> GetListAniosConRegistro();
        Task<List<int?>> GetListNumeroEntrega();
        Task<IEnumerable<AcumuladosResultadoDto>> GetResultadosMuestreoByStatusAsync(Enums.EstatusMuestreo estatusId);
        Task<IEnumerable<AcumuladosResultadoDto>> GetResultadosporMuestreoAsync(int estatusId);
        Task<bool> ExisteSustitucionPrevia(int periodo);
        public Task<IEnumerable<PuntosMuestreoDto>> GetPuntoPR_PMAsync(string claveMuestreo);

        Task<IEnumerable<ReplicasResultadosReglasValidacionDto>> GetReplicasResultadosReglaValidacion(List<int> estatusId);
        Task<bool> CambiarEstatusAsync(Enums.EstatusMuestreo estatus, IEnumerable<long> muestreosIds);
    }
}
