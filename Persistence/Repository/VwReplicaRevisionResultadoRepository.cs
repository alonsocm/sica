using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;


namespace Persistence.Repository
{
    public class VwReplicaRevisionResultadoRepository : Repository<VwReplicaRevisionResultado>, IVwReplicaRevisionResultadoRepository
    {
        public VwReplicaRevisionResultadoRepository(SicaContext context) : base(context)
        {
        }

        public bool ExisteClaveUnica(int noEntrega, string claveUnica)
        {
            var existe = _dbContext.VwReplicaRevisionResultado.Any(r => r.ClaveUnica == claveUnica
                                                                        && r.NumeroEntrega == noEntrega
                                                                        && (r.EstatusMuestreoId == 2 || r.EstatusMuestreoId == 3)
                                                                        && r.EstatusSecaia == null);
            return existe;
        }

        public async Task<IEnumerable<ReplicaResumenDto>> GetResumenResultadosReplicaAsync()
        {
            IEnumerable<ReplicaResumenDto> revisionResumen = await (
                    from result in _dbContext.VwReplicaRevisionResultado
                    where result.EstatusMuestreoId == (int)Application.Enums.EstatusMuestreo.Aprobaciónderesultados
                       || result.EstatusMuestreoId == (int)Application.Enums.EstatusMuestreo_1.OriginalesAprobados
                       && result.EstatusId == (int)Application.Enums.EstatusMuestreo_1.EnviadoResultadosAprobados
                       || result.EstatusId == (int)Application.Enums.EstatusMuestreo_1.RespuestaReplica_RespuestaLNR_EnviadoPenalizacion
                       || result.EstatusId == (int)Application.Enums.EstatusMuestreo_1.RespuestaReplica_RespuestaLNR_EnviadoResultadosAprobados

                    select new ReplicaResumenDto
                    {
                        NoEntrega = result.NumeroEntrega.ToString() ?? string.Empty,
                        ClaveUnica = result.ClaveUnica,
                        ClaveSitio = result.ClaveSitio,
                        ClaveMonitoreo = result.ClaveMonitoreo.ToString() ?? string.Empty,
                        NombreSitio = result.NombreSitio,
                        ClaveParametro = result.ClaveParametro,
                        Laboratorio = result.Laboratorio,
                        TipoCuerpoAgua = result.TipoCuerpoAgua,
                        TipoCuerpoAguaOriginal = result.TipoCuerpoAguaOriginal,
                        Resultado = result.Resultado,
                        estatusResultado = result.Estatus.ToString() ?? string.Empty
                    }
                    ).ToListAsync();

            return revisionResumen;
        }

        public async Task<IEnumerable<ReplicaDiferenteObtenerDto>> GetReplicaDiferenteAsync()
        {
            try
            {
                IEnumerable<ReplicaDiferenteObtenerDto> revisionResultado = (

                        from result in _dbContext.VwReplicaRevisionResultado
                        where result.EstatusId == 23
                           || result.EstatusId == 15
                           || result.EstatusId == 17

                        select new ReplicaDiferenteObtenerDto
                        {
                            MuestreoId = result.MuestreoId,
                            ParametroId = result.ParametroId,
                            UsuarioRevisionId = result.UsuarioRevisionId,
                            ResultadoMuestreoId = result.ResultadoMuestreoId,
                            EstatusResultadoId = result.EstatusId,
                            NoEntrega = result.NumeroEntrega.ToString() ?? string.Empty,
                            ClaveUnica = result.ClaveUnica,
                            ClaveSitio = result.ClaveSitio,
                            ClaveMonitoreo = result.ClaveMonitoreo.ToString() ?? string.Empty,
                            NombreSitio = result.NombreSitio,
                            ClaveParametro = result.ClaveParametro,
                            Laboratorio = result.Laboratorio,
                            TipoCuerpoAgua = result.TipoCuerpoAgua,
                            TipoCuerpoAguaOriginal = result.TipoCuerpoAguaOriginal,
                            ResultadoActualizadoporReplica = result.Resultado,
                            EsCorrectoOCDL = result.EsCorrectoOcdl == null ? "" : (result.EsCorrectoOcdl == true ? "SI" : "NO"),
                            ObservacionOCDL = Convert.ToString(result.ObservacionesOcdl),
                            EsCorrectoSECAIA = result.EsCorrectoSecaia == null ? "" : (result.EsCorrectoSecaia == true ? "SI" : "NO"),
                            ObservacionSECAIA = result.ObservacionesSecaia,
                            ClasificacionObservacion = result.ClasificacionObservacion.ToString() ?? string.Empty,
                            ObservacionSRENAMECA = result.ObservacionSrenameca == null ? "" : result.ObservacionSrenameca,
                            ComentariosAprobacionResultados = result.ComentariosReplicaDiferente == null ? "" : result.ComentariosReplicaDiferente,
                            FechaObservacionSRENAMECA = result.FechaObservacionSrenameca == null ? "" : result.FechaObservacionSrenameca.Value.Date.ToString("dd/MM/yyyy"),
                            SeApruebaResultadodespuesdelaReplica = result.SeApruebaResultadoReplica == null ? "" : (result.SeApruebaResultadoReplica == true ? "SI" : "NO"),
                            FechaEstatusFinal = result.FechaEstatusFinal == null ? "" : result.FechaEstatusFinal.Value.Date.ToString("dd/MM/yyyy"),
                            UsuarioRevision = result.NombreUsuario.ToString() ?? string.Empty,
                            estatusResultado = result.Estatus.ToString() ?? string.Empty,
                        }
                        ).ToList();

                return revisionResultado;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }
    }
}

