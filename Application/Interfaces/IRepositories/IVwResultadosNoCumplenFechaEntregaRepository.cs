using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IVwResultadosNoCumplenFechaEntregaRepository: IRepository<VwResultadosNoCumplenFechaEntrega>
    { 
        List<VwResultadosNoCumplenFechaEntrega> ObtenerDatos();
    }
}
