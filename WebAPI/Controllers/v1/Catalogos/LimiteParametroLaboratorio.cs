﻿using Application.DTOs;
using Application.Features.Catalogos.LimiteParametroLaboratorio.Commands;
using Application.Features.Catalogos.LimiteParametroLaboratorio.Queries;
using Application.Features.Catalogos.Sitios.Commands;
using Application.Models;
using Application.Wrappers;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
using System.Drawing.Printing;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class LimiteParametroLaboratorio: BaseApiController
    {
        //GET: api/<controller> Obtiene  los limitesLaboratios mediante paginación
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, int pageSize, string? filter = "", string? order = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            OrderBy orderBy = null;

            if (!string.IsNullOrEmpty(order) && order.Split('_').Length == 2)
            {
                orderBy = new OrderBy
                {
                    Column = order.Split('_')[0],
                    Type = order.Split('_')[1]
                };
            }

            return Ok(await Mediator.Send(new GetAllLimiteParametrosLaboratorioPaginadosQuery
            {

                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateLimiteLaboratorioCommand limite)
        {
            return Ok(await Mediator.Send(new CreateLimiteLaboratorioCommand
            {
                ParametroId = limite.ParametroId,
                LaboratorioId = limite.LaboratorioId,
                RealizaLaboratorioMuestreoId = limite.RealizaLaboratorioMuestreoId,
                LaboratorioMuestreoId = limite.LaboratorioMuestreoId,
                PeriodoId = limite.PeriodoId,
                Activo = true,
                LDMaCumplir = limite.LDMaCumplir,
                LPCaCumplir = limite.LPCaCumplir,
                LoMuestra = limite.LoMuestra,            
                LoSubrogaId = limite.LoSubrogaId,                
                LaboratorioSubrogadoId = limite.LaboratorioSubrogadoId,
                MetodoAnalitico = limite.MetodoAnalitico,
                LDM = limite.LDM,
                LPC = limite.LPC,              
                AnioId = limite.AnioId,
            })); ;
        }

        [HttpGet("GetDistinctValuesFromColumn")]
        public async Task<IActionResult> Get(string column, string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            return Ok(await Mediator.Send(new GetDistinctValuesFromColumn { Column = column, Filters = filters }));
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateLimiteLaboratorioCommand limite)
        {
            return Ok(await Mediator.Send(new UpdateLimiteLaboratorioCommand
            {
                Id = limite.Id,
                ParametroId = limite.ParametroId,
                LaboratorioId = limite.LaboratorioId,
                RealizaLaboratorioMuestreoId = limite.RealizaLaboratorioMuestreoId,
                LaboratorioMuestreoId = limite.LaboratorioMuestreoId,
                PeriodoId = limite.PeriodoId,
                Activo = true,
                LDMaCumplir = limite.LDMaCumplir,
                LPCaCumplir = limite.LPCaCumplir,
                LoMuestra = limite.LoMuestra,
                LoSubrogaId = limite.LoSubrogaId,
                LaboratorioSubrogadoId = limite.LaboratorioSubrogadoId,
                MetodoAnalitico = limite.MetodoAnalitico,
                LDM = limite.LDM,
                LPC = limite.LPC,
                AnioId = limite.AnioId,
            }));
        }

        [HttpPost("CargaLimitesLaboratorio")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromQuery] bool actualizar, [FromForm] IFormFile archivo)
        {
            string filePath = string.Empty;

            if (archivo.Length > 0)
            {
                filePath = Path.GetTempFileName();

                using var stream = System.IO.File.Create(filePath);

                await archivo.CopyToAsync(stream);
            }

            FileInfo fileInfo = new(filePath);

            ExcelService.Mappings = ExcelLimitesParametroLaboratorioSettings.KeyValues;

            var registros = ExcelService.Import<LimiteParametrosLaboratorioExcel>(fileInfo, "Limites laboratorio");
            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CargaLimitesLaboratorioCommand { LimitesLaboratorios = registros, Actualizar = actualizar }));
        }

    }
}