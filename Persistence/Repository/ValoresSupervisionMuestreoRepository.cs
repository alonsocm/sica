using Application.DTOs;
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
    public class ValoresSupervisionMuestreoRepository : Repository<ValoresSupervisionMuestreo>,IValoresSupervisionMuestreoRepository
    {
        const string cumple = "CUMPLE";
        const string incumplimiento = "NOCUMPLE";
        const string na = "NOAPLICA";
        public ValoresSupervisionMuestreoRepository(SicaContext dbContext) : base(dbContext)
        {

        }
        public List<ValoresSupervisionMuestreo> ConvertiraValoresSupervisionMuestreo(List<ClasificacionCriterioDto> lstClasificacionesCriterio, long supervisionMuestreoId)
        {
            List<ValoresSupervisionMuestreo> lstValoresMuestreo = new List<ValoresSupervisionMuestreo>();

            lstClasificacionesCriterio.ForEach(clasificacionDto =>
            {
                clasificacionDto.Criterios.ForEach(criterioDto =>
                {
                    ValoresSupervisionMuestreo valor = new ValoresSupervisionMuestreo();
                    valor.CriterioSupervisionId = criterioDto.Id;
                    valor.Cumple = (criterioDto.Cumplimiento == cumple) ? true : ((criterioDto.Cumplimiento == incumplimiento) ? false : null);
                    valor.NoAplica = (criterioDto.Cumplimiento == na) ? true : null;
                    valor.ObservacionesCriterio = criterioDto.Observacion;
                    valor.SupervisionMuestreoId = supervisionMuestreoId;
                    lstValoresMuestreo.Add(valor);
                });
            });
            return lstValoresMuestreo;
        }
    }
}
