using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ReporteSupervisionMuestreo : BaseApiController
    {
        private readonly IVwDirectoresResponsablesRepository _directoresponsablesrepository;

        public ReporteSupervisionMuestreo(IVwDirectoresResponsablesRepository directoresponsablesrepository)
        {
            _directoresponsablesrepository = directoresponsablesrepository;
        }

        [HttpGet("DirectoresResponsables")]
        public async Task<IActionResult> DirectoresResponsables()
        {
            var datos = _directoresponsablesrepository.ObtenerTodosElementosAsync().Result.OrderBy(x => x.Nombre);
            return Ok(datos.ToList());
        }
    }
}
