﻿using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.Features.Operacion.ValidacionEvidencias.Commands;
using Application.Features.Operacion.ValidacionEvidencias.Queries;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Shared.Utilities.Services;

namespace WebAPI.Controllers.v1.Operacion
{
    public class ValidacionEvidencias : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IProgramaAnioRepository _progrepor;

        public ValidacionEvidencias(IConfiguration configuration, IWebHostEnvironment env, IProgramaAnioRepository progepo)
        {
            _configuration = configuration;
            _env = env;
            _progrepor = progepo;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] IFormFile archivo)
        {
            string filePath = string.Empty;

            if (archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelAvisoRealizacion.keyValues;

            var registros = ExcelService.Import<AvisoRealizacionDto>(fileInfo, "DataBank");

            System.IO.File.Delete(filePath);

            //return Ok(await Mediator.Send(true));
            return Ok(await Mediator.Send(new CargaARMCommand { Muestreos = registros }));
        }

        [HttpGet]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> ObtenerDatosGenerales() {

            return Ok(await Mediator.Send(new GetValidacionEvidenciasQuery()));

        }

        [HttpPost("validarMuestreo")]
        public async Task<IActionResult> Post(vwValidacionEvienciasDto muestreo, long usuarioId)
        {

            return Ok(await Mediator.Send(new ValidarMuestreoCommand { Muestreos = muestreo, usuarioId = usuarioId }));

        }

    }
}
