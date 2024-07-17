using Application.DTOs.Catalogos;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IParametroRepository : IRepository<ParametrosGrupo>
    {
        public IEnumerable<ParametroDTO> GetParametros();
        public IEnumerable<GrupoParametroDTO> GetGruposParametros();
        public IEnumerable<SubGrupoAnaliticoDTO> GetSubGrupoAnalitico();
        public IEnumerable<UnidadMedidaDTO> GetUnidadesMedida();
    }
}
