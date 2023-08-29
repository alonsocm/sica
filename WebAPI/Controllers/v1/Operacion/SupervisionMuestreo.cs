using Application.DTOs;
using Application.Features.Operacion.SupervisionMuestreo.Commands;
using Application.Features.Operacion.SustitucionLimites.Commands;
using Application.Interfaces.IRepositories;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class SupervisionMuestreo : BaseApiController
    {
        private readonly IMuestreadoresRepository _muestrador;
        private readonly ISitioRepository _sitioRepository;

        public SupervisionMuestreo(IMuestreadoresRepository muestreador, ISitioRepository sitioRepository)
        {
            _muestrador = muestreador; _sitioRepository = sitioRepository;
        }

        [HttpPost("SupervisionMuestreo")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] SupervisionMuestreoDto supervision)
        {
            return Ok(await Mediator.Send(new SupervisionMuestreoCommand { supervision = supervision }));

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
    }


}
