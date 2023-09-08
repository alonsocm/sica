using Application.DTOs;
using Application.Features.Operacion.SupervisionMuestreo.Commands;
using Application.Features.Operacion.SupervisionMuestreo.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class SupervisionMuestreo : BaseApiController
    {
        private readonly IMuestreadoresRepository _muestrador;
        private readonly ISitioRepository _sitioRepository;
        private readonly IVwOrganismosDireccionesRepository _organismoDirecRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IVwDatosGeneralesSupervisionRepository _datosGeneralesSupervisionRepository;

        public SupervisionMuestreo(IMuestreadoresRepository muestreador, ISitioRepository sitioRepository,
            IVwOrganismosDireccionesRepository organismoDirecRepository, IConfiguration configuration, IWebHostEnvironment env, IVwDatosGeneralesSupervisionRepository datosGeneralesSupervisionRepository)
        {
            _muestrador = muestreador; _sitioRepository = sitioRepository;
            _organismoDirecRepository = organismoDirecRepository;
            _configuration = configuration;
            _env = env;
            _datosGeneralesSupervisionRepository = datosGeneralesSupervisionRepository;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromBody] SupervisionMuestreoDto supervision)
        {
            return Ok(await Mediator.Send(new SupervisionMuestreoCommand { supervision = supervision }));
        }

        [HttpPost("ArchivosMuestreo")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ArchivosSupervisionDto archivosSupervision)
        {
            return Ok(await Mediator.Send(new EvidenciaSupervisionCommand { LstEvidencias = archivosSupervision }));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long supervision)
        {
            return Ok(await Mediator.Send(new DeleteSupervisionMuestreo { SupervisionId = supervision }));
        }

        [HttpGet("ResponsablesMuestreadores")]
        public async Task<IActionResult> ResponsablesMuestreadores(long laboratorioId)
        {
            var datos = await _muestrador.ObtenerElementosPorCriterioAsync(x => x.LaboratorioId == laboratorioId);
            return Ok(datos.ToList());
        }

        [HttpGet("SitiosPorCuencaDireccionId")]
        public async Task<IActionResult> SitiosPorCuencaDireccionId(long cuencaDireccionId)
        {
            var datos = await _sitioRepository.ObtenerElementosPorCriterioAsync(x => x.CuencaDireccionesLocalesId == cuencaDireccionId);
            return Ok(datos.ToList());
        }

        [HttpGet("OrganismosDirecciones")]
        public async Task<IActionResult> OrganismosDirecciones()
        {
            var datos = _organismoDirecRepository.ObtenerTodosElementosAsync().Result.OrderBy(x => x.OrganismoCuencaDireccionLocal);
            return Ok(datos.ToList());
        }

        [HttpGet("FormatoSupervisionMuestreo")]
        public async Task<IActionResult> FormatoSupervisionMuestreo()
        {
            var plantilla = new Plantilla(_configuration, _env);
            string path = plantilla.ObtenerRutaPlantilla("SupervisionMuestreo");

            if (System.IO.File.Exists(path))
            {
                return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
            }

            return NotFound();
        }

        [HttpGet("ClaveSitiosPorCuencaDireccionId")]
        public async Task<IActionResult> ClaveSitiosPorCuencaDireccionId(long organismoDireccionId)
        {
            return Ok(await Mediator.Send(new GetClavesSitiosQuery { OrganismosDireccionesRealizaId = organismoDireccionId }));
        }


        [HttpGet("ObtenerSitioPorClave")]
        public async Task<IActionResult> ObtenerSitioPorClave(string claveSitio)
        {
            return Ok(await Mediator.Send(new GetSitioPorClaveQuery { claveSitio = claveSitio }));
        }

        [HttpGet("ClasificacionCriterios")]
        public async Task<IActionResult> ClasificacionCriterios()
        {
            return Ok(await Mediator.Send(new GetClasificacionCriteriosQuery { }));
        }

        [HttpGet("ObtenerSupervisionMuestreoPorId")]
        public async Task<IActionResult> ObtenerSupervisionMuestreoPorId(long supervisionMuestreoId)
        {
            return Ok(await Mediator.Send(new GetSupervisionMuestreoPorIdQuery { SupervisionMuestreoId = supervisionMuestreoId }));
        }

        [HttpDelete("Archivo")]
        public async Task<IActionResult> Delete(long supervisionId, string nombreArchivo)
        {
            if (nombreArchivo is null || string.IsNullOrEmpty(nombreArchivo))
            {
                return BadRequest("Debe especificar un nombre de archivo eliminar");
            }

            return Ok(Mediator.Send(new DeleteArchivoSupervisionMuestreo { NombreArchivo = nombreArchivo, SupervisionId = supervisionId }));
        }

        [HttpGet("Archivo")]
        public async Task<IActionResult> Get(long supervisionId, string nombreArchivo)
        {
            if (nombreArchivo is null || string.IsNullOrEmpty(nombreArchivo))
            {
                return BadRequest("Debe especificar un nombre de archivo para descargar");
            }

            var archivo = await Mediator.Send(new GetArchivoSupervisionMuestreo { NombreArchivo = nombreArchivo, SupervisionId = supervisionId });

            return archivo is null
                ? throw new ApplicationException("No se encontró el archivo de la evidencia solicitada")
                : (IActionResult)File(archivo.Data.Archivo, archivo.Data.ContentType, archivo.Data.NombreArchivo);
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CriteriosBusquedaSupervisionDto? busqueda)
        {
            return Ok(await Mediator.Send(new GetBusquedaSupervisionQuery { Busqueda = busqueda }));
       
        }

        
    }
}
