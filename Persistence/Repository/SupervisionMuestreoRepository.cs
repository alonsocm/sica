using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{

    public class SupervisionMuestreoRepository : Repository<SupervisionMuestreo>, ISupervisionMuestreoRepository
    {

        public SupervisionMuestreoRepository(SicaContext dbContext) : base(dbContext)
        {

        }
        public SupervisionMuestreo ConvertirSupervisionMuestreo(SupervisionMuestreoDto supervisionMuestreo)
        {
            SupervisionMuestreo supervision = new SupervisionMuestreo();
            supervision.FehaMuestreo = Convert.ToDateTime(supervisionMuestreo.FechaMuestreo);
            supervision.HoraInicio = TimeSpan.Parse(supervisionMuestreo.HoraInicio);
            supervision.HoraTermino = TimeSpan.Parse(supervisionMuestreo.HoraTermino);
            supervision.HoraTomaMuestra = TimeSpan.Parse(supervisionMuestreo.HoraTomaMuestra);
            supervision.PuntajeObtenido = supervisionMuestreo.PuntajeObtenido;
            supervision.OrganismosDireccionesRealizaId = supervisionMuestreo.OrganismosDireccionesRealizaId;
            supervision.OrganismoCuencaReportaId = supervisionMuestreo.OrganismoCuencaReportaId;
            supervision.SupervisorConagua = supervisionMuestreo.SupervisorConagua;
            supervision.SitioId = supervisionMuestreo.SitioId;
            supervision.ClaveMuestreo = supervisionMuestreo.ClaveMuestreo;
            supervision.LatitudToma = supervisionMuestreo.LatitudToma;
            supervision.LongitudToma = supervisionMuestreo.LongitudToma;
            supervision.LaboratorioRealizaId = supervisionMuestreo.LaboratorioRealizaId;
            supervision.ResponsableTomaId = supervisionMuestreo.ResponsableTomaId;
            supervision.ResponsableMedicionesId = supervisionMuestreo.ResponsableMedicionesId;
            supervision.ObservacionesMuestreo = supervisionMuestreo.ObservacionesMuestreo;


            return supervision;
        }

        public async Task<IEnumerable<ClasificacionCriterioDto>> ObtenerCriterios()
        {
            var datos = await (from l in _dbContext.ClasificacionCriterio
                               select new ClasificacionCriterioDto
                               {
                                   Id = l.Id,
                                   Descripcion = l.Descripcion,
                                   Criterios = (from r in _dbContext.CriteriosSupervisionMuestreo
                                                where r.ClasificacionCriterioId == l.Id
                                                select new CriterioDto
                                                {
                                                    Id = r.Id,
                                                    Descripcion = r.Descripcion,
                                                    Obligatorio = r.Obligatorio,
                                                    Puntaje = r.Valor
                                                }).ToList()
                               }).ToListAsync();
            return datos;
        }
    }
}
