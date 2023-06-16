using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IVwReplicaRevisionResultadoRepository : IRepository<VwReplicaRevisionResultado>
    {
        bool ExisteClaveUnica(int noEntrega, string claveUnica);

        Task<IEnumerable<ReplicaResumenDto>> GetResumenResultadosReplicaAsync();
        Task<IEnumerable<ReplicaDiferenteObtenerDto>> GetReplicaDiferenteAsync();
    }
}
