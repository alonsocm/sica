using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Azure.Core;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

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
            var datos = await (from r in _dbContext.CriteriosSupervisionMuestreo
                               join l in _dbContext.ClasificacionCriterio on r.ClasificacionCriterioId equals l.Id

                               select new ClasificacionCriterioDto
                               {
                                   Id = l.Id,
                                   Descripcion = l.Descripcion,
                                   Criterios = new List<CriterioDto>()
                               }).ToListAsync();

            foreach (var clasificacion in datos)
            {
                var criterios = await (from r in _dbContext.CriteriosSupervisionMuestreo
                                       where r.ClasificacionCriterioId == clasificacion.Id
                                       select new CriterioDto
                                       {
                                           Id = r.Id,
                                           Descripcion = r.Descripcion,
                                           Obligatorio = r.Obligatorio,
                                           Puntaje = r.Valor
                                       }).ToListAsync();
                clasificacion.Criterios = criterios;
            }
            return datos;


        }




    }
}
