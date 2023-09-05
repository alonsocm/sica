using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class ValoresSupervisionMuestreoRepository : Repository<ValoresSupervisionMuestreo>, IValoresSupervisionMuestreoRepository
    {
        const string cumple = "CUMPLE";
        const string incumplimiento = "NOCUMPLE";
        const string na = "NOAPLICA";
        public ValoresSupervisionMuestreoRepository(SicaContext dbContext) : base(dbContext)
        {

        }
        public List<ValoresSupervisionMuestreo> ConvertiraValoresSupervisionMuestreo(List<ClasificacionCriterioDto> lstClasificacionesCriterio, long supervisionMuestreoId)
        {
            List<ValoresSupervisionMuestreo> lstValoresMuestreo = new();

            lstClasificacionesCriterio.ForEach(clasificacionDto =>
            {
                clasificacionDto.Criterios.ForEach(criterioDto =>
                {
                    ValoresSupervisionMuestreo valor = new()
                    {
                        Id =  criterioDto.ValoresSupervisonMuestreoId ?? 0,
                        CriterioSupervisionId = criterioDto.Id,
                        Resultado = criterioDto.Cumplimiento,
                        ObservacionesCriterio = criterioDto.Observacion,
                        SupervisionMuestreoId = supervisionMuestreoId
                    };
                    lstValoresMuestreo.Add(valor);
                });
            });

            return lstValoresMuestreo;
        }        
    }
}
