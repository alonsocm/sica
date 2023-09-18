﻿using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class VwDatosGeneralesSupervisionRepository : Repository<VwDatosGeneralesSupervision>, IVwDatosGeneralesSupervisionRepository
    {
        public VwDatosGeneralesSupervisionRepository(SicaContext context) : base(context)
        {
        }

        public List<VwDatosGeneralesSupervision> ObtenerBusqueda(CriteriosBusquedaSupervisionDto busqueda)
        {
            var registros = _dbContext.VwDatosGeneralesSupervision.AsQueryable();

            if (registros.Any())
            {
                if (busqueda.OrganismosDireccionesRealizaId != null)
                {
                    registros = registros.Where(x => x.OrganismosDireccionesRealizaId == busqueda.OrganismosDireccionesRealizaId);
                }
                if (busqueda.SitioId != null)
                {
                    registros = registros.Where(x => x.SitioId == busqueda.SitioId);
                }
                if (!string.IsNullOrEmpty(busqueda.FechaMuestreo) && string.IsNullOrEmpty(busqueda.FechaMuestreoFin))
                {
                    registros = registros.Where(x => x.FechaMuestreo == Convert.ToDateTime(busqueda.FechaMuestreo));
                }
                if (!string.IsNullOrEmpty(busqueda.FechaMuestreoFin))
                {
                    registros = registros.Where(x => x.FechaMuestreo >= Convert.ToDateTime(busqueda.FechaMuestreo) && x.FechaMuestreo <= Convert.ToDateTime(busqueda.FechaMuestreoFin));
                }

                if (busqueda.PuntajeObtenido != null)
                {
                    if (busqueda.PuntajeObtenido.ToString().Contains("<")) { registros = registros.Where(x => x.PuntajeObtenido < Convert.ToDecimal(busqueda.PuntajeObtenido.Replace("<", ""))); }
                    else
                    {
                        string inicio = busqueda.PuntajeObtenido.Split("-").ToList()[0].Trim();
                        string fin = busqueda.PuntajeObtenido.Split("-").ToList()[1].Trim();
                        registros = registros.Where(x => x.PuntajeObtenido >= Convert.ToDecimal(inicio) && x.PuntajeObtenido <= Convert.ToDecimal(fin));
                    }




                }
                if (busqueda.LaboratorioRealizaId != null)
                {
                    registros = registros.Where(x => x.LaboratorioRealizaId == busqueda.LaboratorioRealizaId);
                }
                if (!string.IsNullOrEmpty(busqueda.ClaveMuestreo))
                {
                    registros = registros.Where(x => x.ClaveMuestreo == busqueda.ClaveMuestreo);
                }
                if (busqueda.TipoCuerpoAguaId != null)
                {
                    registros = registros.Where(x => x.TipoCuerpoAguaId == busqueda.TipoCuerpoAguaId);
                }
            }

            return registros.ToList();
        }
    }
}
