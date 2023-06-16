using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Persistence.Repository
{
    public class Replicas : Repository<AprobacionResultadoMuestreo>, IReplicas
    {
        public Replicas(SICAContext context) : base(context)
        {

        }
        public async Task<IEnumerable<AprobacionResultadoMuestreoDto>> GetResumenResultadosReplicaAsync(int userId)
        {

            var Usr = await (
                _dbContext.Usuario.Include(t => t.DireccionLocal)
                                        .Include(t => t.Cuenca)
                                        .Where(t => t.Id == userId).FirstOrDefaultAsync()
            );
            try
            {           

            IEnumerable<AprobacionResultadoMuestreoDto> revisionResultado = (

                    from result in _dbContext.VwReplicaRevisionResultado
                    where result.EstatusMuestreoId == 8 
                       && result.EsCorrectoOcdl != null
                       && result.EsCorrectoSecaia != null
                       && (result.EstatusId == 13 || result.EstatusId == null)
                       //&& result.EstatusId == null

                    select new AprobacionResultadoMuestreoDto
                    {
                        MuestreoId = result.MuestreoId,
                        ParametroId = result.ParametroId,
                        UsuarioRevisionId = result.UsuarioRevisionId,
                        ResultadoMuestreoId = result.ResultadoMuestreoId,
                        EstatusResultadoId = result.EstatusId,
                        NoEntrega = result.NumeroEntrega,
                        ClaveUnica = result.ClaveUnica,
                        ClaveSitio = result.ClaveSitio,
                        ClaveMonitoreo = result.ClaveMonitoreo,
                        NombreSitio = result.NombreSitio,
                        ClaveParametro = result.ClaveParametro,
                        Laboratorio = result.Laboratorio,
                        TipoCuerpoAgua = result.TipoCuerpoAgua,
                        TipoCuerpoAguaOriginal = result.TipoCuerpoAguaOriginal,
                        Resultado = result.Resultado,
                        EsCorrectoOCDL = result.EsCorrectoOcdl==null?"":(result.EsCorrectoOcdl==true?"SI":"NO"),                       
                        ObservacionOCDL = Convert.ToString(result.ObservacionesOcdl),
                        EsCorrectoSECAIA = result.EsCorrectoSecaia == null ? "" : (result.EsCorrectoSecaia == true ? "SI" : "NO"),
                        ObservacionSECAIA = result.ClasificacionObservacion,
                        ApruebaResultado = result.ApruebaResultado == null ? "" : (result.ApruebaResultado == true ? "SI" : "NO"),
                        ComentariosAprobacionResultados = result.ComentariosAprobacionResultados,
                        FechaAprobRechazo = result.FechaAprobRechazo==null? "": result.FechaAprobRechazo.Value.Date.ToString("dd/MM/yyyy"),
                        UsuarioRevision = result.NombreUsuario,
                        estatusResultado = result.Estatus == null? "": result.Estatus
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
