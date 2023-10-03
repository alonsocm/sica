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

        public InformeMensualSupervisionDto GetInformeMensualPorAnioMes(string anioReporte, string? anioRegistro, int? mes, long? ocId)
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
                List<long> ocdlExistentes = new();
                var resultados = _dbContext.VwIntervalosTotalesOcDl.Where(x => x.FechaRegistro.Year == Convert.ToInt32(anioRegistro) && x.FechaRegistro.Month == mes && x.Ocid == ocId).ToList();

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
                                                 ocdlId = gpo.Key.Ocdlid
                                             }).ToList();

                    if (resultadosInforme.Count > 0)
                    {
                        foreach (var dato in resultadosInforme)
                        {
                            ocdlExistentes.Add(dato.ocdlId);

                            ResultadoInformeDto result = new()
                            {
                                OcDl = dato.OCdl,
                                TotalSitios = (dato.cant50 + dato.cant51 + dato.cant61 + dato.cant71 + dato.cant81 + dato.cant86 + dato.cant91 + dato.cant96).ToString()
                            };

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

                var faltantes = _dbContext.VwOrganismosDirecciones.Where(x => !ocdlExistentes.Contains(x.Id) && x.OrganismoCuencaId == ocId).ToList();
                List<IntervaloDto> IntervaloVacio = new()
                {
                    new IntervaloDto { Calificacion = "<50", NumeroSitios = "0", Porcentaje = "0.00%" },
                    new IntervaloDto { Calificacion = "51-60", NumeroSitios = "0", Porcentaje = "0.00%" },
                    new IntervaloDto { Calificacion = "61-70", NumeroSitios = "0", Porcentaje = "0.00%" },
                    new IntervaloDto { Calificacion = "71-80", NumeroSitios = "0", Porcentaje = "0.00%" },
                    new IntervaloDto { Calificacion = "81-85", NumeroSitios = "0", Porcentaje = "0.00%" },
                    new IntervaloDto { Calificacion = "86-90", NumeroSitios = "0", Porcentaje = "0.00%" },
                    new IntervaloDto { Calificacion = "91-95", NumeroSitios = "0", Porcentaje = "0.00%" },
                    new IntervaloDto { Calificacion = "96-100", NumeroSitios = "0", Porcentaje = "0.00%" }
                };

                if (faltantes.Count > 0)
                {
                    faltantes.ForEach(ocdl =>
                    {
                        informe.Resultados.Add(
                            new ResultadoInformeDto { OcDl = ocdl.OrganismoCuencaDireccionLocal, TotalSitios = "0", Intervalos = IntervaloVacio });
                    });
                }
            }

            return informe;
        }

        public bool UpdateInformeMensual(InformeMensualDto informe, long informeId, byte[] archivo)
        {
            var informeDb = _dbContext.InformeMensualSupervision.Include(x => x.ArchivoInformeMensualSupervision).First(x => x.Id == informeId);

            informeDb.Memorando = informe.Oficio;
            informeDb.Lugar = informe.Lugar;
            informeDb.Fecha = Convert.ToDateTime(informe.Fecha);
            informeDb.DirectorioFirmaId = informe.ResponsableId;
            informeDb.Iniciales = informe.PersonasInvolucradas;
            informeDb.MesId = informe.Mes;
            informeDb.FechaRegistro = DateTime.Now;
            informeDb.UsuarioRegistroId = informe.Usuario;
            informeDb.ArchivoInformeMensualSupervision.First(x => x.TipoArchivoInformeMensualSupervisionId == 1).Archivo = archivo;
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

        public bool UpdateInformeMensualArchivoFirmado(long informeId, string nombreArchivo, byte[] archivo, long usuarioId)
        {
            var archivoFirmado = _dbContext.ArchivoInformeMensualSupervision.Where(w => w.InformeMensualSupervisionId == informeId && w.TipoArchivoInformeMensualSupervisionId == 2).FirstOrDefault();

            if (archivoFirmado != null)
            {
                archivoFirmado.NombreArchivo = nombreArchivo;
                archivoFirmado.Archivo = archivo;
                archivoFirmado.FechaCarga = DateTime.Now;
                archivoFirmado.UsuarioCargaId = usuarioId;
            }
            else
            {
                _dbContext.ArchivoInformeMensualSupervision.Add(new ArchivoInformeMensualSupervision
                {
                    InformeMensualSupervisionId = informeId,
                    TipoArchivoInformeMensualSupervisionId = 2,
                    NombreArchivo = nombreArchivo,
                    Archivo = archivo,
                    FechaCarga = DateTime.Now,
                    UsuarioCargaId = usuarioId,
                });
            }

            _dbContext.SaveChanges();

            return true;
        }

        public List<InformeMensualSupervisionBusquedaDto> GetBusquedaInformeMensual(InformeMensualSupervisionBusquedaDto busqueda)
        {
            List<InformeMensualSupervisionBusquedaDto> lstInformeDto = new();
            var informes = _dbContext.InformeMensualSupervision.Include("ArchivoInformeMensualSupervision").AsQueryable();

            if (informes.Any())
            {
                if (!string.IsNullOrEmpty(busqueda.Oficio))
                {
                    informes = informes.Where(x => x.Memorando == busqueda.Oficio);
                }

                if (!string.IsNullOrEmpty(busqueda.Lugar))
                {
                    informes = informes.Where(x => x.Lugar == busqueda.Lugar);
                }

                if (!string.IsNullOrEmpty(busqueda.FechaRegistro) && string.IsNullOrEmpty(busqueda.FechaRegistroFin))
                {
                    informes = informes.Where(x => x.FechaRegistro.ToShortDateString() == busqueda.FechaRegistro);
                }

                if (!string.IsNullOrEmpty(busqueda.FechaRegistroFin))
                {
                    informes = informes.Where(x => x.FechaRegistro >= Convert.ToDateTime(busqueda.FechaRegistro) && x.FechaRegistro <= Convert.ToDateTime(busqueda.FechaRegistroFin));
                }

                if (!string.IsNullOrEmpty(busqueda.Iniciales))
                {
                    informes = informes.Where(x => x.Iniciales == busqueda.Iniciales);
                }

                foreach (var informe in informes)
                {
                    InformeMensualSupervisionBusquedaDto informef = new();
                    var directorio = _dbContext.VwDirectoresResponsablesOc.Where(x => x.Id == informe.DirectorioFirmaId).FirstOrDefault();
                    informef.Id = informe.Id;
                    informef.DireccionTecnica = directorio.Oc ?? string.Empty;
                    informef.Oficio = informe.Memorando;
                    informef.Lugar = informe.Lugar;
                    informef.MesReporte = _dbContext.Mes.Where(x => x.Id == informe.MesId).FirstOrDefault().Descripcion;
                    informef.FechaRegistro = informe.FechaRegistro.ToShortDateString();
                    informef.NombreFirma = directorio.Nombre;
                    informef.PuestoFirma = directorio.Puesto;
                    informef.Iniciales = informe.Iniciales;
                    informef.Contrato = _dbContext.PlantillaInformeMensualSupervision.Where(x => x.Anio == informe.Fecha.Year.ToString()).Select(x => x.Contrato).FirstOrDefault();
                    informef.ExisteInformeFirmado = informe.ArchivoInformeMensualSupervision.Any(x => x.TipoArchivoInformeMensualSupervisionId == 2);
                    lstInformeDto.Add(informef);
                }

                if (!string.IsNullOrEmpty(busqueda.NombreFirma))
                    lstInformeDto = lstInformeDto.Where(x => x.NombreFirma == busqueda.NombreFirma).ToList();

                if (!string.IsNullOrEmpty(busqueda.PuestoFirma))
                    lstInformeDto = lstInformeDto.Where(x => x.PuestoFirma == busqueda.PuestoFirma).ToList();

                if (!string.IsNullOrEmpty(busqueda.Contrato))
                    lstInformeDto = lstInformeDto.Where(x => x.Contrato == busqueda.Contrato).ToList();

                if (!string.IsNullOrEmpty(busqueda.MesReporte))
                    lstInformeDto = lstInformeDto.Where(x => x.MesReporte.ToUpper() == busqueda.MesReporte.ToUpper()).ToList();

            }

            return lstInformeDto;
        }

        public Task<List<string>> GetLugaresInformeMensual()
        {
            return _dbContext.InformeMensualSupervision.Select(x => x.Lugar).Distinct().ToListAsync();
        }

        public Task<List<string>> GetMemorandoInformeMensual()
        {
            return _dbContext.InformeMensualSupervision.Select(x => x.Memorando).Distinct().ToListAsync();
        }

        public Task<ArchivoInformeMensualSupervision> GetArchivoInformeMensual(long informeId, int tipo = 1)
        {
            return _dbContext.ArchivoInformeMensualSupervision.FirstAsync(x => x.InformeMensualSupervisionId == informeId && x.TipoArchivoInformeMensualSupervisionId == tipo);
        }
    }
}
