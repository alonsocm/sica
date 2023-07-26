﻿using Application.DTOs;
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
        List<CargaMuestreoDto> ActualizarValorResultado(List<CargaMuestreoDto> muestreosDto);
        Task<IEnumerable<ResultadoParaSustitucionLimitesDto>> ObtenerResultadosParaSustitucionPorPeriodo();
        List<ResultadoParaSustitucionLimitesDto> ActualizarResultadoSustituidoPorLimite(List<ResultadoParaSustitucionLimitesDto> resultadosDto);
    }
}
