using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
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
                                 HoraMuestreo = cm.HoraMuestreo,
                                 TipoCuerpoAgua = cm.TipoCuerpoAgua,
                                 SubtipoCuerpoAgua = cm.SubtipoCuerpoAgua,
                                 LaboratorioRealizoMuestreo = cm.LaboratorioRealizoMuestreo,
                                 LaboratorioSubrogado = cm.LaboratorioSubrogado,
                                 GrupoParametro = cm.GrupoParametro,
                                 ClaveParametro = p.ClaveParametro,
                                 Parametro = p.Descripcion,
                                 Resultado = cm.Resultado,
                                 UnidadMedida = cm.UnidadMedida
                             }).ToList();

            return muestreos;
        }
    }
}
