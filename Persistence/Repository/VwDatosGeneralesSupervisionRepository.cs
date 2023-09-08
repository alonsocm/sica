using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class VwDatosGeneralesSupervisionRepository : Repository<VwDatosGeneralesSupervision>, IVwDatosGeneralesSupervisionRepository
    {
        public VwDatosGeneralesSupervisionRepository(SicaContext context) : base(context)
        {
        }

        public List<VwDatosGeneralesSupervision> ObtenerBusqueda(CriteriosBusquedaSupervisionDto? busqueda)
        {

            List<VwDatosGeneralesSupervision> lstDatos = ObtenerTodosElementosAsync().Result.ToList();

            if (busqueda.OrganismosDireccionesRealizaId != null)
            { lstDatos.Where(x => x.OrganismosDireccionesRealizaId == busqueda.OrganismosDireccionesRealizaId).ToList(); }
            if (busqueda.SitioId != null)
            {
                lstDatos.Where(x => x.SitioId == busqueda.SitioId).ToList();
            }
            if (!string.IsNullOrEmpty(busqueda.FechaMuestreo))
            {
                lstDatos.Where(x => x.FechaMuestreo == Convert.ToDateTime(busqueda.FechaMuestreo)).ToList();
            }
            if (busqueda.PuntajeObtenido != null)
            {
                lstDatos.Where(x => x.PuntajeObtenido == busqueda.PuntajeObtenido).ToList();
            }
            if (busqueda.LaboratorioRealizaId != null)
            {
                lstDatos.Where(x => x.LaboratorioRealizaId == busqueda.LaboratorioRealizaId).ToList();
            }
            if (!string.IsNullOrEmpty(busqueda.ClaveMuestreo))
            {
                lstDatos.Where(x => x.ClaveMuestreo == busqueda.ClaveMuestreo).ToList();
            }
            if (busqueda.TipoCuerpoAguaId != null)
            {
                lstDatos.Where(x => x.TipoCuerpoAguaId == busqueda.TipoCuerpoAguaId).ToList();
            }



            return lstDatos;
        }
    }
}
