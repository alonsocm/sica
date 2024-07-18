using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    internal class ParametroRepository : Repository<ParametrosGrupo>, IParametroRepository
    {
        public ParametroRepository(SicaContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<ParametroDTO> GetParametros()
        {
            var parametros = from p in _dbContext.ParametrosGrupo
                             select new ParametroDTO
                             {
                                 Id = (int)p.Id,
                                 Clave = p.ClaveParametro,
                                 Descripcion = p.Descripcion,
                                 UnidadMedida = p.IdUnidadMedidaNavigation == null ? string.Empty : p.IdUnidadMedidaNavigation.Descripcion,
                                 Grupo = p.GrupoParametro == null ? string.Empty : p.GrupoParametro.Descripcion,
                                 SubGrupo = p.IdSubgrupoNavigation == null ? string.Empty : p.IdSubgrupoNavigation.Descripcion,
                                 ParametroPadre = p.ParametroPadre == null ? string.Empty : p.ParametroPadre.ClaveParametro,
                                 Orden = (int)p.Orden
                             };

            return parametros;
        }

        public IEnumerable<GrupoParametroDTO> GetGruposParametros()
        {
            var grupos = from p in _dbContext.GrupoParametro
                         select new GrupoParametroDTO { Id = p.Id, Descripcion = p.Descripcion };

            return grupos;
        }

        public IEnumerable<UnidadMedidaDTO> GetUnidadesMedida()
        {
            var unidadesMedida = from p in _dbContext.UnidadMedida
                                 select new UnidadMedidaDTO { Id = (int)p.Id, Descripcion = p.Descripcion };

            return unidadesMedida;
        }

        public IEnumerable<SubGrupoAnaliticoDTO> GetSubGrupoAnalitico()
        {
            var subGrupos = from p in _dbContext.SubgrupoAnalitico
                            select new SubGrupoAnaliticoDTO { Id = (int)p.Id, Descripcion = p.Descripcion };

            return subGrupos;
        }
    }
}
