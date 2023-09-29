using Application.DTOs;
using Application.DTOs.InformeMensualSupervisionCampo;
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
            informe.GerenteCalidadAgua = _dbContext.Directorio.Where(x => x.PuestoId == (int)Application.Enums.Puestos.GerenteCalidadAgua).FirstOrDefault().Nombre ?? string.Empty;
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
                                                 OCdl = gpo.Key.OrganismoCuencaDireccionLocal,
                                                 OcdlId = gpo.Key.Ocdlid


                                             }).ToList();


                    if (resultadosInforme.Count > 0)
                    {
                        List<long> ocdlExistentes = new List<long>();

                        foreach (var dato in resultadosInforme)
                        {
                            ocdlExistentes.Add(dato.OcdlId);
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


                        var faltantes = _dbContext.VwOrganismosDirecciones.Where(x => !ocdlExistentes.Contains(x.Id)).ToList();
                        List<IntervaloDto> IntervaloVacio = new List<IntervaloDto>();
                        IntervaloVacio.Add(new IntervaloDto { Calificacion = "<50", NumeroSitios = "0", Porcentaje = "0.00%" });
                        IntervaloVacio.Add(new IntervaloDto { Calificacion = "51-60", NumeroSitios = "0", Porcentaje = "0.00%" });
                        IntervaloVacio.Add(new IntervaloDto { Calificacion = "61-70", NumeroSitios = "0", Porcentaje = "0.00%" });
                        IntervaloVacio.Add(new IntervaloDto { Calificacion = "71-80", NumeroSitios = "0", Porcentaje = "0.00%" });
                        IntervaloVacio.Add(new IntervaloDto { Calificacion = "81-85", NumeroSitios = "0", Porcentaje = "0.00%" });
                        IntervaloVacio.Add(new IntervaloDto { Calificacion = "86-90", NumeroSitios = "0", Porcentaje = "0.00%" });
                        IntervaloVacio.Add(new IntervaloDto { Calificacion = "91-95", NumeroSitios = "0", Porcentaje = "0.00%" });
                        IntervaloVacio.Add(new IntervaloDto { Calificacion = "96-100", NumeroSitios = "0", Porcentaje = "0.00%" });


                        faltantes.ForEach(ocdl =>
                        {
                            informe.Resultados.Add(
                                new ResultadoInformeDto { OcDl = ocdl.OrganismoCuencaDireccionLocal, TotalSitios = "0", Intervalos = IntervaloVacio });

                        });

                    }
                }
            }

            return informe;
        }

        public bool UpdateInformeMensual(InformeMensualDto informe, long informeId)
        {
            var informeDb = _dbContext.InformeMensualSupervision.First(x => x.Id == informeId);

            informeDb.Memorando = informe.Oficio;
            informeDb.Lugar = informe.Lugar;
            informeDb.Fecha = Convert.ToDateTime(informe.Fecha);
            informeDb.DirectorioFirmaId = informe.ResponsableId;
            informeDb.Iniciales = informe.PersonasInvolucradas;
            informeDb.MesId = informe.Mes;
            informeDb.FechaRegistro = DateTime.Now;
            informeDb.UsuarioRegistroId = informe.Usuario;
            informeDb.CopiaInformeMensualSupervision = informe.Copias.Select(x => new CopiaInformeMensualSupervision
            {
                InformeMensualSupervisionId = informeDb.Id,
                Nombre = x.Nombre,
                Puesto = x.Puesto,
            }).ToList();

            var copiasInforme = _dbContext.CopiaInformeMensualSupervision.Where(x => x.InformeMensualSupervisionId == informeDb.Id);

            copiasInforme.ToList().ForEach(x =>
            {
                _dbContext.Remove(x);
            });

            _dbContext.InformeMensualSupervision.Update(informeDb);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<List<InformeMensualSupervisionBusquedaDto>> GetBusquedaInformeMensual(InformeMensualSupervisionBusquedaDto busqueda)
        {
            var informe = (from m in _dbContext.InformeMensualSupervision
                           join n in _dbContext.VwDirectoresResponsablesOc on m.DirectorioFirmaId equals n.Id
                           select new InformeMensualSupervisionBusquedaDto
                           {
                               DireccionTecnica = n.Oc,
                               Oficio = m.Memorando,
                               Lugar = m.Lugar,
                               MesReporte = m.Mes.Descripcion,
                               FechaRegistro = m.FechaRegistro.ToShortDateString(),
                               NombreFirma = m.DirectorioFirma.Nombre,
                               PuestoFirma = m.DirectorioFirma.Puesto.Descripcion,
                               Iniciales = m.Iniciales,
                               Contrato = _dbContext.PlantillaInformeMensualSupervision.Where(x => x.Anio == m.Fecha.Year.ToString()).Select(x => x.Contrato).FirstOrDefault()

                           }
                           ).ToList();


            if (informe.Any())
            {
                if (!string.IsNullOrEmpty(busqueda.Oficio))
                { informe = (List<InformeMensualSupervisionBusquedaDto>)informe.Where(x => x.Oficio == busqueda.Oficio); }
                if (!string.IsNullOrEmpty(busqueda.Lugar))
                { informe = (List<InformeMensualSupervisionBusquedaDto>)informe.Where(x => x.Lugar == busqueda.Lugar); }
                if (!string.IsNullOrEmpty(busqueda.FechaRegistro) && string.IsNullOrEmpty(busqueda.FechaRegistroFin))
                {
                    informe = (List<InformeMensualSupervisionBusquedaDto>)informe.Where(x => x.FechaRegistro == busqueda.FechaRegistro);
                }
                if (!string.IsNullOrEmpty(busqueda.FechaRegistroFin))
                {
                    informe = (List<InformeMensualSupervisionBusquedaDto>)informe.Where(x => Convert.ToDateTime(x.FechaRegistro) >= Convert.ToDateTime(busqueda.FechaRegistro) && Convert.ToDateTime(x.FechaRegistro) <= Convert.ToDateTime(busqueda.FechaRegistroFin));
                }
                if (!string.IsNullOrEmpty(busqueda.NombreFirma))
                { informe = (List<InformeMensualSupervisionBusquedaDto>)informe.Where(x => x.NombreFirma == busqueda.NombreFirma); }
                if (!string.IsNullOrEmpty(busqueda.PuestoFirma))
                { informe = (List<InformeMensualSupervisionBusquedaDto>)informe.Where(x => x.PuestoFirma == busqueda.PuestoFirma); }
                if (!string.IsNullOrEmpty(busqueda.Iniciales))
                { informe = (List<InformeMensualSupervisionBusquedaDto>)informe.Where(x => x.Iniciales == busqueda.Iniciales); }
                if (!string.IsNullOrEmpty(busqueda.Contrato))
                { informe = (List<InformeMensualSupervisionBusquedaDto>)informe.Where(x => x.Contrato == busqueda.Contrato); }

            }
            return informe;
        }

    }
}
