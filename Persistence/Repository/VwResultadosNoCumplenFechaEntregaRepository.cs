using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    
    public class VwResultadosNoCumplenFechaEntregaRepository: Repository<VwResultadosNoCumplenFechaEntrega>, IVwResultadosNoCumplenFechaEntregaRepository
    {
        public VwResultadosNoCumplenFechaEntregaRepository(SicaContext context) : base(context)
        {

        }

        public List<VwResultadosNoCumplenFechaEntrega> ObtenerDatos()
        {

            return _dbContext.VwResultadosNoCumplenFechaEntrega.ToList();
        }
    }
}
