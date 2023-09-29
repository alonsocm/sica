﻿using Application.DTOs.InformeMensualSupervisionCampo;
using Application.Features.Operacion.InformeMensualSupervision.Commands;
using Application.Features.Operacion.InformeMensualSupervision.Queries;
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
            return Ok(await Mediator.Send(new CreateInformeMensualSupervision { Informe = informe }));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] InformeMensualDto informe, long informeId)
        {
            return Ok(await Mediator.Send(new UpdateInformeMensualSupervision { Informe = informe, InformeId = informeId }));
        }

        [HttpGet]
        public async Task<IActionResult> Get(long informe)
        {
            return Ok(await Mediator.Send(new GetInformeMensualSupervisionById { Informe = informe }));
        }

        [HttpGet("DirectoresResponsables")]
        public async Task<IActionResult> DirectoresResponsables(string anio)
        {
            return Ok(await Mediator.Send(new GetDirectoresResponsablesPorAnioQuery { anio = anio }));
        }

        [HttpGet("InformeMensualResultados")]
        public async Task<IActionResult> InformeMensualResultados(string anioReporte, string? anioRegistro, int? mes, long? ocId)
        {
            return Ok(await Mediator.Send(new GetInformeMensualPorMesAnioQuery { anioReporte = anioReporte, anioRegistro = anioRegistro, mes = mes, ocId = ocId }));
        }

        [HttpGet("BusquedaInformeMensual")]
        public async Task<IActionResult> Get([FromQuery] InformeMensualSupervisionBusquedaDto busqueda)
        {
            return Ok(await Mediator.Send(new GetBusquedaInformeMensualSupervisionQuery { Busqueda = busqueda }));
        }
    }
}
