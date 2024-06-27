using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class ResultadoRepository : Repository<ResultadoMuestreo>, IResultado
    {
        public ResultadoRepository(SicaContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ReplicaResultadoDto>> ObtenerReplicasResultados()
        {
            try
            {
                var estatus = new List<int>() { 10, 12, 17, 14, 15 };
                var registros = await (from r in _dbContext.ResultadoMuestreo
                                       where estatus.Contains((int)(r.EstatusResultado != null ? r.EstatusResultado : 0))
                                       select new ReplicaResultadoDto
                                       {
                                           NoEntrega = r.Muestreo.NumeroEntrega.ToString() ?? string.Empty,
                                           ClaveUnica = $"{r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio}-{r.Muestreo.ProgramaMuestreo.DiaProgramado:ddMMyyyy}{r.Parametro.ClaveParametro}",
                                           ClaveSitio = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                           ClaveMonitoreo = $"{r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio}-{r.Muestreo.ProgramaMuestreo.DiaProgramado:ddMMyyyy}",
                                           Nombre = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                           ClaveParametro = r.Parametro.ClaveParametro,
                                           Laboratorio = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Laboratorio != null ? (r.Muestreo.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion ?? string.Empty) : string.Empty,
                                           TipoCuerpoAgua = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion,
                                           TipoCuerpoAguaOriginal = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.SubtipoCuerpoAgua.Descripcion,
                                           Resultado = r.Resultado,
                                           EsCorrectoOCDL = r.EsCorrectoOcdl == null ? string.Empty : (bool)r.EsCorrectoOcdl ? "SI" : "NO",
                                           ObservacionOCDL = r.ObservacionesOcdl ?? string.Empty,
                                           EsCorrectoSECAIA = r.EsCorrectoSecaia == null ? string.Empty : (bool)r.EsCorrectoSecaia ? "SI" : "NO",
                                           ObservacionSECAIA = r.ObservacionesSecaia ?? string.Empty,
                                           ClasificacionObservacion = string.Empty,//TODO:Verificar a qué se refiere este campo
                                           CausaRechazo = string.Empty,//TODO:Verificar a qué se refiere este campo
                                           ResultadoAceptado = string.Empty, //TODO:Verificar si es necesario agregar un campo en ld BD
                                           ResultadoReplica = string.Empty, //TODO:Verificar si es necesario agregar un campo en ld BD
                                           EsMismoResultado = r.EsMismoResultado == null ? string.Empty : (bool)r.EsMismoResultado ? "SI" : "NO",
                                           ObservacionLaboratorio = r.ObservacionLaboratorio ?? string.Empty,//TODO: Se debe agregar nuevo campo en la BD?
                                           FechaReplicaLaboratorio = DateTime.Now.ToString("dd/MM/yy"),//TODO: Agregar nuevo campo en la BD
                                           ObservacionSRNAMECA = string.Empty,//TODO: Agregar campo en la BD
                                           Comentarios = string.Empty,//TODO: Comentarios de quien?
                                           FechaObservacionRENAMECA = DateTime.Now.ToString("dd/MM/yy"),//TODO: Agregar campo en la BD
                                           ResultadoAprobadoDespuesReplica = r.SeApruebaResultadoReplica == null ? string.Empty : (bool)r.SeApruebaResultadoReplica ? "SI" : "NO",//TODO: Se debería agregar nueva tabla?
                                           FechaEstatusFinal = DateTime.Now.ToString("dd/MM/yy"),//TODO: En qué momento se registrará esta fecha?
                                           UsuarioRevisor = string.Empty,//TODO: Se refiere al usuario revisor RENAMECA?
                                           EstatusResultado = r.EstatusResultadoNavigation != null ? r.EstatusResultadoNavigation.Descripcion : string.Empty,
                                           NombreArchivoEvidencias = string.Empty,
                                       }
                                       ).ToListAsync();
                return registros;
            }
            catch (Exception ex)
            {
                var exception = ex;
                throw;
            }
        }

        public async Task<List<ResultadoValidacionReglasDto>> ObtenerResultadosValidacion(List<long> muestreosId)
        {
            var resultados = await (from r in _dbContext.ResultadoMuestreo
                                    where muestreosId.Contains(r.MuestreoId)
                                    select new ResultadoValidacionReglasDto
                                    {
                                        Anio = (int)(r.Muestreo.AnioOperacion != null ? r.Muestreo.AnioOperacion : 0),
                                        NoEntrega = (int)(r.Muestreo.NumeroEntrega != null ? r.Muestreo.NumeroEntrega : 0),
                                        TipoSitio = r.Muestreo.ProgramaMuestreo.ProgramaSitio.TipoSitio.Descripcion ?? string.Empty,
                                        ClaveUnica = $"{r.Muestreo.ProgramaMuestreo.NombreCorrectoArchivo}{r.Parametro.ClaveParametro}",
                                        ClaveSitio = $"{r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio}",
                                        ClaveMonitoreo = r.Muestreo.ProgramaMuestreo.NombreCorrectoArchivo,
                                        FechaRealizacion = r.Muestreo.FechaRealVisita != null ? r.Muestreo.FechaRealVisita.Value.ToString("dd-MM-yyyy") : string.Empty,
                                        Laboratorio = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Laboratorio == null ? string.Empty : r.Muestreo.ProgramaMuestreo.ProgramaSitio.Laboratorio.Nomenclatura ?? string.Empty,
                                        ClaveParametro = r.Parametro.ClaveParametro,
                                        Resultado = r.Resultado,
                                        ValidacionPorReglas = r.ResultadoReglas ?? string.Empty,
                                        FechaAplicacionReglas = DateTime.Now.ToString("dd-MM-yyyy"),
                                    }).ToListAsync();

            return resultados;
        }

        public async Task<IEnumerable<ResultadoParametroReglasDto>> ObtenerResultadosParaReglas(long muestreoId)
        {
            var resultados = await (from r in _dbContext.ResultadoMuestreo
                                    join p in _dbContext.ParametrosGrupo on r.ParametroId equals p.Id
                                    where r.MuestreoId == muestreoId
                                    select new ResultadoParametroReglasDto
                                    {
                                        IdMuestreo = r.MuestreoId,
                                        IdResultado = r.Id,
                                        IdParametro = r.ParametroId,
                                        IdLaboratorio = r.LaboratorioId,
                                        ClaveParametro = p.ClaveParametro,
                                        Valor = r.Resultado,
                                        ResultadoReglas = r.ResultadoReglas ?? string.Empty,
                                        Validado = false
                                    }).ToListAsync();

            return resultados;
        }

        public void ActualizarResultadosValidadosPorReglas(List<ResultadoParametroReglasDto> resultadosDto)
        {
            resultadosDto.ForEach(resultadoDto =>
            {
                var resultadoRegla = (resultadoDto.ResultadoReglas == null || resultadoDto.ResultadoReglas == string.Empty) ? "OK" : resultadoDto.ResultadoReglas;

                var resultado = _dbContext.ResultadoMuestreo.Where(x => x.Id == resultadoDto.IdResultado)
                                                            .ExecuteUpdate(s => s.SetProperty(e => e.ResultadoReglas, resultadoRegla));
            });
        }

        public (List<CargaMuestreoDto>, List<CargaMuestreoDto>) ActualizarValorResultado(List<CargaMuestreoDto> muestreosDto)
        {
            var resultadosNoEncontrados = new List<CargaMuestreoDto>();
            var resultadosNuevos = new List<CargaMuestreoDto>();
            long idMuestreo = 0;
            List<string> muestreosDistinct = muestreosDto.Select(x => x.Muestreo).Distinct().ToList();

            if (muestreosDistinct.Count > 0)
            {
                foreach (var muestreo in muestreosDistinct)
                {
                    long progMuestreoId = _dbContext.VwClaveMuestreo.Where(x => x.ClaveMuestreo == muestreo).Select(x => x.ProgramaMuestreoId).FirstOrDefault();
                    idMuestreo = _dbContext.Muestreo.Where(x => x.ProgramaMuestreoId == progMuestreoId).Select(x => x.Id).FirstOrDefault();

                    if (idMuestreo != 0)
                    {
                        var updateMuestreo = _dbContext.Muestreo.Where(x => x.Id == idMuestreo).ExecuteUpdate(s => s.SetProperty(e => e.HoraInicio, TimeSpan.Parse(muestreosDto[0].HoraInicioMuestreo))
                                            .SetProperty(s => s.HoraFin, TimeSpan.Parse(muestreosDto[0].HoraFinMuestreo))
                                            //.SetProperty(s => s.EstatusId, (int)Application.Enums.EstatusMuestreo.Cargado)
                                            .SetProperty(s => s.FechaRealVisita, Convert.ToDateTime(muestreosDto[0].FechaRealVisita)));
                    }
                    else
                    {
                        List<CargaMuestreoDto> resultadosMuestreo = new List<CargaMuestreoDto>();
                        resultadosMuestreo = muestreosDto.Where(x => x.Muestreo == muestreo).ToList();
                        resultadosNuevos.AddRange(resultadosMuestreo);
                        muestreosDto.RemoveAll(x => x.Muestreo.Equals(muestreo));
                    }
                }
            }

            if (muestreosDto.Count > 0)
            {
                muestreosDto.ForEach(resultadoDto =>
                {
                    var resultadoMuestreo = _dbContext.ResultadoMuestreo.Where(x => x.IdResultadoLaboratorio == Convert.ToInt64(resultadoDto.IdResultado));
                    if (resultadoMuestreo.ToList().Count > 0)
                    {
                        var resultado = resultadoMuestreo.ExecuteUpdate(s => s.SetProperty(e => e.Resultado, resultadoDto.Resultado)
                                                               .SetProperty(x => x.ObservacionLaboratorio, resultadoDto.ObservacionesLaboratorio)
                                                               .SetProperty(s => s.FechaEntrega, DateTime.Parse(resultadoDto.FechaEntrega))
                                                               .SetProperty(s => s.LaboratorioSubrogadoId, _dbContext.Laboratorios.Where(x => x.Nomenclatura == resultadoDto.LaboratorioSubrogado).FirstOrDefault().Id));
                    }
                    else { resultadosNoEncontrados.Add(resultadoDto); }
                });
            }
            return (resultadosNoEncontrados, resultadosNuevos);
        }

        public async Task<IEnumerable<ResultadoParaSustitucionLimitesDto>> ObtenerResultadosParaSustitucionPorPeriodo(int periodo)
        {
            IEnumerable<ResultadoParaSustitucionLimitesDto> resultados = new List<ResultadoParaSustitucionLimitesDto>();

            switch (periodo)
            {
                case 1:
                    resultados = await (from r in _dbContext.ResultadoMuestreo
                                        where r.Muestreo.FechaRealVisita.Value.Year >= DateTime.Now.Year && r.Muestreo.FechaRealVisita.Value <= DateTime.Now
                                        select new ResultadoParaSustitucionLimitesDto
                                        {
                                            IdMuestreo = r.MuestreoId,
                                            IdParametro = r.ParametroId,
                                            IdResultado = r.Id,
                                            ClaveParametro = r.Parametro.ClaveParametro,
                                            ValorOriginal = r.Resultado,
                                            LaboratorioId = r.LaboratorioId
                                        }).ToListAsync();
                    break;
                case 2:
                    resultados = await (from r in _dbContext.ResultadoMuestreo
                                        where r.Muestreo.FechaRealVisita.Value.Year < DateTime.Now.Year
                                        select new ResultadoParaSustitucionLimitesDto
                                        {
                                            IdMuestreo = r.MuestreoId,
                                            IdParametro = r.ParametroId,
                                            IdResultado = r.Id,
                                            ClaveParametro = r.Parametro.ClaveParametro,
                                            ValorOriginal = r.Resultado,
                                            LaboratorioId = r.LaboratorioId
                                        }).ToListAsync();
                    break;
                case 3:
                    resultados = await (from r in _dbContext.ResultadoMuestreo
                                        where r.Muestreo.FechaRealVisita.Value <= DateTime.Now
                                        select new ResultadoParaSustitucionLimitesDto
                                        {
                                            IdMuestreo = r.MuestreoId,
                                            IdParametro = r.ParametroId,
                                            IdResultado = r.Id,
                                            ClaveParametro = r.Parametro.ClaveParametro,
                                            ValorOriginal = r.Resultado,
                                            LaboratorioId = r.LaboratorioId
                                        }).ToListAsync();
                    break;
                default:
                    break;
            }

            return resultados;
        }

        public List<ResultadoParaSustitucionLimitesDto> ActualizarResultadoSustituidoPorLimite(List<ResultadoParaSustitucionLimitesDto> resultadosDto)
        {
            var resultadosNoEncontrados = new List<ResultadoParaSustitucionLimitesDto>();

            resultadosDto.ForEach(resultadoDto =>
            {
                var resultado = _dbContext.ResultadoMuestreo.Where(x => x.Id == resultadoDto.IdResultado)
                                                            .ExecuteUpdate(s => (resultadoDto.esSustitucionLaboratorio) ? s.SetProperty(e => e.ResultadoSustituidoPorLaboratorio, resultadoDto.ValorSustituido) : s.SetProperty(e => e.ResultadoSustituidoPorLimite, resultadoDto.ValorSustituido));
                if (resultado == 0)
                    resultadosNoEncontrados.Add(resultadoDto);
            });

            return resultadosNoEncontrados;
        }

        public async Task<List<MuestreoSustituidoDto>> ObtenerResultadosSustituidos(List<long>? muestreosId)
        {
            var muestreos = await (from hs in _dbContext.HistorialSustitucionLimites
                                   join muestreo in _dbContext.Muestreo on hs.MuestreoId equals muestreo.Id
                                   where muestreosId == null || muestreosId.Contains(muestreo.Id)
                                   select new MuestreoSustituidoDto
                                   {
                                       NoEntrega = muestreo.NumeroEntrega.ToString() ?? string.Empty,
                                       TipoSitio = muestreo.ProgramaMuestreo.ProgramaSitio.TipoSitio.Descripcion,
                                       ClaveSitio = muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                       NombreSitio = muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                       ClaveMonitoreo = muestreo.ProgramaMuestreo.NombreCorrectoArchivo,
                                       FechaRealizacion = muestreo.FechaRealVisita != null ? muestreo.FechaRealVisita.Value.ToString("dd-MM-yyyy") : string.Empty,
                                       Laboratorio = muestreo.ProgramaMuestreo.ProgramaSitio.Laboratorio.Nomenclatura,
                                       CuerpoAgua = muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion,
                                       TipoCuerpoAguaOriginal = muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.SubtipoCuerpoAgua.Descripcion,
                                       TipoCuerpoAgua = muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion,
                                       MuestreoId = muestreo.Id,
                                       Anio = string.IsNullOrEmpty(muestreo.AnioOperacion.ToString()) ? string.Empty : muestreo.AnioOperacion.ToString()
                                   }).ToListAsync();

            muestreos = muestreos.Distinct().ToList();

            muestreos.ForEach(f =>
            {
                var resultados = (from parametro in _dbContext.ParametrosGrupo
                                  join resultado in _dbContext.ResultadoMuestreo on
                                  new { ParametroId = (int)parametro.Id, f.MuestreoId } equals new { ParametroId = (int)resultado.ParametroId, resultado.MuestreoId } into gp
                                  from subresultado in gp.DefaultIfEmpty()
                                  orderby parametro.Orden
                                  select new ResultadoSustituidoDto
                                  {
                                      Id = parametro.Id,
                                      Orden = parametro.Orden ?? 0,
                                      ClaveParametro = parametro.ClaveParametro,
                                      Valor = string.IsNullOrEmpty(subresultado.ResultadoSustituidoPorLimite) ? subresultado.Resultado : subresultado.ResultadoSustituidoPorLimite
                                  }
                                 ).ToList();

                f.Resultados = resultados;
            });

            return muestreos;
        }

        public async Task<IEnumerable<ResultadoParaSustitucionLimitesDto>> ObtenerResultadosParaSustitucionPorAnios(List<int> anios)
        {
            var resultados = await (from r in _dbContext.ResultadoMuestreo
                                    where anios.Contains(r.Muestreo.AnioOperacion ?? 0)
                                    && r.Muestreo.EstatusId == (int)Application.Enums.EstatusMuestreo.AprobacionResultado
                                    select new ResultadoParaSustitucionLimitesDto
                                    {
                                        IdMuestreo = r.MuestreoId,
                                        IdParametro = r.ParametroId,
                                        IdResultado = r.Id,
                                        ClaveParametro = r.Parametro.ClaveParametro,
                                        ValorOriginal = r.Resultado,
                                        LaboratorioId = r.LaboratorioId,
                                        Anio = Convert.ToInt32(r.Muestreo.AnioOperacion),
                                        LaboratorioMuestreo = r.Laboratorio.Nomenclatura

                                    }).ToListAsync();

            return resultados;
        }
    }
}
