using Application.DTOs;
using Application.Features.Catalogos.CuencaDireccionesLocales.Queries;
using Application.Features.Sitios.Queries.GetAllSitios;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class CuencasDireccionesLocales: BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {           

            return Ok(await Mediator.Send(new GetAllCuencaDireccionLocalQuery {}));
        }

       
    }
}
