using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.DTOs.EvidenciasMuestreo;
using Application.DTOs.RevisionOCDL;
using Application.DTOs.Users;
using Application.Enums;
using Application.Interfaces.IRepositories;
using Application.Models;
using Domain.Entities;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Repository
{
    public class MuestreoRepository : Repository<Muestreo>, IMuestreoRepository
    {
        public MuestreoRepository(SicaContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MuestreoDto>> GetResumenMuestreosAsync(IEnumerable<int> estatus)
        {            
            var muestreos = await (from m in _dbContext.Muestreo
                                   join vpm in _dbContext.VwClaveMuestreo on m.ProgramaMuestreoId equals vpm.ProgramaMuestreoId
                                   where estatus.Contains(m.EstatusId)
                                   select new MuestreoDto
                                   {
                                       MuestreoId = m.Id,
                                       OCDL = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal == null ? m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Clave : m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Clave ?? string.Empty,
                                       ClaveSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                       ClaveMonitoreo = vpm.ClaveMuestreo ?? string.Empty,
                                       Estado = m.ProgramaMuestreo.ProgramaSitio.Sitio.Estado.Nombre ?? string.Empty,
                                       CuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion,
                                       TipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion ?? string.Empty,
                                       TipoCuerpoAguaHomologado = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.TipoHomologado.Descripcion ?? string.Empty,
                                       SubTipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.SubtipoCuerpoAgua.Descripcion ?? string.Empty,
                                       Laboratorio = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Nomenclatura ?? string.Empty,
                                       FechaRealizacion = m.FechaRealVisita.Value.ToString("dd/MM/yyyy") ?? string.Empty,
                                       FechaLimiteRevision = m.FechaLimiteRevision.Value.ToString("dd/MM/yyyy") ?? string.Empty,
                                       NumeroCarga = m.NumeroCarga.ToString() + "-" + m.AnioOperacion ?? string.Empty,
                                       NumeroEntrega = m.NumeroEntrega.ToString(),
                                       Estatus = m.Estatus.Etiqueta,
                                       HoraInicio = $"{m.HoraInicio:hh\\:mm\\:ss}" ?? string.Empty,
                                       HoraFin = $"{m.HoraFin:hh\\:mm\\:ss}" ?? string.Empty,
                                       ProgramaAnual = m.AnioOperacion.ToString() ?? string.Empty,
                                       FechaProgramada = m.ProgramaMuestreo.DiaProgramado.ToString("dd/MM/yyyy"),
                                       TipoSitio = m.ProgramaMuestreo.ProgramaSitio.TipoSitio.Descripcion.ToString() ?? string.Empty,
                                       NombreSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                       FechaCarga = m.FechaCarga.ToString("dd/MM/yyyy") ?? string.Empty,
                                       Observaciones = string.Empty,
                                       ClaveSitioOriginal = string.Empty,
                                       HoraCargaEvidencias = $"{m.FechaCargaEvidencias:yyyy-MM-dd}",
                                       NumeroCargaEvidencias = string.Empty,
                                       TipoCuerpoAguaOriginal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion ?? string.Empty,
                                       DireccionLocal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Descripcion ?? string.Empty,
                                       OrganismoCuenca = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Clave ?? string.Empty,
                                       FechaCargaEvidencias = m.FechaCargaEvidencias == null ? string.Empty : m.FechaCargaEvidencias.ToString() ?? string.Empty,
                                       TipoCargaResultados = m.TipoCarga.Descripcion,
                                       EvidenciasEsperadas = (int)m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.EvidenciasEsperadas,
                                       esReplica = "NO",
                                       EstatusId = m.EstatusId
                                   })
                                   .ToListAsync();


            var evidencias = await (from e in _dbContext.EvidenciaMuestreo
                                    where muestreos.Select(s => s.MuestreoId).Contains(e.MuestreoId)
                                    select new
                                    {
                                        e.MuestreoId,
                                        e.NombreArchivo,
                                        e.TipoEvidenciaMuestreo.Sufijo,
                                        e.TipoEvidenciaMuestreoId
                                    }).ToListAsync();

            var laboratoriosubrogado = await (from e in _dbContext.ResultadoMuestreo
                                              where muestreos.Select(s => s.MuestreoId).Contains(e.MuestreoId) && e.LaboratorioSubrogadoId != null
                                              select new { e.LaboratorioSubrogado.Nomenclatura, e.MuestreoId }).Distinct().ToListAsync();

            //Cambiar para los que ahora no tienen resultados
            var fechasEntrega = await (from resultado in _dbContext.ResultadoMuestreo
                                       where muestreos.Select(s => s.MuestreoId).Contains(resultado.MuestreoId)
                                       select new { resultado.FechaEntrega, resultado.MuestreoId }).Distinct().ToListAsync();

            muestreos.ForEach(async f =>
            {
                var laboratorioSubrogado = laboratoriosubrogado.Where(s => s.MuestreoId == f.MuestreoId).ToList().Select(s => s.Nomenclatura).Distinct().ToList();
                var fechaEntrega = fechasEntrega.Where(s => s.MuestreoId == f.MuestreoId).OrderBy(x => x.FechaEntrega).Select(s => s.FechaEntrega).Distinct();

                for (int i = 0; i < laboratorioSubrogado.ToList().Count; i++)
                {
                    f.LaboratorioSubrogado += laboratorioSubrogado[i] + "/";
                }

                f.LaboratorioSubrogado = f.LaboratorioSubrogado.TrimEnd('/');
                f.FechaEntregaMuestreo = fechaEntrega.Count() != 0 ? fechaEntrega.ToList()[0].ToString("dd/MM/yyyy") : string.Empty;
                f.Evidencias.AddRange(evidencias.Where(s => s.MuestreoId == f.MuestreoId).Select(s => new EvidenciaDto { NombreArchivo = s.NombreArchivo, Sufijo = s.Sufijo, TipoEvidencia = s.TipoEvidenciaMuestreoId }).ToList());
                f.CumpleNumeroEvidencias = f.Evidencias.Count == f.EvidenciasEsperadas ? "SI" : "NO";
            });

            var lstIncidencias = muestreos.Where(x => x.EstatusId == (int)Application.Enums.EstatusMuestreo.ResumenValidaciónReglas).ToList();
            lstIncidencias.ForEach(g => {

                var existeREplica =  (from resultado in _dbContext.ResultadoMuestreo
                                           where resultado.MuestreoId == g.MuestreoId &&
                                           resultado.EstatusResultadoId == (int)Application.Enums.EstatusResultado.CargaRéplicasLaboratorioExterno
                                           select resultado).ToList();

                if (existeREplica.Any()) { g.esReplica = "SI"; } else { muestreos.Remove(g); }
            });
            return muestreos;
        }

        public List<Muestreo> ConvertToMuestreosList(List<CargaMuestreoDto> cargaMuestreoDtoList, bool validado, int tipocarga)
        {
            var cargaMuestreos = cargaMuestreoDtoList.Select(s => new { s.Muestreo, s.Claveconagua, s.TipoCuerpoAgua, s.FechaRealVisita, s.HoraInicioMuestreo, s.HoraFinMuestreo, s.AnioOperacion, s.NoCarga }).Distinct().ToList();
            var muestreos = (from cm in cargaMuestreos
                             join vcm in _dbContext.VwClaveMuestreo on cm.Muestreo equals vcm.ClaveMuestreo
                             select new Muestreo
                             {
                                 ProgramaMuestreoId = vcm.ProgramaMuestreoId,
                                 FechaRealVisita = Convert.ToDateTime(cm.FechaRealVisita),
                                 HoraInicio = TimeSpan.Parse(cm.HoraInicioMuestreo),
                                 HoraFin = TimeSpan.Parse(cm.HoraFinMuestreo),
                                 EstatusId = validado ? (int)Application.Enums.EstatusMuestreo.Liberaciondemonitoreos : (int)Application.Enums.EstatusMuestreo.CargaResultados,
                                 ResultadoMuestreo = GenerarResultados(cm.Muestreo, cargaMuestreoDtoList),
                                 NumeroCarga = cm.NoCarga,
                                 AnioOperacion = Convert.ToInt32(cm.AnioOperacion),
                                 FechaCarga = DateTime.Now,
                                 TipoCargaId = tipocarga
                             }).ToList();

            return muestreos;
        }

        public List<ResultadoMuestreo> GenerarResultados(string claveMuestreo, List<CargaMuestreoDto> cargaMuestreoDto)
        {

            var laboratorios = _dbContext.Laboratorios.ToList();
            var parametros = _dbContext.ParametrosGrupo.ToList();

            var resultados = (from cm in cargaMuestreoDto
                              join p in parametros on cm.ClaveParametro equals p.ClaveParametro
                              join l in laboratorios on cm.LaboratorioRealizoMuestreo equals l.Nomenclatura
                              join ls in laboratorios on cm.LaboratorioSubrogado equals ls.Nomenclatura into lsg
                              from laboratorioSubrogado in lsg.DefaultIfEmpty()
                              where cm.Muestreo == claveMuestreo
                              select new ResultadoMuestreo
                              {
                                  ParametroId = p.Id,
                                  Resultado = cm.Resultado ?? string.Empty,
                                  LaboratorioId = l.Id,
                                  LaboratorioSubrogadoId = laboratorioSubrogado?.Id,
                                  FechaEntrega = DateTime.Parse(cm.FechaEntrega),
                                  IdResultadoLaboratorio = Convert.ToInt64(cm.IdResultado),
                                  ObservacionLaboratorio = cm.ObservacionesLaboratorio
                              }).ToList();

            return resultados;
        }

        public List<ResultadoMuestreo> GenerarResultados(List<CargaMuestreoDto> cargaMuestreoDto)
        {
            var laboratorios = _dbContext.Laboratorios.ToList();
            var parametros = _dbContext.ParametrosGrupo.ToList();

            var resultados = (from cm in cargaMuestreoDto
                              join vcm in _dbContext.VwClaveMuestreo on cm.Muestreo equals vcm.ClaveMuestreo
                              join ms in _dbContext.Muestreo on vcm.ProgramaMuestreoId equals ms.ProgramaMuestreoId
                              join p in parametros on cm.ClaveParametro equals p.ClaveParametro
                              join l in laboratorios on cm.LaboratorioRealizoMuestreo equals l.Nomenclatura
                              join ls in laboratorios on cm.LaboratorioSubrogado equals ls.Nomenclatura into lsg

                              from laboratorioSubrogado in lsg.DefaultIfEmpty()

                              select new ResultadoMuestreo
                              {
                                  MuestreoId = ms.Id,
                                  ParametroId = p.Id,
                                  Resultado = cm.Resultado ?? string.Empty,
                                  LaboratorioId = l.Id,
                                  LaboratorioSubrogadoId = laboratorioSubrogado?.Id,
                                  FechaEntrega = DateTime.Parse(cm.FechaEntrega),
                                  IdResultadoLaboratorio = Convert.ToInt64(cm.IdResultado),
                                  ObservacionLaboratorio = cm.ObservacionesLaboratorio
                              }).ToList();

            return resultados;
        }



        public async Task<IEnumerable<ResumenResultadosDTO>> GetResumenResultados(IEnumerable<long> muestreos)
        {
            var resultados = new List<ResumenResultadosDTO>();

            resultados = await (from m in _dbContext.Muestreo
                                join rm in _dbContext.ResultadoMuestreo on m.Id equals rm.MuestreoId
                                join pg in _dbContext.ParametrosGrupo on rm.ParametroId equals pg.Id
                                join sga in _dbContext.SubgrupoAnalitico on pg.IdSubgrupo equals sga.Id
                                where muestreos.Contains(m.Id)
                                group new { sga } by new { sga.Id, sga.Descripcion } into gpo
                                select new ResumenResultadosDTO
                                {
                                    Cantidad = gpo.Count(),
                                    Grupo = gpo.Key.Descripcion
                                }).ToListAsync();

            return resultados;
        }

        public List<Muestreo> ConvertMuestreosParamsList(List<UpdateMuestreoExcelDto> updateMuestreoExcelDtoList)
        {
            var cargaMuestreos = updateMuestreoExcelDtoList.ToList();
            var muestreos = (from cm in cargaMuestreos
                             join pm in _dbContext.ParametrosGrupo on cm.ClaveParametro equals pm.ClaveParametro

                             select new Muestreo
                             {
                                 //                     ProgramaMuestreoId = vcm.ProgramaMuestreoId,
                                 //                     FechaRealVisita = cm.FechaRealVisita,
                                 //                     HoraInicio = TimeSpan.Parse(cm.HoraInicioMuestreo),
                                 //                     HoraFin = TimeSpan.Parse(cm.HoraFinMuestreo),
                                 //                     EstatusId = (int)Application.Enums.EstatusMuestreo.Validado,
                                 //                     ResultadoMuestreo = GenerarResultados(cm.Muestreo, updateMuestreoExcelDtoList),
                                 //                     NumeroEntrega = 0                          
                             }).ToList();

            return muestreos;    //CAMBIAR POR LA BUENA FALTA ESTA PARTE 
        }

        public string GetTipoCuerpoAguaHomologado(string claveMuestreo)
        {
            var tipoCuerpoAguaHomologado = (from vw in _dbContext.VwClaveMuestreo
                                            join m in _dbContext.Muestreo on vw.ProgramaMuestreoId equals m.ProgramaMuestreoId
                                            where vw.ClaveMuestreo == claveMuestreo
                                            select
                                              m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.TipoHomologado != null
                                              ? m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.TipoHomologado.Descripcion
                                              : m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion).FirstOrDefault();

            return tipoCuerpoAguaHomologado;
        }

        public async Task<List<int?>> GetListAniosConRegistro()
        {
            var lista = await _dbContext.Muestreo.Select(t => t.AnioOperacion).Distinct().ToListAsync();

            return lista;
        }


        public async Task<List<int?>> GetListNumeroEntrega()
        {
            var lista = await _dbContext.Muestreo.Select(t => t.NumeroEntrega).Distinct().ToListAsync();

            return lista;
        }


        public async Task<IEnumerable<AcumuladosResultadoDto>> GetResultadosMuestreoByStatusAsync(Application.Enums.EstatusMuestreo estatus)
        {
            var muestreos = await (from m in _dbContext.Muestreo
                                   join vpm in _dbContext.VwClaveMuestreo on m.ProgramaMuestreoId equals vpm.ProgramaMuestreoId
                                   join resMuestreo in _dbContext.ResultadoMuestreo on m.Id equals resMuestreo.MuestreoId
                                   //join costo in _dbContext.ParametrosCostos on resMuestreo.ParametroId equals costo.ParametroId
                                   where m.EstatusId == (int)estatus
                                   select new AcumuladosResultadoDto
                                   {

                                       MuestreoId = m.Id,
                                       ClaveUnica = $"{vpm.ClaveMuestreo}{resMuestreo.Parametro.ClaveParametro}",
                                       ClaveMonitoreo = vpm.ClaveMuestreo ?? string.Empty,
                                       ClaveSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                       NombreSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                       FechaProgramada = m.ProgramaMuestreo.DiaProgramado.ToString("dd/MM/yyyy"),
                                       FechaRealizacion = m.FechaRealVisita.Value.ToString("dd/MM/yyyy") ?? string.Empty,
                                       HoraInicio = $"{m.HoraInicio:hh\\:mm\\:ss}" ?? string.Empty,
                                       HoraFin = $"{m.HoraFin:hh\\:mm\\:ss}" ?? string.Empty,
                                       TipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion ?? string.Empty,
                                       SubTipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.SubtipoCuerpoAgua.Descripcion,
                                       Laboratorio = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Nomenclatura ?? string.Empty,
                                       LaboratorioRealizoMuestreo = resMuestreo.Laboratorio.Nomenclatura ?? string.Empty,
                                       LaboratorioSubrogado = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Nomenclatura ?? string.Empty,
                                       GrupoParametro = resMuestreo.Parametro.GrupoParametro.Descripcion,
                                       SubGrupo = resMuestreo.Parametro.IdSubgrupoNavigation.Descripcion,
                                       ClaveParametro = resMuestreo.Parametro.ClaveParametro,
                                       Parametro = resMuestreo.Parametro.Descripcion,
                                       UnidadMedida = resMuestreo.Parametro.IdUnidadMedidaNavigation.Descripcion,
                                       Resultado = resMuestreo.Resultado,
                                       ProgramaAnual = m.AnioOperacion.ToString() ?? string.Empty,
                                       Estatus = m.Estatus.Descripcion,
                                       TipoSitio = m.ProgramaMuestreo.ProgramaSitio.TipoSitio.Descripcion.ToString() ?? string.Empty,
                                       DireccionLocal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Descripcion ?? string.Empty,
                                       OrganismoCuenca = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Clave ?? string.Empty,
                                       //costoParametro = costo.Precio,
                                       CostoParametro = _dbContext.ParametrosCostos.Where(x => x.ParametroId.Equals(resMuestreo.ParametroId)).Select(y => y.Precio).FirstOrDefault(),
                                       NumeroCarga = m.NumeroCarga.ToString() + "-" + m.AnioOperacion ?? string.Empty,
                                       FechaEntrega = resMuestreo.FechaEntrega.ToString("dd/MM/yyyy") ?? string.Empty,
                                       IdResultadoLaboratorio = resMuestreo.IdResultadoLaboratorio,
                                       ValidadoReglas = m.EstatusId == (int)Application.Enums.EstatusMuestreo.ResumenValidaciónReglas,
                                       ResultadoReglas = resMuestreo.ResultadoReglas ?? string.Empty,
                                       ResultadoMuestreoId = resMuestreo.Id,
                                       ValidacionFinal = resMuestreo.ValidacionFinal == null ? string.Empty : resMuestreo.ValidacionFinal.Value ? "OK" : "NO",
                                       ObservacionFinal = resMuestreo.ObservacionFinal ?? string.Empty
                                   }).ToListAsync();

            var evidencias = await (from e in _dbContext.EvidenciaMuestreo
                                    where muestreos.Select(s => s.MuestreoId).Contains(e.MuestreoId)
                                    select new
                                    {
                                        e.MuestreoId,
                                        e.NombreArchivo,
                                        e.TipoEvidenciaMuestreo.Sufijo
                                    }).ToListAsync();

            muestreos.ForEach(f =>
            {
                f.Evidencias.AddRange(evidencias.Where(s => s.MuestreoId == f.MuestreoId).Select(s => new EvidenciaDto { NombreArchivo = s.NombreArchivo, Sufijo = s.Sufijo }).ToList());
            });

            return muestreos;
        }

        public async Task<IEnumerable<AcumuladosResultadoDto>> GetResultadosporMuestreoAsync(Application.Enums.EstatusMuestreo estatus)
        {
            var muestreos = await (from resultados in _dbContext.VwResultadosInicialReglas
                                   where resultados.EstatusId == (int)estatus
                                   select new AcumuladosResultadoDto
                                   {
                                       ClaveSitio = resultados.ClaveSitio,
                                       ClaveMonitoreo = resultados.ClaveMuestreo,
                                       NombreSitio = resultados.NombreSitio,
                                       FechaRealizacion = resultados.FechaRealizacion.Value.ToString("dd/MM/yyyy") ?? string.Empty,
                                       FechaProgramada = resultados.FechaProgramada.ToString("dd/MM/yyyy"),
                                       DiferenciaDias = Convert.ToInt32(resultados.DiferenciaEnDias.ToString()),
                                       FechaEntregaTeorica = resultados.FechaEntregaTeorica.Value.ToString("dd/MM/yyyy") ?? string.Empty,
                                       LaboratorioRealizoMuestreo = resultados.Laboratorio ?? string.Empty,
                                       CuerpoAgua = resultados.CuerpoDeAgua,
                                       TipoCuerpoAgua = resultados.TipoCuerpoAgua,
                                       SubTipoCuerpoAgua = resultados.SubtipoCuerpoDeAgua,
                                       NumParametrosEsperados = Convert.ToInt32(resultados.NumDatosEsperados),
                                       NumParametrosCargados = Convert.ToInt32(resultados.NumDatosReportados),
                                       //muestreoCompletoPorResultados = (resultados.MuestreoCompletoPorResultados == null) ? "SI" : resultados.MuestreoCompletoPorResultados.ToString(),
                                       MuestreoCompletoPorResultados = resultados.MuestreoCompletoPorResultados.ToString(),
                                       CumpleReglasCondic = resultados.CumpleConLasReglasCondicionantes ?? "SI",
                                       AnioOperacion = resultados.AnioOperacion ?? 0,
                                       NumeroCarga = resultados.NumeroCarga.ToString() + "-" + resultados.AnioOperacion ?? string.Empty,
                                       MuestreoId = resultados.MuestreoId,
                                       EstatusId = resultados.EstatusId,
                                       TipoCuerpoAguaId = resultados.TipoCuerpoAguaId,
                                       TipoSitioId = resultados.TipoSitioId,
                                       CumpleFechaEntrega = (resultados.NumFechasNoCumplidas > 0) ? "NO" : "SI",
                                       AutorizacionIncompleto = resultados.AutorizacionIncompleto,
                                       AutorizacionFechaEntrega = resultados.AutorizacionFechaEntrega,
                                       UsuarioValido = resultados.UsuarioValido,
                                       PorcentajePago = resultados.PorcentajePago.ToString() ?? string.Empty,
                                   }).ToListAsync();

            foreach (var dato in muestreos)
            {
                string[] valor = { dato.CumpleReglasCondic, dato.MuestreoCompletoPorResultados, dato.CumpleFechaEntrega };
                dato.CumpleTodosCriterios = valor.Contains("NO") ? false : true;

                List<string> datParam = (dato.CumpleReglasCondic == "NO") ? GetParametrosFaltantes(dato.TipoSitioId, dato.TipoCuerpoAguaId, dato.MuestreoId) : new List<string>();
                dato.Observaciones = (datParam.Count > 0) ? datParam[0] : string.Empty;
                dato.ClaveParametro = (datParam.Count > 0) ? datParam[1] : string.Empty;
            }

            return muestreos;
        }

        protected List<string> GetParametrosFaltantes(long tipoSitioId, long tipoCuerpoAguaId, long muestreoId)
        {
            List<string> datosParametros = new();
            string valor = "";
            string clavesParametros = "";

            var resultadosCargados = (from param in _dbContext.ParametrosSitioTipoCuerpoAgua
                                      join reglas in _dbContext.ReglasRelacionParametro on param.ParametroId equals reglas.ParametroId
                                      join resultados in _dbContext.ResultadoMuestreo on reglas.ParametroId equals resultados.ParametroId
                                      select new ResultadoMuestreoDto
                                      {
                                          TipoSitioId = param.TipoSitioId,
                                          TipoCuerpoAguaId = param.TipoCuerpoAguaId,
                                          MuestreoId = resultados.MuestreoId,
                                          ParametroId = param.ParametroId,


                                      }).Where(x => x.TipoSitioId == tipoSitioId && x.TipoCuerpoAguaId == tipoCuerpoAguaId && x.MuestreoId == muestreoId).Select(y => y.ParametroId);

            var muestreos = (from param in _dbContext.ParametrosSitioTipoCuerpoAgua
                             join reglas in _dbContext.ReglasRelacionParametro on param.ParametroId equals reglas.ParametroId
                             join paramgrupo in _dbContext.ParametrosGrupo on param.ParametroId equals paramgrupo.Id
                             select new ResultadoMuestreoDto
                             {
                                 TipoSitioId = param.TipoSitioId,
                                 TipoCuerpoAguaId = param.TipoCuerpoAguaId,
                                 ParametroId = param.ParametroId,
                                 ClaveParametro = paramgrupo.ClaveParametro
                             }).Where(x => x.TipoSitioId == tipoSitioId && x.TipoCuerpoAguaId == tipoCuerpoAguaId && (!resultadosCargados.Contains(x.ParametroId))).Distinct();

            if (muestreos.ToList().Count > 0)
            {
                muestreos.ToList().ForEach(x => valor += (x.ParametroId.ToString() + ','));
                muestreos.ToList().ForEach(x => clavesParametros += (x.ClaveParametro + ','));
                datosParametros.Add(valor);
                datosParametros.Add(clavesParametros);
            }

            return datosParametros;
        }

        public async Task<bool> ExisteSustitucionPrevia(int periodo)
        {
            bool existe = false;

            switch (periodo)
            {
                case 1:
                    existe = await (from m in _dbContext.Muestreo
                                    join hs in _dbContext.HistorialSustitucionLimites on m.Id equals hs.MuestreoId
                                    where m.FechaRealVisita.Value.Year >= DateTime.Now.Year && m.FechaRealVisita.Value <= DateTime.Now
                                    select hs
                              ).AnyAsync();
                    break;
                case 2:
                    existe = await (from m in _dbContext.Muestreo
                                    join hs in _dbContext.HistorialSustitucionLimites on m.Id equals hs.MuestreoId
                                    where m.FechaRealVisita.Value.Year < DateTime.Now.Year
                                    select hs
                             ).AnyAsync();
                    break;
                case 3:
                    existe = await (from m in _dbContext.Muestreo
                                    join hs in _dbContext.HistorialSustitucionLimites on m.Id equals hs.MuestreoId
                                    where m.FechaRealVisita.Value <= DateTime.Now
                                    select hs
                             ).AnyAsync();
                    break;
                default:
                    break;
            }

            return existe;
        }

        public async Task<IEnumerable<PuntosMuestreoDto>> GetPuntoPR_PMAsync(string claveMuestreo)
        {
            List<long> idParametros = new();
            idParametros.Add((long)Application.Enums.IdLatitudLongitudParametro.latitusSitio);
            idParametros.Add((long)Application.Enums.IdLatitudLongitudParametro.LongitudSitio);
            var informacionMuestreo = _dbContext.VwClaveMuestreo.Where(x => x.ClaveMuestreo == claveMuestreo).FirstOrDefault();
            var puntoPR = _dbContext.Sitio.Where(x => x.Id == informacionMuestreo.SitioId).FirstOrDefault();

            List<PuntosMuestreoDto> puntosMuestreoDto = new();
            puntosMuestreoDto.Add(new PuntosMuestreoDto
            {
                ClaveMuestreo = claveMuestreo,
                Longitud = puntoPR.Longitud,
                Latitud = puntoPR.Latitud,
                NombrePunto = Application.Enums.PuntosMuestreo.PuntodeReferencia_PR.ToString(),
                Punto = Application.Enums.PuntosMuestreo.PuntodeReferencia_PR.ToString().Split('_')[1]
            });

            var puntoPM = await (from resultado in _dbContext.ResultadoMuestreo
                                 join muestreo in _dbContext.Muestreo on resultado.MuestreoId equals muestreo.Id
                                 where muestreo.ProgramaMuestreoId == informacionMuestreo.ProgramaMuestreoId && idParametros.Contains(resultado.ParametroId)
                                 select resultado).ToListAsync();

            if (puntoPM.Count > 0)
            {
                puntosMuestreoDto.Add(new PuntosMuestreoDto
                {
                    ClaveMuestreo = claveMuestreo,
                    Longitud = Convert.ToDouble(puntoPM.Where(x => x.ParametroId == (long)Application.Enums.IdLatitudLongitudParametro.LongitudSitio).FirstOrDefault().Resultado),
                    Latitud = Convert.ToDouble(puntoPM.Where(x => x.ParametroId == (long)Application.Enums.IdLatitudLongitudParametro.latitusSitio).FirstOrDefault().Resultado),
                    NombrePunto = Application.Enums.PuntosMuestreo.PuntodeMuestreo_PM.ToString(),
                    Punto = Application.Enums.PuntosMuestreo.PuntodeMuestreo_PM.ToString().Split('_')[1]
                });
            }
            return puntosMuestreoDto;

        }

        public async Task<IEnumerable<ReplicasResultadosReglasValidacionDto>> GetReplicasResultadosReglaValidacion(List<int> EstatusResultadoId)
        {

            var replicasResultados = await (from res in _dbContext.ResultadoMuestreo
                                            join m in _dbContext.Muestreo on res.MuestreoId equals m.Id
                                            join vcm in _dbContext.VwClaveMuestreo on m.ProgramaMuestreoId equals vcm.ProgramaMuestreoId
                                            join estatus in _dbContext.EstatusResultado on res.EstatusResultadoId equals estatus.Id
                                            where EstatusResultadoId.Contains((int)res.EstatusResultadoId)
                                            select new ReplicasResultadosReglasValidacionDto
                                            {
                                                EstatusResultadoId = (int)res.EstatusResultadoId,
                                                NombreEstatus = estatus.Etiqueta,
                                                NumeroCarga = m.NumeroCarga.ToString(),
                                                ResultadoMuestreoId = res.Id,
                                                ClaveUnica = $"{vcm.ClaveMuestreo}{res.Parametro.ClaveParametro}",
                                                ClaveSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                                ClaveMonitoreo = vcm.ClaveMuestreo ?? string.Empty,
                                                Nombre = m.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                                ClaveParametro = res.Parametro.ClaveParametro,
                                                TipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion ?? string.Empty,
                                                TipoHomologado = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.TipoHomologado.Descripcion ?? string.Empty,
                                                Resultado = res.Resultado,
                                                CorrectoResultadoReglaValidacion = res.ResultadoReglas == "OK" ? true : false,
                                                ObservacionReglaValidacion = res.ResultadoReglas,
                                            }).ToListAsync();

            replicasResultados.ForEach(async replica =>
            {
                List<int> lstEstatus = new List<int>();
                lstEstatus.Add((int)Application.Enums.EstatusResultado.IncidenciasResultados);
                lstEstatus.Add((int)Application.Enums.EstatusResultado.EnvíoLaboratorioExterno);


                if (!lstEstatus.Contains(replica.EstatusResultadoId))
                {
                    var dato = _dbContext.ReplicasResultadosReglasValidacion.Where(x => x.ResultadoMuestreoId.Equals(replica.ResultadoMuestreoId)).FirstOrDefault();                    
                   
                        var archivos = (from relReplica in _dbContext.RelacionEvidenciasReplicaResultadosReglas
                                         join replicaResultados in _dbContext.ReplicasResultadosReglasValidacion on relReplica.ReplicasResultadosReglasValidacionId equals replicaResultados.Id
                                         join evid in _dbContext.EvidenciasReplicasResultadoReglasValidacion on relReplica.EvidenciasReplicasResultadoReglasValidacionId equals evid.Id
                                         where replicaResultados.ResultadoMuestreoId == replica.ResultadoMuestreoId
                                         select evid).ToList();

                    replica.Evidencias.AddRange(archivos.Select(s => new EvidenciaDto { NombreArchivo = s.NombreArchivo, Sufijo = s.NombreArchivo.Substring(s.NombreArchivo.LastIndexOf('.'), s.NombreArchivo.Length - s.NombreArchivo.LastIndexOf('.')) }));


                    if (dato != null)
                    {
                        replica.Id = dato.Id;
                        replica.AceptaRechazo = dato.AceptaRechazo;
                        replica.ResultadoReplica = dato.ResultadoReplica;
                        replica.MismoResultado = dato.MismoResultado;
                        replica.ObservacionLaboratorio = dato.ObservacionLaboratorio;
                        replica.FechaReplicaLaboratorio = dato.FechaReplicaLaboratorio;
                        replica.ObservacionSrenameca = dato.ObservacionSrenameca;
                        replica.EsDatoCorrectoSrenameca = dato.EsDatoCorrectoSrenameca;
                        replica.FechaObservacionSrenameca = dato.FechaObservacionSrenameca;
                        replica.ObservacionesReglasReplica = dato.ObservacionesReglasReplica;
                        replica.ApruebaResultadoReplica = dato.ApruebaResultadoReplica;
                        replica.FechaEstatusFinal = dato.FechaEstatusFinal;
                        replica.UsuarioIdReviso = dato.UsuarioIdReviso;

                        if (archivos.Any())
                        {
                            archivos.Select(x => x.NombreArchivo).ToList().ForEach(archivo => replica.NombreEvidencias += archivo + '/');
                            replica.NombreEvidencias = replica.NombreEvidencias.TrimEnd('/');
                        }

                        if (dato.UsuarioIdReviso != 0)
                        {
                            var usuarioValido = _dbContext.Usuario.Where(x => x.Id.Equals(replica.UsuarioIdReviso)).FirstOrDefault();
                            replica.UsuarioReviso = usuarioValido.Nombre + ' ' + usuarioValido.ApellidoPaterno + ' ' + usuarioValido.ApellidoPaterno;
                        }
                    }

                }
            });
            return replicasResultados;
        }

        public async Task<bool> CambiarEstatusAsync(Application.Enums.EstatusMuestreo estatus, IEnumerable<long> muestreosIds)
        {
            var muestreos = await _dbContext.Muestreo.Where(m => muestreosIds.Contains(m.Id)).ToListAsync();

            if (muestreos == null || !muestreos.Any())
            {
                return false;
            }

            muestreos.ForEach(x => x.EstatusId = (int)estatus);

            try
            {
                _dbContext.UpdateRange(muestreos);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ocurrió un error al actualizar el estatus de los muestreos: {ex.Message}");
            }
        }
        public async Task<IQueryable<MonitoreoOCDL>> GetMonitoreosOCDL(int userId)
        {
            var usuario = await _dbContext.Usuario
                .Where(u => u.Id == userId)
                .Include(u => u.DireccionLocal)
                .Include(u => u.Cuenca)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                throw new ArgumentException("No se encontró el usuario en la base de datos");
            }

            IQueryable<Muestreo> muestreos = _dbContext.Muestreo.Where(m => m.EstatusId == (int)Application.Enums.EstatusMuestreo.RevisiónOCDLSECAIA ||
                                                m.EstatusId == (int)Application.Enums.EstatusMuestreo.Liberaciondemonitoreosconextencióndefecha);

            if (usuario.DireccionLocalId != null)
            {
                muestreos = muestreos.Where(m => m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.DlocalId == usuario.DireccionLocalId);
            }
            else if (usuario.CuencaId != null)
            {
                muestreos = muestreos.Where(m => m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.OcuencaId == usuario.CuencaId);
            }

            var registros = from m in muestreos
                            join vpm in _dbContext.VwClaveMuestreo on m.ProgramaMuestreoId equals vpm.ProgramaMuestreoId
                            select new MonitoreoOCDL
                            {
                                MonitoreoId = m.Id,
                                NumeroEntrega = m.NumeroEntrega.ToString() + "-" + m.AnioOperacion.ToString(),
                                OCDL = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal != null ?
                                        m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Descripcion :
                                        m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Descripcion,
                                ClaveSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                ClaveMonitoreo = vpm.ClaveMuestreo,
                                Sitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                FechaRealizacion = m.FechaRealVisita.HasValue ? m.FechaRealVisita.Value.ToString("dd/MM/yyyy") : string.Empty,
                                FechaLimiteRevison = m.FechaLimiteRevision.HasValue ? m.FechaLimiteRevision.Value.ToString("dd/MM/yyyy") : string.Empty,
                                Laboratorio = m.ProgramaMuestreo.ProgramaSitio.Laboratorio == null ? "Sin laboratorio asignado" : m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion,
                                TipoCuerpoAguaOriginal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion,
                                TipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.TipoHomologado == null ? string.Empty :
                                                 m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.TipoHomologado.Descripcion,
                            };

            return registros;
        }
    }
}
