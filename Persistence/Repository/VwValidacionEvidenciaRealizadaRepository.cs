using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{

    public class VwValidacionEvidenciaRealizadaRepository: Repository<VwValidacionEvidenciaRealizada>, IVwValidacionEvidenciaRealizadaRepository
    {
        public VwValidacionEvidenciaRealizadaRepository(SicaContext context) : base(context)
        {

        }

        public List<VwValidacionEvidenciaRealizada> ObtenerValidacionesRealizadas(bool rechazo)
        {

            return _dbContext.VwValidacionEvidenciaRealizada.Where(x => x.Rechazo == rechazo).ToList();
        }
    }

    
}
