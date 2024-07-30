using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ITipoCuerpoAguaRepository : IRepository<TipoCuerpoAgua>
    {
        public IEnumerable<TipoCuerpoAguaDto> GetTipoCuerpoAgua();
    }
}
