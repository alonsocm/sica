using Application.DTOs;
using Application.DTOs.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface ISupervisionMuestreoRepository : IRepository<SupervisionMuestreo>
    {
        SupervisionMuestreo ConvertirSupervisionMuestreo(SupervisionMuestreoDto supervisionMuestreo);
        Task<IEnumerable<ClasificacionCriterioDto>> ObtenerCriterios();


    }
}
