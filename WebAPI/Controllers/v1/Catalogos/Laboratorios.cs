﻿using Application.Features.Catalogos.CuerpoDeAgua.Queries;
using Application.Features.Catalogos.Laboratorios.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class Laboratorios: BaseApiController
    {
        [HttpGet("Laboratorios")]
        public async Task<IActionResult> GetLaboratoriosQuery()
        {
            return Ok(await Mediator.Send(new GetLaboratoriosQuery()));
        }
    }
}
