using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IValoresSupervisionMuestreoRepository:IRepository<ValoresSupervisionMuestreo>
    {
        List<ValoresSupervisionMuestreo> ConvertiraValoresSupervisionMuestreo(List<ClasificacionCriterioDto> lstClasificacionesCriterio, long supervisionMuestreoId);
    }
}
