﻿using Application.DTOs;
using Application.DTOs.Users;
using Application.Features.Catalogos.Emergencias.Commands;
using Application.Features.Muestreos.Commands.Liberacion;
using Application.Features.Muestreos.Queries;
using Application.Features.Operacion.Muestreos.Commands.Actualizar;
using Application.Features.Operacion.Muestreos.Commands.Carga;
using Application.Features.Operacion.Muestreos.Queries;
using Application.Features.Operacion.MuestreosEmergencias.Queries;
using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using Domain.Entities;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class Muestreos : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IProgramaAnioRepository _progrepor;

        public Muestreos(IConfiguration configuration, IWebHostEnvironment env, IProgramaAnioRepository progepo)
        {
            _configuration=configuration;
            _env=env;
            _progrepor = progepo;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] CargaMuestreos cargaMuestreos)
        {
            string filePath = string.Empty;

            if (cargaMuestreos.Archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await cargaMuestreos.Archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelSettings.keyValues;

            var registros = ExcelService.Import<CargaMuestreoDto>(fileInfo, "ebaseca");

            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CargaMuestreosCommand { Muestreos = registros, Validado = cargaMuestreos.Validado, Reemplazar=cargaMuestreos.Reemplazar }));
        }

        [HttpPost("CargaEmergencias")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] CargaEmergenciasDto cargaMuestreos)
        {
            string filePath = string.Empty;

            if (cargaMuestreos.Archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();
                using var stream = System.IO.File.Create(filePath);
                await cargaMuestreos.Archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);
            ExcelService.Mappings = CargaEmergencia.ColumnasPropiedades;

            var registros = ExcelService.Import<CargaMuestreoEmergenciaDto>(fileInfo, "EMERGENCIAS");
            System.IO.File.Delete(filePath);

            var emergencias = registros.Select(x => new { x.NombreEmergencia, x.Sitio }).Distinct().ToList();

            List<Emergencia> emergenciasNuevas = new();
            List<string> emergenciasPreviamenteCargadas = new();

            emergencias.ForEach(async emergencia =>
            {
                var existeEmergenciaCatalogo = await Mediator.Send(new ExisteEmergenciaQuery { NombreEmergencia = emergencia.NombreEmergencia });

                if (!existeEmergenciaCatalogo.Data)
                {
                    Emergencia nuevaEmergencia = new() { Anio = Convert.ToInt32(cargaMuestreos.Anio), NombreEmergencia = emergencia.NombreEmergencia, NombreSitio = emergencia.Sitio };
                    emergenciasNuevas.Add(nuevaEmergencia);
                    await Mediator.Send(new AgregarEmergenciaCommand { Emergencia = nuevaEmergencia });
                }
                else
                {
                    var existeCargaPrevia = await Mediator.Send(new ExisteMuestreoEmergenciaQuery { NombreEmergencia = emergencia.NombreEmergencia, NombreSitio = emergencia.Sitio });

                    if (existeCargaPrevia.Data)
                    {
                        emergenciasPreviamenteCargadas.Add(emergencia.NombreEmergencia);
                    }
                }
            });

            if (emergenciasPreviamenteCargadas.Any())
            {
                return Ok(new Response<List<string>>() { Succeded=false, Data=emergenciasPreviamenteCargadas, Message="Se encontraron emergencias previamente cargadas" });
            }

            return Ok(await Mediator.Send(new CargaMuestreosEmergenciaCommand { Muestreos = registros }));
        }

        [HttpPost("ExportarExcel")]
        public IActionResult Post(List<MuestreoDto> muestreos)
        {
            List<MuestreoExcel> muestreosExcel = new();

            muestreos.ForEach(muestreo =>
                muestreosExcel.Add(new MuestreoExcel
                {
                    OCDL = muestreo.OCDL,
                    ClaveSitio = muestreo.ClaveSitio,
                    ClaveMonitoreo = muestreo.ClaveMonitoreo,
                    Estado = muestreo.Estado,
                    TipoCuerpoAgua = muestreo.TipoCuerpoAgua,
                    Laboratorio = muestreo.Laboratorio,
                    FechaRealizacion = muestreo.FechaRealizacion,
                    NumeroEntrega = muestreo.NumeroEntrega,
                    FechaLimiteRevision = muestreo.FechaLimiteRevision,
                    Estatus = muestreo.Estatus,
                    TipoCargaResultados = muestreo.TipoCargaResultados,
                }
            ));

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("LiberacionMonitoreos");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);

            ExcelService.ExportToExcel(muestreosExcel, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);

            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool esLiberacion)
        {
            return Ok(await Mediator.Send(new GetMuestreos { EsLiberacion = esLiberacion }));
        }

        [HttpPut]
        public async Task<IActionResult> Put(List<MuestreoRevisionDto> request)
        {
            return Ok(await Mediator.Send(new EnvioRevisionMuestreosCommand { Muestreos=request }));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(List<int> muestreos)
        {
            return Ok(await Mediator.Send(new EliminarMuestreoCommand { Muestreos = muestreos }));
        }

        [HttpGet("ResumenResultadosPorMuestreo")]
        public async Task<IActionResult> Get([FromQuery] List<int> muestreos)
        {
            return Ok(await Mediator.Send(new GetResumenResultadosByMuestreo { Muestreos = muestreos }));
        }

        [HttpGet("Aprobados")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetMuestreosAprobados()));
        }

        [HttpGet("AniosConRegistro")]
        public async Task<IActionResult> AniosConRegistro()
        {
            return Ok(await Mediator.Send(new GetAniosConOperacion()));
        }

        [HttpGet("ProgramaAnios")]
        public async Task<IActionResult> ProgramaAnios()
        {
            var datos = await _progrepor.ObtenerTodosElementosAsync();
            return Ok(datos);
        }

        [HttpGet("CambioEstatus")]
        public async Task<IActionResult> CambioEstatus(int estatus, long muestreoId)
        {
            return Ok(await Mediator.Send(new PutMuestreoEstatus { estatus = estatus, muestreoId =  muestreoId }));
        }

        [HttpPut("cambioEstatusMuestreos")]
        public async Task<IActionResult> CambioEstatusMuestreos(ActualizarEstatusListMuestreos datos)
        {
            return Ok(await Mediator.Send(new ActualizarEstatusListMuestreos { estatusId = datos.estatusId, muestreos = datos.muestreos }));
        }
    }
}