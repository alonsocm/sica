using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Ardalis.Specification.EntityFrameworkCore;

namespace Persistence.Repository
{
    public class ResumenResMuestreosRepository : Repository<ResultadoMuestreo>, IResumenResRepository

    {
        const int estatusEnviado = (int)Application.Enums.EstatusMuestreo.Enviado;
        const int estatusExtensionFecha = (int)Application.Enums.EstatusMuestreo.EnviadoConExtensionFecha;
        const int estatusVencido = (int)Application.Enums.EstatusMuestreo.PendienteDeEnvioAprobacionFinal;

        public ResumenResMuestreosRepository(SICAContext context) : base(context) { }

        public async Task<IEnumerable<ResultadoMuestreoDto>> GetResumenResultadosMuestreoAsync(int estatusId, int userId, bool isOCDL)
        {
            List<ResultadoMuestreoDto> resultadosMuestreos = new();

            if (_dbContext.Muestreo.Any(x => (isOCDL) ? (x.EstatusOcdl == estatusId) : (x.EstatusSecaia == estatusId)))
            {
                var usuarioDl = await _dbContext.Usuario.Include(t => t.DireccionLocal)
                                                        .Include(t => t.Cuenca)
                                                        .Where(t => t.Id == userId)
                                                        .FirstOrDefaultAsync();

                resultadosMuestreos = (from rm in _dbContext.ResultadoMuestreo
                                       join vcm in _dbContext.VwClaveMuestreo on rm.Muestreo.ProgramaMuestreoId equals vcm.ProgramaMuestreoId
                                       join users in _dbContext.Usuario on rm.Muestreo.UsuarioRevisionOcdlid equals users.Id into usuario
                                       from usr in usuario.DefaultIfEmpty()
                                       where
                                             (isOCDL ? (rm.Muestreo.EstatusOcdl == estatusId || rm.Muestreo.EstatusOcdl == estatusVencido)
                                                     : (rm.Muestreo.EstatusSecaia == estatusId || rm.Muestreo.EstatusSecaia == estatusVencido))
                                       &&
                                             (rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.OcuencaId == usuarioDl.CuencaId ||
                                              rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.DlocalId == usuarioDl.DireccionLocalId)
                                       orderby rm.Parametro.ClaveParametro ascending
                                       select new ResultadoMuestreoDto
                                       {
                                           Id = rm.Id,
                                           MuestreoId = rm.Muestreo.Id,
                                           ParametroId = rm.ParametroId,
                                           Resultado = rm.Resultado,
                                           //checar el valor de observaciones OCDL y SECAIA
                                           Observaciones = (rm.ObservacionesOcdlid == null || rm.ObservacionesOcdlid == 11) ? rm.ObservacionesOcdl : rm.ObservacionesOcdlNavigation.Descripcion,
                                           ObservacionSECAIA = (rm.ObservacionesSecaiaid == null || rm.ObservacionesSecaiaid == 11) ? rm.ObservacionesSecaia : rm.ObservacionesSecaiaNavigation.Descripcion,
                                           ObservacionSECAIAId = rm.ObservacionesSecaiaid,
                                           NoEntregaOCDL = rm.Muestreo.NumeroEntrega.ToString() ?? "0",
                                           ClaveUnica = $"{vcm.ClaveMuestreo}{rm.Parametro.ClaveParametro}",
                                           ClaveSitio = rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                           ClaveMonitoreo = $"{vcm.ClaveMuestreo}",
                                           NombreSitio = rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                           ClaveParametro = rm.Parametro.ClaveParametro,
                                           Laboratorio = rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion ?? "Sin laboratorio asignado",
                                           TipoCuerpoAgua = rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion,
                                           TipoCuerpoAguaOriginal = rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion,
                                           FechaRevision = rm.Muestreo.FechaRevisionOcdl.ToString() ?? string.Empty,
                                           NombreUsuario = isOCDL ? $"{usr.Nombre} {usr.ApellidoPaterno} {usr.ApellidoMaterno}" : $"{rm.Muestreo.UsuarioRevisionSecaia.Nombre} {rm.Muestreo.UsuarioRevisionSecaia.ApellidoPaterno} {rm.Muestreo.UsuarioRevisionSecaia.ApellidoMaterno}",
                                           EstatusResultado = isOCDL ? rm.Muestreo.EstatusOcdlNavigation.Descripcion : rm.Muestreo.EstatusSecaiaNavigation.Descripcion,
                                           TipoAprobacion = rm.Muestreo.TipoAprobacion != null ? rm.Muestreo.TipoAprobacion.Descripcion.ToString() : string.Empty,
                                           EstatusId = rm.Muestreo.EstatusId,
                                           CuerpoTipoSubtipo = rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAguaId,
                                           EsCorrectoResultado = (rm.EsCorrectoOcdl == true) ? "SI" : "NO",
                                           FechaRealizacion = rm.Muestreo.FechaRealVisita.ToString() ?? string.Empty,
                                           OrganismoCuenca = rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Descripcion,
                                           DireccionLocal = rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal != null ? rm.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Descripcion : string.Empty,
                                           FechaLimiteRevision = rm.Muestreo.FechaLimiteRevision.ToString(),
                                           EstatusOCDL = rm.Muestreo.EstatusOcdl,
                                           EstatusSECAIA = rm.Muestreo.EstatusSecaia
                                       }).ToList();
            }

            return resultadosMuestreos;
        }

