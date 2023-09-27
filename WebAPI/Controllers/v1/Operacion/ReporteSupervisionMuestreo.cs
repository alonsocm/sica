using Application.Features.Operacion.ReporteSupervisionMuestreo.Queries;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ReporteSupervisionMuestreo : BaseApiController
    {
        private readonly IVwDirectoresResponsablesRepository _directoresponsablesrepository;
        private readonly IInformeMensualSupervisionRepository _informemensualrepository;

        public ReporteSupervisionMuestreo(IVwDirectoresResponsablesRepository directoresponsablesrepository, IInformeMensualSupervisionRepository informemensualrepository)
        {
            _directoresponsablesrepository = directoresponsablesrepository;
            _informemensualrepository = informemensualrepository;
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
