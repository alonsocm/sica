using Application.DTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class InformeMensualSupervisionRepository : Repository<InformeMensualSupervision>, IInformeMensualSupervisionRepository
    {
        public InformeMensualSupervisionRepository(SicaContext dbContext) : base(dbContext)
        {

        }

        public async Task<InformeMensualSupervisionDto> GetInformeMensualPorAnioMes(string anioReporte, string? anioRegistro, int? mes)
        {
            InformeMensualSupervisionDto informe = new InformeMensualSupervisionDto();
            informe.Atencion = _dbContext.DestinatariosAtencion.Where(x => x.Activo == true).ToListAsync().Result.Select(x => x.Descripcion).ToList();
            informe.GerenteCalidadAgua = _dbContext.Directorio.Where(x => x.PuestoId == (int)Application.Enums.Puestos.GerenteCalidadAgua).Select(x => x.Nombre).ToString() ?? string.Empty;
            var plantilla = _dbContext.PlantillaInformeMensualSupervision.Where(x => x.Anio == anioReporte).FirstOrDefault();
            informe.Contrato = plantilla.Contrato;
            informe.DenominacionContrato = plantilla.DenominacionContrato;
            informe.NumeroSitios = plantilla.SitiosMiniMax;
            informe.Indicaciones = plantilla.Indicaciones;

            if (mes != null)
            {
                var resultados = _dbContext.VwIntervalosTotalesOcDl.Where(x => x.FechaRegistro.Value.Year == Convert.ToInt32(anioRegistro) && x.FechaRegistro.Value.Month == mes).ToList();
                if (resultados != null)
                {
                    var resultadosInforme = (from r in resultados
                                             orderby r.OrganismoCuencaDireccionLocal
                                             group new { r } by new
                                             {
                                                 r._50,
                                                 r._5160,
                                                 r._6170,
                                                 r._7180,
                                                 r._8185,
                                                 r._8690,
                                                 r._9195,
                                                 r._96100,
                                                 r.Ocdlid,
                                                 r.OrganismoCuencaDireccionLocal
                                             } into gpo
                                             select new
                                             {
                                                 cant50 = (gpo.Key._50 != null) ? gpo.Count() : 0,
                                                 cant51 = (gpo.Key._5160 != null) ? gpo.Count() : 0,
                                                 cant61 = (gpo.Key._6170 != null) ? gpo.Count() : 0,
                                                 cant71 = (gpo.Key._7180 != null) ? gpo.Count() : 0,
                                                 cant81 = (gpo.Key._8185 != null) ? gpo.Count() : 0,
                                                 cant86 = (gpo.Key._8690 != null) ? gpo.Count() : 0,
                                                 cant91 = (gpo.Key._9195 != null) ? gpo.Count() : 0,
                                                 cant96 = (gpo.Key._96100 != null) ? gpo.Count() : 0,
                                                 OCdl = gpo.Key.OrganismoCuencaDireccionLocal

                                             }).ToList();


                    if (resultadosInforme.Count > 0)
                    {
                        foreach (var dato in resultadosInforme)
                        {
                            ResultadoInformeDto result = new ResultadoInformeDto();
                            result.OcDl = dato.OCdl;
                            result.TotalSitios = (dato.cant50 + dato.cant51 + dato.cant61 + dato.cant71 + dato.cant81 + dato.cant86 + dato.cant91 + dato.cant96).ToString();
                            result.Intervalos.Add(new IntervaloDto { Calificacion = "<50", NumeroSitios = dato.cant50.ToString(), Porcentaje = (dato.cant50 == 0) ? "0.00%" : "50%" });
                            result.Intervalos.Add(new IntervaloDto { Calificacion = "51-60", NumeroSitios = dato.cant51.ToString(), Porcentaje = (dato.cant51 == 0) ? "0.00%" : "60%" });
                            result.Intervalos.Add(new IntervaloDto { Calificacion = "61-70", NumeroSitios = dato.cant61.ToString(), Porcentaje = (dato.cant61 == 0) ? "0.00%" : "70%" });
                            result.Intervalos.Add(new IntervaloDto { Calificacion = "71-80", NumeroSitios = dato.cant71.ToString(), Porcentaje = (dato.cant71 == 0) ? "0.00%" : "80%" });
                            result.Intervalos.Add(new IntervaloDto { Calificacion = "81-85", NumeroSitios = dato.cant81.ToString(), Porcentaje = (dato.cant81 == 0) ? "0.00%" : "85%" });
                            result.Intervalos.Add(new IntervaloDto { Calificacion = "86-90", NumeroSitios = dato.cant86.ToString(), Porcentaje = (dato.cant86 == 0) ? "0.00%" : "90%" });
                            result.Intervalos.Add(new IntervaloDto { Calificacion = "91-95", NumeroSitios = dato.cant91.ToString(), Porcentaje = (dato.cant91 == 0) ? "0.00%" : "95%" });
                            result.Intervalos.Add(new IntervaloDto { Calificacion = "96-100", NumeroSitios = dato.cant96.ToString(), Porcentaje = (dato.cant96 == 0) ? "0.00%" : "100%" });
                            informe.Resultados.Add(result);
                        }
                    }
                }
            }

            return informe;
        }
    }
}
