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
            SupervisionMuestreo supervision = new()
            {
                Id = supervisionMuestreo.Id,
                FehaMuestreo = Convert.ToDateTime(supervisionMuestreo.FechaMuestreo),
                HoraInicio = TimeSpan.Parse(supervisionMuestreo.HoraInicio),
                HoraTermino = TimeSpan.Parse(supervisionMuestreo.HoraTermino),
                HoraTomaMuestra = TimeSpan.Parse(supervisionMuestreo.HoraTomaMuestra),
                PuntajeObtenido = supervisionMuestreo.PuntajeObtenido,
                OrganismosDireccionesRealizaId = supervisionMuestreo.OrganismosDireccionesRealizaId,
                OrganismoCuencaReportaId = supervisionMuestreo.OrganismoCuencaReportaId,
                SupervisorConagua = supervisionMuestreo.SupervisorConagua,
                SitioId = supervisionMuestreo.SitioId,
                ClaveMuestreo = supervisionMuestreo.ClaveMuestreo,
                LatitudToma = supervisionMuestreo.LatitudToma,
                LongitudToma = supervisionMuestreo.LongitudToma,
                LaboratorioRealizaId = supervisionMuestreo.LaboratorioRealizaId,
                ResponsableTomaId = supervisionMuestreo.ResponsableTomaId,
                ResponsableMedicionesId = supervisionMuestreo.ResponsableMedicionesId,
                ObservacionesMuestreo = supervisionMuestreo.ObservacionesMuestreo
            };

            return supervision;
        }

        public async Task<bool> EliminarSupervision(long supervisionId)
        {
            SupervisionMuestreo supervisionMuestreo = await _dbContext.SupervisionMuestreo.Where(f => f.Id == supervisionId).Include("EvidenciaSupervisionMuestreo").Include("ValoresSupervisionMuestreo")?.FirstOrDefaultAsync();

            if (supervisionMuestreo == null)
            {
                throw new KeyNotFoundException("Registro de supervision no encontrado");
            }

            _dbContext.RemoveRange(supervisionMuestreo.EvidenciaSupervisionMuestreo);
            _dbContext.RemoveRange(supervisionMuestreo.ValoresSupervisionMuestreo);
            _dbContext.Remove(supervisionMuestreo);

            await _dbContext.SaveChangesAsync();

            return true;
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
                                                    Puntaje = r.Valor,
                                                    EsExcepcionNoAplica = r.EsExcepcionNoAplica
                                                }).ToList()
                               }).ToListAsync();
            return datos;
        }
    }
}
