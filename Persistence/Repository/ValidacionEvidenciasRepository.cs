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
                             join vcm in _dbContext.Laboratorios on cm.Laboratorio.ToUpper() equals vcm.Nomenclatura.ToUpper()
                             join tipSitio in _dbContext.TipoSitio on cm.TipoSitio.ToUpper() equals tipSitio.Descripcion.ToUpper()
                             join tipSupervision in _dbContext.TipoSupervision on cm.TipoSupervision.ToUpper() equals tipSupervision.Descripcion.ToUpper()
                             join brig in _dbContext.BrigadaMuestreo on cm.BrigadaMuestreo.ToUpper() equals brig.Descripcion.ToUpper()
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
                                 FolioEventualidad = cm.FolioEventualidad,
                                 FechaAprobacionEventualidad = Convert.ToDateTime(cm.FechaAprobacionEventualidad),
                                 TipoSupervisionId = tipSupervision.Id,
                                 DocumentoEventualidad = cm.DocumentoEventualidad,
                                 TipoEventualidad = cm.TipoEventualidad,
                                 FechaReprogramacion = Convert.ToDateTime(cm.FechaReprogramación)

                             }).ToList();

            return muestreos;
        }
    }
}
