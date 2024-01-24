using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class ValidacionEvidenciasRepository : Repository<AvisoRealizacion>, IValidacionEvidenciasRepository
    {
        public ValidacionEvidenciasRepository(SicaContext context) : base(context)
        {
        }


        public List<AvisoRealizacion> ConvertToMuestreosList(List<AvisoRealizacionDto> cargaMuestreoDtoList)
        {
            var laboratorios = _dbContext.Laboratorios.ToList();
            var parametros = _dbContext.ParametrosGrupo.ToList();
            var tipoSitio = _dbContext.TipoSitio.ToList();
            var tipoSupervision = _dbContext.TipoSupervision.ToList();
            var brigada = _dbContext.BrigadaMuestreo.ToList();




            //var cargaMuestreos = cargaMuestreoDtoList.Select(s => new { s.ClaveMuestreo, s.ClaveSitio, s.TipoCuerpoAgua, s.FechaRealVisita, s.HoraInicioMuestreo, s.HoraFinMuestreo, s.AnioOperacion, s.NoEntrega }).Distinct().ToList();
            var muestreos = (from cm in cargaMuestreoDtoList
                             join vcm in _dbContext.Laboratorios on cm.Laboratorio equals vcm.Nomenclatura
                             join tipSitio in _dbContext.TipoSitio on cm.TipoSitio.Trim().ToUpper() equals tipSitio.Descripcion.ToUpper()
                             join tipSupervision in _dbContext.TipoSupervision on cm.TipoSupervision.Trim().ToUpper() equals tipSupervision.Descripcion.Trim().ToUpper()
                             join brig in _dbContext.BrigadaMuestreo on cm.BrigadaMuestreo.Trim().ToUpper() equals brig.Descripcion.ToUpper()
                             select new AvisoRealizacion
                             {
                                 ClaveMuestreo = cm.ClaveMuestreo,
                                 ClaveSitio = cm.ClaveSitio,
                                 TipoSitioId = tipSitio.Id,
                                 LaboratorioId = vcm.Id,
                                 ConEventualidades = cm.ConEventualidades,
                                 FechaProgramada = Convert.ToDateTime(cm.FechaProgramada),
                                 FechaRealVisita = Convert.ToDateTime(cm.FechaRealVisita),
                                 BrigadaMuestreoId = brig.Id,
                                 ConQcmuestreo = cm.ConQCMuestreos,
                                 FolioEventualidad = cm.FolioEventualidad ?? null,
                                 FechaAprobacionEventualidad = (cm.FechaAprobacionEventualidad != null && cm.FechaAprobacionEventualidad != string.Empty) ? Convert.ToDateTime(cm.FechaAprobacionEventualidad) : null,
                                 TipoSupervisionId = tipSupervision.Id,
                                 DocumentoEventualidad = cm.DocumentoEventualidad ?? null,
                                 TipoEventualidad = cm.TipoEventualidad ?? null,
                                 FechaReprogramacion = (cm.FechaReprogramacion != null && cm.FechaReprogramacion != string.Empty) ? Convert.ToDateTime(cm.FechaReprogramacion) : null

                             }).ToList();

            return muestreos.ToList();
        }
    }
}
