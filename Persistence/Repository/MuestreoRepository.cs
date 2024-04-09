using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repository
{
    public class MuestreoRepository : Repository<Muestreo>, IMuestreoRepository
    {
        public MuestreoRepository(SicaContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MuestreoDto>> GetResumenMuestreosAsync(List<long> estatus)
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
                                       SubTipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.SubtipoCuerpoAgua.Descripcion ?? string.Empty,
                                       Laboratorio = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Nomenclatura ?? string.Empty,
                                       FechaRealizacion = m.FechaRealVisita.Value.ToString("dd/MM/yyyy") ?? string.Empty,
                                       FechaLimiteRevision = m.FechaLimiteRevision.Value.ToString("dd/MM/yyyy") ?? string.Empty,
                                       NumeroEntrega = m.NumeroEntrega.ToString() ?? string.Empty,
                                       Estatus = m.Estatus.Descripcion,
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
                                       OrganismoCuenca = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Clave ?? string.Empty
                                   })
                                   .ToListAsync();


            var evidencias = await (from e in _dbContext.EvidenciaMuestreo
                                    where muestreos.Select(s => s.MuestreoId).Contains(e.MuestreoId)
                                    select new
                                    {
                                        e.MuestreoId,
                                        e.NombreArchivo,
                                        e.TipoEvidenciaMuestreo.Sufijo
                                    }).ToListAsync();

            var laboratoriosubrogado = await (from e in _dbContext.ResultadoMuestreo
                                              where muestreos.Select(s => s.MuestreoId).Contains(e.MuestreoId) && e.LaboratorioSubrogadoId != null
                                              select new { e.LaboratorioSubrogado.Nomenclatura, e.MuestreoId }).Distinct().ToListAsync();

            var fechentrega = await (from e in _dbContext.ResultadoMuestreo
                                     where muestreos.Select(s => s.MuestreoId).Contains(e.MuestreoId)
                                     select new { e.FechaEntrega, e.MuestreoId }).Distinct().ToListAsync();

            muestreos.ForEach(f =>
            {
                var laboratorioSubrogado = laboratoriosubrogado.Where(s => s.MuestreoId == f.MuestreoId).ToList().Select(s => s.Nomenclatura).Distinct().ToList();
                var Fechaentrega = fechentrega.Where(s => s.MuestreoId == f.MuestreoId).ToList().OrderBy(x => x.FechaEntrega).Select(s => s.FechaEntrega).Distinct();

                for (int i = 0; i < laboratorioSubrogado.ToList().Count; i++)
                {
                    f.LaboratorioSubrogado += laboratorioSubrogado[i] + "/";
                }

                f.LaboratorioSubrogado = f.LaboratorioSubrogado.TrimEnd('/');
                f.FechaEntregaMuestreo = Fechaentrega.ToList()[0].ToString("dd/MM/yyyy");
                f.Evidencias.AddRange(evidencias.Where(s => s.MuestreoId == f.MuestreoId).Select(s => new EvidenciaDto { NombreArchivo = s.NombreArchivo, Sufijo = s.Sufijo }).ToList());
            });

            return muestreos;
        }

        public IEnumerable<object> GetDistinctValuesFromColumn(string column, IEnumerable<MuestreoDto> data)
        {
            var muestreos = data.AsQueryable();
            var valores = muestreos.Select(GetProperty(column)).Distinct();

            return valores;
        }

        public Expression<Func<MuestreoDto, object>> GetProperty(string column)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus,
                "numeroentrega" => muestreo => muestreo.NumeroEntrega,
                "clavesitio" => muestreo => muestreo.ClaveSitio,
                "claveMonitoreo" => muestreo => muestreo.ClaveMonitoreo,
                "tipositio" => muestreo => muestreo.TipoSitio,
                "nombresitio" => muestreo => muestreo.NombreSitio,
                "ocdl" => muestreo => muestreo.OCDL,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua,
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua,
                "programaanual" => muestreo => muestreo.ProgramaAnual,
                "laboratorio" => muestreo => muestreo.Laboratorio,
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado,
                "fechaprogramada" => muestreo => muestreo.FechaProgramada,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion,
                "horainicio" => muestreo => muestreo.HoraInicio,
                "horafin" => muestreo => muestreo.HoraFin,
                "fechacarga" => muestreo => muestreo.FechaCarga,
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo,
                _ => muestreo => muestreo.ClaveMonitoreo
            };
        }

        public List<Muestreo> ConvertToMuestreosList(List<CargaMuestreoDto> cargaMuestreoDtoList, bool validado)
        {
            var laboratorios = _dbContext.Laboratorios.ToList();
            var parametros = _dbContext.ParametrosGrupo.ToList();

            var cargaMuestreos = cargaMuestreoDtoList.Select(s => new { s.Muestreo, s.Claveconagua, s.TipoCuerpoAgua, s.FechaRealVisita, s.HoraInicioMuestreo, s.HoraFinMuestreo, s.AnioOperacion, s.NoEntrega }).Distinct().ToList();
            var muestreos = (from cm in cargaMuestreos
                             join vcm in _dbContext.VwClaveMuestreo on cm.Muestreo equals vcm.ClaveMuestreo
                             select new Muestreo
                             {
                                 ProgramaMuestreoId = vcm.ProgramaMuestreoId,
                                 FechaRealVisita = Convert.ToDateTime(cm.FechaRealVisita),
                                 HoraInicio = TimeSpan.Parse(cm.HoraInicioMuestreo),
                                 HoraFin = TimeSpan.Parse(cm.HoraFinMuestreo),
                                 EstatusId = validado ? (int)Application.Enums.EstatusMuestreo.NoEnviado : (int)Application.Enums.EstatusMuestreo.Cargado,
                                 ResultadoMuestreo = GenerarResultados(cm.Muestreo, cargaMuestreoDtoList, laboratorios, parametros),
                                 NumeroEntrega = Convert.ToInt32(cm.NoEntrega),
                                 AnioOperacion = Convert.ToInt32(cm.AnioOperacion),
                                 FechaCarga = DateTime.Now
                             }).ToList();

            return muestreos;
        }

        public List<ResultadoMuestreo> GenerarResultados(string claveMuestreo, List<CargaMuestreoDto> cargaMuestreoDto, List<Laboratorios> laboratorios, List<ParametrosGrupo> parametros)
        {
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
                                  FechaEntrega = cm.FechaEntrega,
                                  IdResultadoLaboratorio = Convert.ToInt64(cm.IdResultado),
                                  ObservacionLaboratorio = cm.ObservacionesLaboratorio
                              }).ToList();

            return resultados;
        }

        public async Task<IEnumerable<ResumenResultadosDto>> GetResumenResultados(List<int> muestreos)
        {
            var resultados = new List<ResumenResultadosDto>();

            resultados = await (from m in _dbContext.Muestreo
                                join rm in _dbContext.ResultadoMuestreo on m.Id equals rm.MuestreoId
                                join pg in _dbContext.ParametrosGrupo on rm.ParametroId equals pg.Id
                                join sga in _dbContext.SubgrupoAnalitico on pg.IdSubgrupo equals sga.Id
                                where muestreos.Contains(Convert.ToInt32(m.Id))
                                group new { sga } by new { sga.Id, sga.Descripcion } into gpo
                                select new ResumenResultadosDto
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


        public async Task<IEnumerable<AcumuladosResultadoDto>> GetResultadosMuestreoEstatusMuestreoAsync(int estatusId)
        {
            var muestreos = await (from m in _dbContext.Muestreo
                                   join vpm in _dbContext.VwClaveMuestreo on m.ProgramaMuestreoId equals vpm.ProgramaMuestreoId
                                   join resMuestreo in _dbContext.ResultadoMuestreo on m.Id equals resMuestreo.MuestreoId
                                   join costo in _dbContext.ParametrosCostos on resMuestreo.ParametroId equals costo.ParametroId
                                   where m.EstatusId == estatusId
                                   select new AcumuladosResultadoDto
                                   {
                                       MuestreoId = m.Id,
                                       claveUnica = $"{vpm.ClaveMuestreo}{resMuestreo.Parametro.ClaveParametro}",
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
                                       laboratorioRealizoMuestreo = resMuestreo.Laboratorio.Nomenclatura ?? string.Empty,
                                       LaboratorioSubrogado = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Nomenclatura ?? string.Empty,
                                       grupoParametro = resMuestreo.Parametro.GrupoParametro.Descripcion,
                                       subGrupo = resMuestreo.Parametro.IdSubgrupoNavigation.Descripcion,
                                       claveParametro = resMuestreo.Parametro.ClaveParametro,
                                       parametro = resMuestreo.Parametro.Descripcion,
                                       unidadMedida = resMuestreo.Parametro.IdUnidadMedidaNavigation.Descripcion,
                                       resultado = resMuestreo.Resultado,
                                       ProgramaAnual = m.AnioOperacion.ToString() ?? string.Empty,
                                       Estatus = m.Estatus.Descripcion,
                                       TipoSitio = m.ProgramaMuestreo.ProgramaSitio.TipoSitio.Descripcion.ToString() ?? string.Empty,
                                       DireccionLocal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Descripcion ?? string.Empty,
                                       OrganismoCuenca = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Clave ?? string.Empty,
                                       costoParametro = costo.Precio,
                                       NumeroEntrega = m.NumeroEntrega.ToString() ?? string.Empty,
                                       fechaEntrega = resMuestreo.FechaEntrega.ToString("dd/MM/yyyy") ?? string.Empty,
                                       idResultadoLaboratorio = (long)resMuestreo.IdResultadoLaboratorio,
                                       validadoReglas = (m.EstatusId == (int)Application.Enums.EstatusMuestreo.ValidadoPorReglas) ? true : false,
                                       resultadoReglas = resMuestreo.ResultadoReglas ?? string.Empty



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

        public async Task<IEnumerable<AcumuladosResultadoDto>> GetResultadosporMuestreoAsync(List<int> anios, List<int> numeroCarga, int estatusId)
        {
            var muestreos = await (from resultados in _dbContext.VwResultadosInicialReglas

                                   select new AcumuladosResultadoDto
                                   {
                                       ClaveSitio = resultados.ClaveSitio,
                                       ClaveMonitoreo = resultados.ClaveMuestreo,
                                       NombreSitio = resultados.NombreSitio,
                                       FechaRealizacion = resultados.FechaRealizacion.Value.ToString("dd/MM/yyyy") ?? string.Empty,
                                       FechaProgramada = resultados.FechaProgramada.ToString("dd/MM/yyyy"),
                                       diferenciaDias = Convert.ToInt32(resultados.DiferenciaEnDias.ToString()),
                                       fechaEntregaTeorica = resultados.FechaEntregaTeorica.Value.ToString("dd/MM/yyyy") ?? string.Empty,
                                       laboratorioRealizoMuestreo = resultados.Laboratorio ?? string.Empty,
                                       CuerpoAgua = resultados.CuerpoDeAgua,
                                       TipoCuerpoAgua = resultados.TipoCuerpoAgua,
                                       SubTipoCuerpoAgua = resultados.SubtipoCuerpoDeAgua,
                                       numParametrosEsperados = Convert.ToInt32(resultados.NumDatosEsperados),
                                       numParametrosCargados = Convert.ToInt32(resultados.NumDatosReportados),
                                       muestreoCompletoPorResultados = (resultados.MuestreoCompletoPorResultados == null) ? "SI" : resultados.MuestreoCompletoPorResultados.ToString(),
                                       cumpleReglasCondic = (resultados.CumpleConLasReglasCondicionantes == null) ? "SI" : resultados.CumpleConLasReglasCondicionantes,
                                       anioOperacion = resultados.AnioOperacion ?? 0,
                                       NumeroEntrega = resultados.NumeroEntrega.ToString() ?? string.Empty,
                                       MuestreoId = resultados.MuestreoId,
                                       estatusId = resultados.EstatusId,
                                       tipoCuerpoAguaId = resultados.TipoCuerpoAguaId,
                                       tipoSitioId = resultados.TipoSitioId,
                                       cumpleFechaEntrega = (resultados.NumFechasNoCumplidas > 0) ? "NO" : "SI"
                                   }).Where(x => anios.Contains(x.anioOperacion) && numeroCarga.Contains(Convert.ToInt32(x.NumeroEntrega)) && x.estatusId == estatusId).ToListAsync();

            foreach (var dato in muestreos)
            {
                List<string> datParam = (dato.cumpleReglasCondic == "NO") ? GetParametrosFaltantes(dato.tipoSitioId, dato.tipoCuerpoAguaId, dato.MuestreoId) : new List<string>();
                dato.Observaciones = (datParam.Count > 0) ? datParam[0] : string.Empty;
                dato.claveParametro = (datParam.Count > 0) ? datParam[1] : string.Empty;
            }
            return muestreos.ToList();
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

        public Expression<Func<MuestreoDto, bool>> GetExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo == value,
                "estatus" => muestreo => muestreo.Estatus == value,
                "numeroentrega" => muestreo => muestreo.NumeroEntrega == value,
                "clavesitio" => muestreo => muestreo.ClaveSitio == value,
                "claveMonitoreo" => muestreo => muestreo.ClaveMonitoreo == value,
                "tipositio" => muestreo => muestreo.TipoSitio == value,
                "nombresitio" => muestreo => muestreo.NombreSitio == value,
                "ocdl" => muestreo => muestreo.OCDL == value,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua == value,
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua == value,
                "programaanual" => muestreo => muestreo.ProgramaAnual == value,
                "laboratorio" => muestreo => muestreo.Laboratorio == value,
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado == value,
                "fechaprogramada" => muestreo => muestreo.FechaProgramada == value,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion == value,
                "horainicio" => muestreo => muestreo.HoraInicio == value,
                "horafin" => muestreo => muestreo.HoraFin == value,
                "fechacarga" => muestreo => muestreo.FechaCarga == value,
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo == value,
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }

        public Expression<Func<MuestreoDto, bool>> GetContainsExpression(string column, List<string> value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => value.Contains(muestreo.Estatus),
                "numeroentrega" => muestreo => value.Contains(muestreo.NumeroEntrega),
                "clavesitio" => muestreo => value.Contains(muestreo.ClaveSitio),
                "clavemonitoreo" => muestreo => value.Contains(muestreo.ClaveMonitoreo),
                "tipositio" => muestreo => value.Contains(muestreo.TipoSitio),
                "nombresitio" => muestreo => value.Contains(muestreo.NombreSitio),
                "ocdl" => muestreo => value.Contains(muestreo.OCDL),
                "tipocuerpoagua" => muestreo => value.Contains(muestreo.TipoCuerpoAgua),
                "subtipocuerpoagua" => muestreo => value.Contains(muestreo.SubTipoCuerpoAgua),
                "programaanual" => muestreo => value.Contains(muestreo.ProgramaAnual),
                "laboratorio" => muestreo => value.Contains(muestreo.Laboratorio),
                "laboratoriosubrogado" => muestreo => value.Contains(muestreo.LaboratorioSubrogado),
                "fechaprogramada" => muestreo => value.Contains(muestreo.FechaProgramada),
                "fecharealizacion" => muestreo => value.Contains(muestreo.FechaRealizacion),
                "horainicio" => muestreo => value.Contains(muestreo.HoraInicio),
                "horafin" => muestreo => value.Contains(muestreo.HoraFin),
                "fechacarga" => muestreo => value.Contains(muestreo.FechaCarga),
                "fechaentregamuestreo" => muestreo => value.Contains(muestreo.FechaEntregaMuestreo),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public Expression<Func<MuestreoDto, bool>> GetContainsExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus.Contains(value),
                "numeroentrega" => muestreo => muestreo.NumeroEntrega.Contains(value),
                "clavesitio" => muestreo => muestreo.ClaveSitio.Contains(value),
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo.Contains(value),
                "tipositio" => muestreo => muestreo.TipoSitio.Contains(value),
                "nombresitio" => muestreo => muestreo.NombreSitio.Contains(value),
                "ocdl" => muestreo => muestreo.OCDL.Contains(value),
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua.Contains(value),
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua.Contains(value),
                "programaanual" => muestreo => muestreo.ProgramaAnual.Contains(value),
                "laboratorio" => muestreo => muestreo.Laboratorio.Contains(value),
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado.Contains(value),
                "fechaprogramada" => muestreo => muestreo.FechaProgramada.Contains(value),
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion.Contains(value),
                "horainicio" => muestreo => muestreo.HoraInicio.Contains(value),
                "horafin" => muestreo => muestreo.HoraFin.Contains(value),
                "fechacarga" => muestreo => muestreo.FechaCarga.Contains(value),
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo.Contains(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public Expression<Func<MuestreoDto, bool>> GetNotContainsExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => !muestreo.Estatus.Contains(value),
                "numeroentrega" => muestreo => !muestreo.NumeroEntrega.Contains(value),
                "clavesitio" => muestreo => !muestreo.ClaveSitio.Contains(value),
                "clavemonitoreo" => muestreo => !muestreo.ClaveMonitoreo.Contains(value),
                "tipositio" => muestreo => !muestreo.TipoSitio.Contains(value),
                "nombresitio" => muestreo => !muestreo.NombreSitio.Contains(value),
                "ocdl" => muestreo => !muestreo.OCDL.Contains(value),
                "tipocuerpoagua" => muestreo => !muestreo.TipoCuerpoAgua.Contains(value),
                "subtipocuerpoagua" => muestreo => !muestreo.SubTipoCuerpoAgua.Contains(value),
                "programaanual" => muestreo => !muestreo.ProgramaAnual.Contains(value),
                "laboratorio" => muestreo => !muestreo.Laboratorio.Contains(value),
                "laboratoriosubrogado" => muestreo => !muestreo.LaboratorioSubrogado.Contains(value),
                "fechaprogramada" => muestreo => !muestreo.FechaProgramada.Contains(value),
                "fecharealizacion" => muestreo => !muestreo.FechaRealizacion.Contains(value),
                "horainicio" => muestreo => !muestreo.HoraInicio.Contains(value),
                "horafin" => muestreo => !muestreo.HoraFin.Contains(value),
                "fechacarga" => muestreo => !muestreo.FechaCarga.Contains(value),
                "fechaentregamuestreo" => muestreo => !muestreo.FechaEntregaMuestreo.Contains(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public Expression<Func<MuestreoDto, bool>> GetNotEqualExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus != value,
                "numeroentrega" => muestreo => muestreo.NumeroEntrega != value,
                "clavesitio" => muestreo => muestreo.ClaveSitio != value,
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo != value,
                "tipositio" => muestreo => muestreo.TipoSitio != value,
                "nombresitio" => muestreo => muestreo.NombreSitio != value,
                "ocdl" => muestreo => muestreo.OCDL != value,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua != value,
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua != value,
                "programaanual" => muestreo => muestreo.ProgramaAnual != value,
                "laboratorio" => muestreo => muestreo.Laboratorio != value,
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado != value,
                "fechaprogramada" => muestreo => muestreo.FechaProgramada != value,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion != value,
                "horainicio" => muestreo => muestreo.HoraInicio != value,
                "horafin" => muestreo => muestreo.HoraFin != value,
                "fechacarga" => muestreo => muestreo.FechaCarga != value,
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo != value,
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
        public Expression<Func<MuestreoDto, bool>> GetBeginsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus.StartsWith(value),
                "numeroentrega" => muestreo => muestreo.NumeroEntrega.StartsWith(value),
                "clavesitio" => muestreo => muestreo.ClaveSitio.StartsWith(value),
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo.StartsWith(value),
                "tipositio" => muestreo => muestreo.TipoSitio.StartsWith(value),
                "nombresitio" => muestreo => muestreo.NombreSitio.StartsWith(value),
                "ocdl" => muestreo => muestreo.OCDL.StartsWith(value),
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua.StartsWith(value),
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua.StartsWith(value),
                "programaanual" => muestreo => muestreo.ProgramaAnual.StartsWith(value),
                "laboratorio" => muestreo => muestreo.Laboratorio.StartsWith(value),
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado.StartsWith(value),
                "fechaprogramada" => muestreo => muestreo.FechaProgramada.StartsWith(value),
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion.StartsWith(value),
                "horainicio" => muestreo => muestreo.HoraInicio.StartsWith(value),
                "horafin" => muestreo => muestreo.HoraFin.StartsWith(value),
                "fechacarga" => muestreo => muestreo.FechaCarga.StartsWith(value),
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo.StartsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
        public Expression<Func<MuestreoDto, bool>> GetNotBeginsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => !muestreo.Estatus.StartsWith(value),
                "numeroentrega" => muestreo => !muestreo.NumeroEntrega.StartsWith(value),
                "clavesitio" => muestreo => !muestreo.ClaveSitio.StartsWith(value),
                "clavemonitoreo" => muestreo => !muestreo.ClaveMonitoreo.StartsWith(value),
                "tipositio" => muestreo => !muestreo.TipoSitio.StartsWith(value),
                "nombresitio" => muestreo => !muestreo.NombreSitio.StartsWith(value),
                "ocdl" => muestreo => !muestreo.OCDL.StartsWith(value),
                "tipocuerpoagua" => muestreo => !muestreo.TipoCuerpoAgua.StartsWith(value),
                "subtipocuerpoagua" => muestreo => !muestreo.SubTipoCuerpoAgua.StartsWith(value),
                "programaanual" => muestreo => !muestreo.ProgramaAnual.StartsWith(value),
                "laboratorio" => muestreo => !muestreo.Laboratorio.StartsWith(value),
                "laboratoriosubrogado" => muestreo => !muestreo.LaboratorioSubrogado.StartsWith(value),
                "fechaprogramada" => muestreo => !muestreo.FechaProgramada.StartsWith(value),
                "fecharealizacion" => muestreo => !muestreo.FechaRealizacion.StartsWith(value),
                "horainicio" => muestreo => !muestreo.HoraInicio.StartsWith(value),
                "horafin" => muestreo => !muestreo.HoraFin.StartsWith(value),
                "fechacarga" => muestreo => !muestreo.FechaCarga.StartsWith(value),
                "fechaentregamuestreo" => muestreo => !muestreo.FechaEntregaMuestreo.StartsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
        public Expression<Func<MuestreoDto, bool>> GetEndsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus.EndsWith(value),
                "numeroentrega" => muestreo => muestreo.NumeroEntrega.EndsWith(value),
                "clavesitio" => muestreo => muestreo.ClaveSitio.EndsWith(value),
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo.EndsWith(value),
                "tipositio" => muestreo => muestreo.TipoSitio.EndsWith(value),
                "nombresitio" => muestreo => muestreo.NombreSitio.EndsWith(value),
                "ocdl" => muestreo => muestreo.OCDL.EndsWith(value),
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua.EndsWith(value),
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua.EndsWith(value),
                "programaanual" => muestreo => muestreo.ProgramaAnual.EndsWith(value),
                "laboratorio" => muestreo => muestreo.Laboratorio.EndsWith(value),
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado.EndsWith(value),
                "fechaprogramada" => muestreo => muestreo.FechaProgramada.EndsWith(value),
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion.EndsWith(value),
                "horainicio" => muestreo => muestreo.HoraInicio.EndsWith(value),
                "horafin" => muestreo => muestreo.HoraFin.EndsWith(value),
                "fechacarga" => muestreo => muestreo.FechaCarga.EndsWith(value),
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo.EndsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
        public Expression<Func<MuestreoDto, bool>> GetNotEndsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => !muestreo.Estatus.EndsWith(value),
                "numeroentrega" => muestreo => !muestreo.NumeroEntrega.EndsWith(value),
                "clavesitio" => muestreo => !muestreo.ClaveSitio.EndsWith(value),
                "clavemonitoreo" => muestreo => !muestreo.ClaveMonitoreo.EndsWith(value),
                "tipositio" => muestreo => !muestreo.TipoSitio.EndsWith(value),
                "nombresitio" => muestreo => !muestreo.NombreSitio.EndsWith(value),
                "ocdl" => muestreo => !muestreo.OCDL.EndsWith(value),
                "tipocuerpoagua" => muestreo => !muestreo.TipoCuerpoAgua.EndsWith(value),
                "subtipocuerpoagua" => muestreo => !muestreo.SubTipoCuerpoAgua.EndsWith(value),
                "programaanual" => muestreo => !muestreo.ProgramaAnual.EndsWith(value),
                "laboratorio" => muestreo => !muestreo.Laboratorio.EndsWith(value),
                "laboratoriosubrogado" => muestreo => !muestreo.LaboratorioSubrogado.EndsWith(value),
                "fechaprogramada" => muestreo => !muestreo.FechaProgramada.EndsWith(value),
                "fecharealizacion" => muestreo => !muestreo.FechaRealizacion.EndsWith(value),
                "horainicio" => muestreo => !muestreo.HoraInicio.EndsWith(value),
                "horafin" => muestreo => !muestreo.HoraFin.EndsWith(value),
                "fechacarga" => muestreo => !muestreo.FechaCarga.EndsWith(value),
                "fechaentregamuestreo" => muestreo => !muestreo.FechaEntregaMuestreo.EndsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
    }
}