        public async Task<IEnumerable<ResultadoMuestreoDto>> GetResumenResultados(int userId, bool isOCDL)
        {
            var Usr = await (_dbContext.Usuario.Include(t => t.DireccionLocal)
                                                    .Include(t => t.Cuenca)
                                                    .Where(t => t.Id == userId).FirstOrDefaultAsync());

            IQueryable<Muestreo> muestreos;

            // SI TIENE PERFIL A NIVEL DIRECCION LOCAL...
            if (Usr.DireccionLocalId != null)
            {
                muestreos = _dbContext.Muestreo.Include(t => t.ProgramaMuestreo)
                                               .ThenInclude(t => t.ProgramaSitio)
                                               .ThenInclude(t => t.Sitio)
                                               .ThenInclude(t => t.CuencaDireccionesLocales).Where(t => t.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.DlocalId
                                                == Usr.DireccionLocalId && (t.EstatusId == estatusEnviado || t.EstatusId == estatusExtensionFecha));
            }

            else if (Usr.CuencaId != null)
            {
                muestreos = _dbContext.Muestreo.Include(t => t.ProgramaMuestreo)
                                               .ThenInclude(t => t.ProgramaSitio)
                                               .ThenInclude(t => t.Sitio)
                                               .ThenInclude(t => t.CuencaDireccionesLocales).Where(t => t.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.OcuencaId
                                               == Usr.CuencaId && (t.EstatusId == estatusEnviado || t.EstatusId == estatusExtensionFecha));
            }
            else
            {
                muestreos = _dbContext.Muestreo.Include(t => t.ProgramaMuestreo)
                                               .ThenInclude(t => t.ProgramaSitio)
                                               .ThenInclude(t => t.Sitio)
                                               .ThenInclude(t => t.CuencaDireccionesLocales).Where(t => t.EstatusId == estatusEnviado || t.EstatusId == estatusExtensionFecha);
            }

            muestreos = (isOCDL) ? muestreos.Where(x => x.EstatusOcdl == null) : muestreos.Where(x => x.EstatusSecaia == null);

            var lstSalida = from m in muestreos
                            join vpm in _dbContext.VwClaveMuestreo on m.ProgramaMuestreoId equals vpm.ProgramaMuestreoId
                            select new ResultadoMuestreoDto
                            {
                                MuestreoId = m.Id,
                                NoEntregaOCDL = m.NumeroEntrega.ToString() + "-" + m.AnioOperacion.ToString(),
                                NombreSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio.ToString(),
                                FechaRealizacion = m.FechaRealVisita.ToString(),
                                FechaLimiteRevision = m.FechaLimiteRevision.ToString(),
                                fechaLimiteRevisionVencidos = m.FechaLimiteRevision,
                                ClaveSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                ClaveMonitoreo = vpm.ClaveMuestreo,
                                Laboratorio = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion ?? "Sin laboratorio asignado",
                                TipoCuerpoAgua = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion,
                                TipoHomologado = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.TipoHomologado.Descripcion,
                                FechaRevision = m.FechaRevisionOcdl.ToString() ?? string.Empty,
                                NombreUsuario = m.UsuarioRevisionOcdl.Nombre + ' ' + m.UsuarioRevisionOcdl.ApellidoPaterno + ' ' + m.UsuarioRevisionOcdl.ApellidoMaterno,
                                EstatusResultado = m.Estatus.Descripcion,
                                TipoAprobacion = m.TipoAprobacion != null ? m.TipoAprobacion.Descripcion.ToString() : string.Empty,
                                EstatusId = m.EstatusId,
                                CuerpoTipoSubtipo = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAguaId,
                                OrganismoCuenca = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Descripcion,
                                DireccionLocal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal != null ? m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Dlocal.Descripcion :
                                m.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.Ocuenca.Descripcion,
                                EstatusOCDL = m.EstatusOcdl,
                                EstatusSECAIA = m.EstatusSecaia,
                                TipoCuerpoAguaOriginal = m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.TipoHomologado == null ? string.Empty :
                                                         m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.TipoHomologado.Descripcion

                            };

            List<ResultadoMuestreoDto> lstResultados = await lstSalida.ToListAsync();

            foreach (var item in lstResultados)
            {
                var parametros = await (from rm in _dbContext.ResultadoMuestreo
                                        join muestreo in _dbContext.Muestreo on rm.MuestreoId equals muestreo.Id
                                        join pgpos in _dbContext.ParametrosGrupo on rm.ParametroId equals pgpos.Id
                                        join vcm in _dbContext.VwClaveMuestreo on muestreo.ProgramaMuestreoId equals vcm.ProgramaMuestreoId
                                        where muestreo.Id == item.MuestreoId

                                        select new ParametrosDto
                                        {
                                            Id = pgpos.Id,
                                            MuestreoId = muestreo.Id,
                                            Resulatdo = rm.Resultado,
                                            ClaveParametro = pgpos.ClaveParametro,
                                            ObservacionesOCDLId = rm.ObservacionesOcdlid,
                                            IsCorrecto = rm.EsCorrectoOcdl,
                                            NombreParametro = pgpos.Descripcion,
                                            ClaveUnica = $"{vcm.ClaveMuestreo}{pgpos.ClaveParametro}",
                                        }
                    ).ToListAsync();

                var archivosn = await (from arch in _dbContext.EvidenciaMuestreo
                                       join muestreo in _dbContext.Muestreo on arch.MuestreoId equals muestreo.Id

                                       where muestreo.Id == item.MuestreoId
                                       select new EvidenciaDto
                                       {
                                           NombreArchivo = arch.NombreArchivo,
                                           TipoEvidencia = arch.TipoEvidenciaMuestreoId,
                                           Sufijo = arch.TipoEvidenciaMuestreo.Sufijo
                                       }
                                       ).ToListAsync();
                item.lstEvidencias = archivosn.ToList();
                item.lstParametros = parametros.ToList();
            }
            return lstResultados;
        }

