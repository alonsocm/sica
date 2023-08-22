using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class MuestreoEmergenciasRepository : Repository<MuestreoEmergencia>, IMuestreoEmergenciasRepository
    {
        public MuestreoEmergenciasRepository(SicaContext context) : base(context)
        {
        }
        public List<MuestreoEmergencia> ConvertToMuestreosList(List<CargaMuestreoEmergenciaDto> cargaMuestreoDtoList)
        {
            var muestreos = (from cm in cargaMuestreoDtoList
                             join p in _dbContext.ParametrosGrupo on cm.ClaveParametro equals p.ClaveParametro
                             select new MuestreoEmergencia
                             {
                                 Numero = cm.Numero,
                                 NombreEmergencia = cm.NombreEmergencia,
                                 ClaveUnica = cm.ClaveUnica,
                                 IdLaboratorio = cm.IdLaboratorio,
                                 Sitio = cm.Sitio,
                                 FechaProgramada = Convert.ToDateTime(cm.FechaProgramada),
                                 FechaRealVisita = Convert.ToDateTime(cm.FechaRealVisita),
                                 HoraMuestreo = string.Empty,
                                 TipoCuerpoAgua = cm.TipoCuerpoAgua,
                                 SubtipoCuerpoAgua = cm.SubtipoCuerpoAgua,
                                 LaboratorioRealizoMuestreo = cm.LaboratorioRealizoMuestreo,
                                 LaboratorioSubrogado = cm.LaboratorioSubrogado,
                                 ParametroId = p.Id,
                                 Resultado = cm.Resultado,
                             }).ToList();

            return muestreos;
        }

        public async Task<IEnumerable<ResultadoParaSustitucionLimitesDto>> ObtenerResultadosParaSustitucion()
        {
            IEnumerable<ResultadoParaSustitucionLimitesDto> resultados = new List<ResultadoParaSustitucionLimitesDto>();

            resultados = await (from r in _dbContext.MuestreoEmergencia
                                join l in _dbContext.Laboratorios on r.LaboratorioRealizoMuestreo equals l.Nomenclatura
                                select new ResultadoParaSustitucionLimitesDto
                                {
                                    IdMuestreo = r.Id,
                                    IdParametro = r.ParametroId,
                                    IdResultado = r.Id,
                                    ClaveParametro = r.Parametro.ClaveParametro,
                                    ValorOriginal = r.Resultado,
                                    LaboratorioId = l.Id
                                }).ToListAsync();

            return resultados;
        }
        public async Task<IEnumerable<ResultadoMuestreoEmergenciaDto>> ObtenerResultadosEmergenciasPorAnio()
        {
            IEnumerable<ResultadoMuestreoEmergenciaDto> resultadosEmergencias = new List<ResultadoMuestreoEmergenciaDto>();

            resultadosEmergencias = await (from r in _dbContext.MuestreoEmergencia
                                           join l in _dbContext.Laboratorios on r.LaboratorioRealizoMuestreo equals l.Nomenclatura
                                           select new ResultadoMuestreoEmergenciaDto
                                           {
                                               NombreEmergencia = r.NombreEmergencia,
                                               Sitio = r.Sitio,
                                               //ClaveUnica = r.ClaveUnica,
                                               FechaRealizacion = r.FechaRealVisita.ToString("dd/MM/yyyy"),
                                               //ClaveParametro = r.Parametro.ClaveParametro,
                                               //Resultado = r.Resultado,
                                               Laboratorio = r.LaboratorioRealizoMuestreo,
                                               TipoCuerpoAgua = r.TipoCuerpoAgua,
                                           }).Distinct().ToListAsync();

            resultadosEmergencias.ToList().ForEach(f =>
            {
                var resultados = (from parametro in _dbContext.ParametrosGrupo
                                  join resultado in _dbContext.MuestreoEmergencia on
                                  new { ParametroId = (int)parametro.Id, f.NombreEmergencia } equals new { ParametroId = (int)resultado.ParametroId, resultado.NombreEmergencia } into gp
                                  from subresultado in gp.DefaultIfEmpty()
                                  orderby parametro.Orden
                                  select new ResultadoSustituidoDto
                                  {
                                      Id=parametro.Id,
                                      Orden = parametro.Orden ?? 0,
                                      ClaveParametro = parametro.ClaveParametro,
                                      Valor = string.IsNullOrEmpty(subresultado.ResultadoSustituidoPorLimite) ? subresultado.Resultado : subresultado.ResultadoSustituidoPorLimite
                                  }
                                 ).ToList();

                f.Resultados = resultados;
            });

            return resultadosEmergencias;
        }

        public List<ResultadoParaSustitucionLimitesDto> ActualizarResultadoSustituidoPorLimite(List<ResultadoParaSustitucionLimitesDto> resultadosDto)
        {
            var resultadosNoEncontrados = new List<ResultadoParaSustitucionLimitesDto>();

            resultadosDto.ForEach(resultadoDto =>
            {
                var resultado = _dbContext.MuestreoEmergencia.Where(x => x.Id == resultadoDto.IdResultado)
                                                            .ExecuteUpdate(s => s.SetProperty(e => e.ResultadoSustituidoPorLimite, resultadoDto.ValorSustituido));
                if (resultado == 0)
                    resultadosNoEncontrados.Add(resultadoDto);
            });

            return resultadosNoEncontrados;
        }

        public async Task<bool> ExisteCargaPreviaAsync(string nombreEmergencia, string nombreSitio)
        {
            var existeCargaPrevia = await (from m in _dbContext.MuestreoEmergencia
                                           join e in _dbContext.Emergencia on m.NombreEmergencia equals e.NombreEmergencia
                                           where m.NombreEmergencia == nombreEmergencia && m.Sitio == nombreSitio
                                           select m).AnyAsync();

            return existeCargaPrevia;
        }
    }
}
