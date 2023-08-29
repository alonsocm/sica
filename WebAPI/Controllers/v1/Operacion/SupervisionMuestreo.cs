using Application.DTOs;
using Application.Features.Operacion.SupervisionMuestreo.Commands;
using Application.Features.Operacion.SustitucionLimites.Commands;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Operacion
{
    public class SupervisionMuestreo : BaseApiController
    {

        [HttpPost("SupervisionMuestreo")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] SupervisionMuestreoDto supervision)
        {
            return Ok(await Mediator.Send(new SupervisionMuestreoCommand { supervision = supervision }));

        }
    }


}
