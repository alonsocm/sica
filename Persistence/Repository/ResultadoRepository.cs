using Application.DTOs;
using Application.DTOs.Users;
using Application.Enums;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ResultadoRepository : Repository<ResultadoMuestreo>, IResultado
    {
        public ResultadoRepository(SICAContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ReplicaResultadoDto>> ObtenerReplicasResultados()
        {
            try
            {
                var estatus = new List<int>() { 10, 12, 17, 14, 15 };
                var registros = await (from r in _dbContext.ResultadoMuestreo
                                       where estatus.Contains((int)r.EstatusResultado)
                                       select new ReplicaResultadoDto
                                       {
                                           NoEntrega = r.Muestreo.NumeroEntrega.ToString()??string.Empty,
                                           ClaveUnica = $"{r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio}-{r.Muestreo.ProgramaMuestreo.DiaProgramado:ddMMyyyy}{r.Parametro.ClaveParametro}",
                                           ClaveSitio = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio,
                                           ClaveMonitoreo = $"{r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.ClaveSitio}-{r.Muestreo.ProgramaMuestreo.DiaProgramado:ddMMyyyy}",
                                           Nombre = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.NombreSitio,
                                           ClaveParametro = r.Parametro.ClaveParametro,
                                           Laboratorio = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Laboratorio.Descripcion??string.Empty,
                                           TipoCuerpoAgua = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion,
                                           TipoCuerpoAguaOriginal = r.Muestreo.ProgramaMuestreo.ProgramaSitio.Sitio.CuerpoTipoSubtipoAgua.SubtipoCuerpoAgua.Descripcion,
                                           Resultado = r.Resultado,
                                           EsCorrectoOCDL = r.EsCorrectoOcdl == null ? string.Empty : (bool)r.EsCorrectoOcdl ? "SI" : "NO",
                                           ObservacionOCDL = r.ObservacionesOcdl??string.Empty,
                                           EsCorrectoSECAIA = r.EsCorrectoSecaia == null ? string.Empty : (bool)r.EsCorrectoSecaia ? "SI" : "NO",
                                           ObservacionSECAIA = r.ObservacionesSecaia??string.Empty,
                                           ClasificacionObservacion = string.Empty,//TODO:Verificar a qué se refiere este campo
                                           CausaRechazo = string.Empty,//TODO:Verificar a qué se refiere este campo
                                           ResultadoAceptado = string.Empty, //TODO:Verificar si es necesario agregar un campo en ld BD
                                           ResultadoReplica = string.Empty, //TODO:Verificar si es necesario agregar un campo en ld BD
                                           EsMismoResultado = r.EsMismoResultado == null ? string.Empty : (bool)r.EsMismoResultado ? "SI" : "NO",
                                           ObservacionLaboratorio = r.ObservacionLaboratorio??string.Empty,//TODO: Se debe agregar nuevo campo en la BD?
                                           FechaReplicaLaboratorio = DateTime.Now.ToString("dd/MM/yy"),//TODO: Agregar nuevo campo en la BD
                                           ObservacionSRNAMECA = string.Empty,//TODO: Agregar campo en la BD
                                           Comentarios = string.Empty,//TODO: Comentarios de quien?
                                           FechaObservacionRENAMECA = DateTime.Now.ToString("dd/MM/yy"),//TODO: Agregar campo en la BD
                                           ResultadoAprobadoDespuesReplica = r.SeApruebaResultadoReplica == null ? string.Empty : (bool)r.SeApruebaResultadoReplica ? "SI" : "NO",//TODO: Se debería agregar nueva tabla?
                                           FechaEstatusFinal = DateTime.Now.ToString("dd/MM/yy"),//TODO: En qué momento se registrará esta fecha?
                                           UsuarioRevisor = string.Empty,//TODO: Se refiere al usuario revisor RENAMECA?
                                           EstatusResultado = r.EstatusResultadoNavigation.Descripcion??string.Empty,
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
    }
}
