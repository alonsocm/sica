using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Persistence.Repository
{
    public class VwValidacionEvienciasRepository: Repository<VwValidacionEviencias>, IVwValidacionEvienciasRepository
    {
        public VwValidacionEvienciasRepository(SicaContext context) : base(context)
        {

        }

        public List<VwValidacionEviencias> ObtenerDatosGenerales() {

            return _dbContext.VwValidacionEviencias.ToList();
        }

        //public async Task<IEnumerable<vwValidacionEvienciasDto>> ObtenerPuntos(List<VwValidacionEviencias> lstValidaciones)
        //{
        //    foreach (var dato in lstValidaciones)
        //    {
        //        dato
        //    }
        
        
        //}



    }
}
