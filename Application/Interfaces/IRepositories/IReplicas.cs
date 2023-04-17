using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IReplicas : IRepository<AprobacionResultadoMuestreo>
    {
        #region Revisíon Resultado
        Task<IEnumerable<AprobacionResultadoMuestreoDto>> GetResumenResultadosReplicaAsync(int userId);
        #endregion

    }
}
