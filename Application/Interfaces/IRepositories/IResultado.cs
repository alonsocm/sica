using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IResultado : IRepository<ResultadoMuestreo>
    {
        Task<IEnumerable<ReplicaResultadoDto>> ObtenerReplicasResultados();
        Task<List<ResultadoValidacionReglasDto>> ObtenerResultadosValidacion(List<long> muestreosId);
        Task<IEnumerable<ResultadoParametroReglasDto>> ObtenerResultadosParaReglas(long muestreoId);
    }
}
