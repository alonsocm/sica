using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Operacion
{
    public class SupervisionMuestreo : BaseApiController
    {

        [HttpPost("SupervisionMuestreo")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] SupervisionMuestreoDto supervision)
        {
            return Ok("ok");
        
        }
    }


}