        public List<ResultadoMuestreo> ConvertMuestreosParamsList(List<UpdateMuestreoExcelDto> resultadosDto)
        {
            var resultados = (from cm in resultadosDto
                              join vcm in _dbContext.VwClaveMuestreo on cm.ClaveMonitoreo equals vcm.ClaveMuestreo
                              join pm in _dbContext.ProgramaMuestreo on vcm.ProgramaMuestreoId equals pm.Id
                              join m in _dbContext.Muestreo on pm.Id equals m.ProgramaMuestreoId
                              join rm in _dbContext.ResultadoMuestreo on m.Id equals rm.MuestreoId
                              join cp in _dbContext.ParametrosGrupo on rm.ParametroId equals cp.Id
                              where cm.ClaveParametro == cp.ClaveParametro
                              select new ResultadoMuestreo
                              {
                                  Id = rm.Id,
                                  ObservacionesOcdl = cm.ObservacionOCDL,
                                  ObservacionesOcdlid = cm.ObservacionOCDL != "" ? 11 : null,
                                  EsCorrectoOcdl = cm.ObservacionOCDL == "",
                                  MuestreoId = rm.MuestreoId,
                                  ParametroId = rm.ParametroId,
                              }).ToList();

            var observaciones = (from a in _dbContext.Observaciones select new Observaciones { Id = a.Id, Descripcion = a.Descripcion.ToUpper().Trim() }).ToList();
            //POSTERIORMENTE CAMBIARLO E INTEGRARLO CUANDO SE CREA LA ENTIDAD, PASO ANTERIOR
            foreach (var resultado in resultados)
            {
                if (resultado.ObservacionesOcdlid == 11)
                {
                    List<Observaciones> valor = observaciones.Where(x => x.Descripcion == resultado.ObservacionesOcdl?.ToUpper().Trim()).ToList();
                    resultado.ObservacionesOcdl = (valor.Count > 0) ? "" : resultado.ObservacionesOcdl;
                    resultado.ObservacionesOcdlid = (valor.Count > 0) ? valor[0].Id : resultado.ObservacionesOcdlid;
                }
            }

            return resultados;
        }

