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
    public class VwDatosGeneralesSupervisionRepository: Repository<VwDatosGeneralesSupervision>, IVwDatosGeneralesSupervisionRepository
    {
        public VwDatosGeneralesSupervisionRepository(SicaContext context) : base(context)
        {
        }

        public List<VwDatosGeneralesSupervision> ObtenerBusqueda(CriteriosBusquedaSupervisionDto? busqueda)
        {

            List<VwDatosGeneralesSupervision> lstDatos = null;

            if (busqueda == null)
            {
                lstDatos = ObtenerTodosElementosAsync().Result.ToList();
            }

            else
            {

                if (busqueda.OrganismosDireccionesRealizaId != null)
                { lstDatos = (lstDatos == null) ? ObtenerTodosElementosAsync().Result.Where(x => x.OrganismosDireccionesRealizaId == busqueda.OrganismosDireccionesRealizaId).ToList() : lstDatos.Where(x => x.OrganismosDireccionesRealizaId == busqueda.OrganismosDireccionesRealizaId).ToList(); }
                if (busqueda.SitioId != null)
                {
                    lstDatos = (lstDatos == null) ? ObtenerTodosElementosAsync().Result.Where(x => x.SitioId == busqueda.SitioId).ToList() : lstDatos.Where(x => x.SitioId == busqueda.SitioId).ToList();
                }
                if (!string.IsNullOrEmpty(busqueda.FechaMuestreo))
                {
                    lstDatos = (lstDatos == null) ? ObtenerTodosElementosAsync().Result.Where(x => x.FechaMuestreo == Convert.ToDateTime(busqueda.FechaMuestreo)).ToList() : lstDatos.Where(x => x.FechaMuestreo == Convert.ToDateTime(busqueda.FechaMuestreo)).ToList();
                }
                if (busqueda.PuntajeObtenido != null)
                {
                    lstDatos = (lstDatos == null) ? ObtenerTodosElementosAsync().Result.Where(x => x.PuntajeObtenido == busqueda.PuntajeObtenido).ToList() : lstDatos.Where(x => x.PuntajeObtenido == busqueda.PuntajeObtenido).ToList();
                }
                if (busqueda.LaboratorioRealizaId != null)
                {
                    lstDatos = (lstDatos == null) ? ObtenerTodosElementosAsync().Result.Where(x => x.LaboratorioRealizaId == busqueda.LaboratorioRealizaId).ToList() : lstDatos.Where(x => x.LaboratorioRealizaId == busqueda.LaboratorioRealizaId).ToList();
                }
                if (!string.IsNullOrEmpty(busqueda.ClaveMuestreo))
                {
                    lstDatos = (lstDatos == null) ? ObtenerTodosElementosAsync().Result.Where(x => x.ClaveMuestreo == busqueda.ClaveMuestreo).ToList() : lstDatos.Where(x => x.ClaveMuestreo == busqueda.ClaveMuestreo).ToList();
                }
                if (busqueda.TipoCuerpoAguaId != null)
                {
                    lstDatos = (lstDatos == null) ? ObtenerTodosElementosAsync().Result.Where(x => x.TipoCuerpoAguaId == busqueda.TipoCuerpoAguaId).ToList() : lstDatos.Where(x => x.TipoCuerpoAguaId == busqueda.TipoCuerpoAguaId).ToList();
                }
            }
            
            
            return lstDatos;
        }
    }
}
