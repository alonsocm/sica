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

        public async Task<IEnumerable<ClasificacionCriterioDto>> ValoresSupervisionMuestreoDtoPorId(List<ValoresSupervisionMuestreo> valores)
        {
            //var valores = _dbContext.ValoresSupervisionMuestreo.Where(x => x.SupervisionMuestreoId == supervisionMuestreoId).ToList();
            List<int> criteriosSupervisionId = valores.Select(x => x.CriterioSupervisionId).ToList();
            List<ClasificacionCriterioDto> lstClasificacionCriterioDto = new List<ClasificacionCriterioDto>();

          
                lstClasificacionCriterioDto = await (from l in _dbContext.ClasificacionCriterio
                                   select new ClasificacionCriterioDto
                                   {
                                       Id = l.Id,
                                       Descripcion = l.Descripcion,
                                       Criterios = (from r in _dbContext.CriteriosSupervisionMuestreo
                                                    where r.ClasificacionCriterioId == l.Id && criteriosSupervisionId.Contains(r.Id)
                                                    select new CriterioDto
                                                    {
                                                        Id = r.Id,
                                                        Descripcion = r.Descripcion,
                                                        Obligatorio = r.Obligatorio,
                                                        Puntaje = r.Valor      
                                                    }).ToList()
                                   }).ToListAsync();



            foreach (var dato in lstClasificacionCriterioDto)
            {
                foreach (var item in dato.Criterios)
                {
                    var valSupervision = valores.Where(x => x.CriterioSupervisionId == item.Id).FirstOrDefault();
                    item.ValoresSupervisonMuestreoId = valSupervision.Id;
                    item.Cumplimiento = valSupervision.Resultado;


                }

            }

                    
            return lstClasificacionCriterioDto;
        }
    }
}