        public List<ResultadoMuestreo> ConvertMuestreosParamsListSECAIA(List<UpdateMuestreoSECAIAExcelDto> updateMuestreoExcelDtoList)
        {
            var cargaMuestreos = updateMuestreoExcelDtoList.ToList();
            var observaciones = (from a in _dbContext.Observaciones select new Observaciones { Id = a.Id, Descripcion = a.Descripcion.ToUpper().Trim() }).ToList();
            var muestreos = (from cm in cargaMuestreos
                             join vcm in _dbContext.VwClaveMuestreo on cm.ClaveMonitoreo equals vcm.ClaveMuestreo
                             join pm in _dbContext.ProgramaMuestreo on vcm.ProgramaMuestreoId equals pm.Id
                             join m in _dbContext.Muestreo on pm.Id equals m.ProgramaMuestreoId
                             join rm in _dbContext.ResultadoMuestreo on m.Id equals rm.MuestreoId
                             join cp in _dbContext.ParametrosGrupo on rm.ParametroId equals cp.Id
                             where cm.ClaveParametro == cp.ClaveParametro
                             select new ResultadoMuestreo
                             {
                                 Id = rm.Id,
                                 ObservacionesSecaia = cm.ObservacionSECAIA,
                                 ObservacionesSecaiaid = cm.ObservacionSECAIA != "" ? 11 : null,
                                 Resultado = cm.Resultado,
                                 MuestreoId = rm.MuestreoId,
                                 ParametroId = rm.ParametroId,
                             }).ToList();

            //POSTERIORMENTE CAMBIARLO E INTEGRARLO CUANDO SE CREA LA ENTIDAD, PASO ANTERIOR
            foreach (var item in muestreos)
            {
                if (item.ObservacionesSecaiaid == 11)
                {
                    List<Observaciones> valor = observaciones.Where(x => x.Descripcion == item.ObservacionesSecaia?.ToUpper().Trim()).ToList();
                    item.ObservacionesSecaia = (valor.Count > 0) ? "" : item.ObservacionesSecaia;
                    item.ObservacionesSecaiaid = (valor.Count > 0) ? valor[0].Id : item.ObservacionesSecaiaid;
                }
            }
            return muestreos;
        }

