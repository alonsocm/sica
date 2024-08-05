using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class TipoCuerpoAguaRepository : Repository<TipoCuerpoAgua>, ITipoCuerpoAguaRepository
    {
        public TipoCuerpoAguaRepository(SicaContext context) : base(context) { }
        public IEnumerable<TipoCuerpoAguaDto> GetTipoCuerpoAgua()
        {
            var tipoCuerpoAgua = from t in _dbContext.TipoCuerpoAgua
                                 select new TipoCuerpoAguaDto
                                 {
                                     Id = t.Id,
                                     Descripcion = t.Descripcion,
                                     TipoHomologadoId = t.TipoHomologadoId != null ? (int)t.TipoHomologadoId : 0,
                                     TipoHomologadoDescripcion = t.TipoHomologado == null ? string.Empty : t.TipoHomologado.Descripcion,
                                     Activo = t.Activo ? "SI" : "NO",
                                     Frecuencia = t.Frecuencia,
                                     EvidenciasEsperadas = t.EvidenciasEsperadas,
                                     TiempoMinimoMuestreo = t.TiempoMinimoMuestreo
                                 };

            return tipoCuerpoAgua;
        }
    }
}
