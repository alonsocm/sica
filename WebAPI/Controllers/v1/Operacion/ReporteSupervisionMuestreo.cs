using Application.DTOs.InformeMensualSupervisionCampo;
using Application.Features.Operacion.ReporteSupervisionMuestreo.Commands;
using Application.Features.Operacion.ReporteSupervisionMuestreo.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ReporteSupervisionMuestreo : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] InformeMensualDto informe)
        {
            return Ok(await Mediator.Send(new InformeMensualSupervisionCommand { Informe = informe }));
        }

        [HttpGet("DirectoresResponsables")]
        public async Task<IActionResult> DirectoresResponsables(string anio)
        {
            return Ok(await Mediator.Send(new GetDirectoresResponsablesPorAnioQuery { anio = anio }));
        }

        [HttpGet("InformeMensualResultados")]
        public async Task<IActionResult> InformeMensualResultados(string anioReporte, string? anioRegistro, int? mes)
        {
            return Ok(await Mediator.Send(new GetInformeMensualPorMesAnioQuery { anioReporte = anioReporte, anioRegistro = anioRegistro, mes = mes }));
        }
    }
}