        public async Task<IEnumerable<ResultadoMuestreoDto>> GetResumenResultadosTemp(int userId, int cuerpAgua, int estatusId, int anio)
        {
            var usr = await (_dbContext.Usuario.Include(t => t.DireccionLocal)
                                                    .Include(t => t.Cuenca)
                                                    .Where(t => t.Id == userId).FirstOrDefaultAsync());

            IQueryable<Muestreo> muestreos = _dbContext.Muestreo.Where(x => (anio == 0 ? null : anio) == null || anio == x.AnioOperacion)
                                                                .Include(t => t.ProgramaMuestreo)
                                                                .ThenInclude(t => t.ProgramaSitio)
                                                                .ThenInclude(t => t.Sitio)
                                                                .ThenInclude(t => t.CuencaDireccionesLocales);

            if (usr.DireccionLocalId != null)
            {
                muestreos = muestreos.Where(t => t.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.DlocalId
                                                                    == usr.DireccionLocalId && t.EstatusId == estatusEnviado  || t.EstatusId == estatusExtensionFecha);
            }

            else if (usr.CuencaId != null)
            {
                muestreos = muestreos.Where(t => t.ProgramaMuestreo.ProgramaSitio.Sitio.CuencaDireccionesLocales.OcuencaId
                                                                == usr.CuencaId && t.EstatusId == estatusEnviado || t.EstatusId == estatusExtensionFecha);
            }

            var lstSalida = from m in muestreos
                            join vpm in _dbContext.VwClaveMuestreo on m.ProgramaMuestreoId equals vpm.ProgramaMuestreoId
                            join csta in _dbContext.CuerpoTipoSubtipoAgua on m.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAguaId equals csta.Id
                            join ca in _dbContext.CuerpoAgua on csta.CuerpoAguaId equals ca.Id
                            join tca in _dbContext.TipoCuerpoAgua on csta.TipoCuerpoAguaId equals tca.Id
                            //join stca in _dbContext.SubtipoCuerpoAgua on csta.SubtipoCuerpoAguaId equals stca.Id
                            join th in _dbContext.TipoHomologado on tca.TipoHomologadoId equals th.Id
                            join ts in _dbContext.TipoSitio on m.ProgramaMuestreo.ProgramaSitio.TipoSitioId equals ts.Id
                            where (th.Id == (cuerpAgua == 0 ? th.Id : cuerpAgua)) && (m.EstatusId == estatusId)
                            select new ResultadoMuestreoDto
                            {
                                MuestreoId = m.Id,
                                NoEntregaOCDL = m.NumeroEntrega.ToString() + "-" + m.AnioOperacion.ToString(),
                                ClaveSitioOriginal = ((m.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio) + (m.ProgramaMuestreo.DomingoSemanaProgramada.ToString("yyyy"))),
                                ClaveSitio = m.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                ClaveMonitoreo = vpm.ClaveMuestreo,
                                FechaRealizacion = m.FechaRealVisita.ToString(),
                                Laboratorio = m.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion ?? "Sin laboratorio asignado",
                                TipoCuerpoAguaId = tca.Id,
                                TipoCuerpoAgua = tca.Descripcion,
                                TipoHomologadoId = th.Id,
                                TipoHomologado = th.Descripcion,
                                TipoSitio = ts.TipoSitio1,
                                EstatusId = m.EstatusId
                            };

            List<ResultadoMuestreoDto> lstResultados = await lstSalida.ToListAsync();

            foreach (var item in lstResultados)
            {
                var parametros = from pg in _dbContext.ParametrosGrupo
                                 join rm in (
                                     from srm in _dbContext.ResultadoMuestreo
                                     where srm.MuestreoId == item.MuestreoId
                                     select srm
                                     ) on pg.Id equals rm.ParametroId into T
                                 from subQry in T.DefaultIfEmpty()
                                 orderby pg.Orden
                                 select new ParametrosDto
                                 {
                                     Id= pg.Id,
                                     ClaveParametro= pg.ClaveParametro,
                                     Resulatdo = subQry == null ? String.Empty : subQry.Resultado,
                                     MuestreoId = subQry == null ? 0 : subQry.MuestreoId,
                                     Descripcion=pg.Descripcion,
                                     Orden=(long)pg.Orden
                                 };

                List<ParametrosDto> lstparamfinal = new List<ParametrosDto>();
                lstparamfinal = parametros.ToList();
                item.lstParametros = lstparamfinal;
            }
            return lstResultados;
        }
    }
}