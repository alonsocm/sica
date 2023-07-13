using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

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
                                       OCDL = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal == null ? m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Clave : m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Clave??string.Empty,
                                       ClaveSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                       ClaveMonitoreo = vpm.ClaveMuestreo??string.Empty,
                                       Estado = m.ProgramaMuestreo.ProgramaSitio.Sitio.Estado.Nombre??string.Empty,
                                       CuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion,
                                       TipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion??string.Empty,
                                       Laboratorio = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion??string.Empty,
                                       FechaRealizacion = m.FechaRealVisita.ToString()??string.Empty,
                                       FechaLimiteRevision = m.FechaLimiteRevision.ToString()??string.Empty,
                                       NumeroEntrega = m.NumeroEntrega.ToString()??string.Empty,
                                       Estatus = m.Estatus.Descripcion,
                                       HoraInicio = $"{m.HoraInicio:hh\\:mm\\:ss}"??string.Empty,
                                       HoraFin =$"{m.HoraFin:hh\\:mm\\:ss}"??string.Empty,
                                       ProgramaAnual = m.AnioOperacion.ToString()??string.Empty,
                                       FechaProgramada = m.ProgramaMuestreo.DiaProgramado.ToString(),
                                       TipoSitio = m.ProgramaMuestreo.ProgramaSitio.TipoSitio.TipoSitio1.ToString()??string.Empty,
                                       NombreSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                       FechaCarga = m.FechaCarga.ToString("yyyy-MM-dd")??string.Empty,
                                       LaboratorioSubrogado = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion ?? string.Empty,
                                       Observaciones = string.Empty,
                                       ClaveSitioOriginal = string.Empty,
                                       HoraCargaEvidencias = $"{m.FechaCargaEvidencias:yyyy-MM-dd}",
                                       NumeroCargaEvidencias = string.Empty,
                                       TipoCuerpoAguaOriginal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion ?? string.Empty,
                                       DireccionLocal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Descripcion ?? string.Empty,
                                       OrganismoCuenca = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Clave ?? string.Empty
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

        public List<Muestreo> ConvertToMuestreosList(List<CargaMuestreoDto> cargaMuestreoDtoList, bool validado)
        {
            var cargaMuestreos = cargaMuestreoDtoList.Select(s => new { s.Muestreo, s.Claveconagua, s.TipoCuerpoAgua, s.FechaRealVisita, s.HoraInicioMuestreo, s.HoraFinMuestreo, s.AnioOperacion }).Distinct().ToList();
            var muestreos = (from cm in cargaMuestreos
                             join vcm in _dbContext.VwClaveMuestreo on cm.Muestreo equals vcm.ClaveMuestreo
                             select new Muestreo
                             {
                                 ProgramaMuestreoId = vcm.ProgramaMuestreoId,
                                 FechaRealVisita = Convert.ToDateTime(cm.FechaRealVisita),
                                 HoraInicio = TimeSpan.Parse(cm.HoraInicioMuestreo),
                                 HoraFin = TimeSpan.Parse(cm.HoraFinMuestreo),
                                 EstatusId = validado ? (int)Application.Enums.EstatusMuestreo.NoEnviado : (int)Application.Enums.EstatusMuestreo.Cargado,
                                 ResultadoMuestreo = GenerarResultados(cm.Muestreo, cargaMuestreoDtoList),
                                 NumeroEntrega = 0,
                                 AnioOperacion = Convert.ToInt32(cm.AnioOperacion),
                                 FechaCarga = DateTime.Now
                             }).ToList();

            return muestreos;
        }

        public List<ResultadoMuestreo> GenerarResultados(string claveMuestreo, List<CargaMuestreoDto> cargaMuestreoDto)
        {

            var resultados = (from cm in cargaMuestreoDto
                              join p in _dbContext.ParametrosGrupo on cm.ClaveParametro equals p.ClaveParametro
                              join l in _dbContext.Laboratorios on cm.LaboratorioRealizoMuestreo equals l.Nomenclatura
                              where cm.Muestreo == claveMuestreo
                              select new ResultadoMuestreo
                              {
                                  ParametroId = p.Id,
                                  Resultado = cm.Resultado??string.Empty,
                                  LaboratorioId = l.Id,
                                  FechaEntrega = cm.FechaEntrega,
                                  IdResultadoLaboratorio = Convert.ToInt64(cm.IdResultado)
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

        public async Task<IEnumerable<AcumuladosResultadoDto>> GetResultadosMuestreoEstatusMuestreoAsync(int estatusId)
        {
            var muestreos = await (from m in _dbContext.Muestreo
                                   join vpm in _dbContext.VwClaveMuestreo on m.ProgramaMuestreoId equals vpm.ProgramaMuestreoId
                                   join resMuestreo in _dbContext.ResultadoMuestreo on m.Id equals resMuestreo.MuestreoId
                                   join costo in _dbContext.ParametrosCostos on resMuestreo.ParametroId equals costo.ParametroId
                                   where m.EstatusId  == estatusId
                                   select new AcumuladosResultadoDto
                                   {
                                       MuestreoId = m.Id,
                                       claveUnica = $"{vpm.ClaveMuestreo}{resMuestreo.Parametro.ClaveParametro}",
                                       ClaveMonitoreo = vpm.ClaveMuestreo ?? string.Empty,
                                       ClaveSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                       NombreSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                       FechaProgramada = m.ProgramaMuestreo.DiaProgramado.ToString(),
                                       FechaRealizacion = m.FechaRealVisita.ToString() ?? string.Empty,
                                       HoraInicio = $"{m.HoraInicio:hh\\:mm\\:ss}" ?? string.Empty,
                                       HoraFin = $"{m.HoraFin:hh\\:mm\\:ss}" ?? string.Empty,
                                       TipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion ?? string.Empty,
                                       SubTipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.SubtipoCuerpoAgua.Descripcion,
                                       Laboratorio = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion ?? string.Empty,
                                       laboratorioRealizoMuestreo = resMuestreo.Laboratorio.Descripcion ?? string.Empty,
                                       LaboratorioSubrogado = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion ?? string.Empty,
                                       subGrupo = resMuestreo.Parametro.IdSubgrupoNavigation.Descripcion,
                                       claveParametro = resMuestreo.Parametro.ClaveParametro,
                                       parametro = resMuestreo.Parametro.Descripcion,
                                       unidadMedida = resMuestreo.Parametro.IdUnidadMedidaNavigation.Descripcion,
                                       resultado = resMuestreo.Resultado,
                                       ProgramaAnual = m.AnioOperacion.ToString() ?? string.Empty,
                                       Estatus = m.Estatus.Descripcion,
                                       TipoSitio = m.ProgramaMuestreo.ProgramaSitio.TipoSitio.TipoSitio1.ToString() ?? string.Empty,
                                       DireccionLocal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Descripcion ?? string.Empty,
                                       OrganismoCuenca = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Clave ?? string.Empty,
                                       costoParametro = costo.Precio,
                                       NumeroEntrega = m.NumeroEntrega.ToString() ?? string.Empty,
                                       fechaEntrega = resMuestreo.FechaEntrega.ToString("dd/MM/yy") ?? string.Empty,
                                       idResultadoLaboratorio = (long)resMuestreo.IdResultadoLaboratorio



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
                                       FechaRealizacion = resultados.FechaRealizacion.ToString() ?? string.Empty,
                                       FechaProgramada = resultados.FechaProgramada.ToString(),
                                       diferenciaDias = Convert.ToInt32(resultados.DiferenciaEnDias.ToString()),
                                       fechaEntregaTeorica = resultados.FechaEntregaTeorica.ToString() ?? string.Empty,
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
                                       cumpleFechaEntrega = (resultados.NumFechasNOCumplidas > 0) ? "NO" : "SI"
                                   }).Where(x => anios.Contains(x.anioOperacion) && numeroCarga.Contains(Convert.ToInt32(x.NumeroEntrega)) && x.estatusId==estatusId).ToListAsync();

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

    }
}
