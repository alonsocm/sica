using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.Features.Muestreos.Queries;
using Application.Features.Operacion.ReplicasResultadosReglasValidacion.Queries;
using Application.Features.Operacion.Resultados.Queries;
using Application.Models;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
using System.Drawing.Printing;
using WebAPI.Shared;

namespace WebAPI.Controllers.v1.Operacion
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ReplicasResultadosReglasValidacion : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ReplicasResultadosReglasValidacion(IMapper mapper, IConfiguration configuration, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int estatusId, int page, int pageSize, string? filter = "", string? order = "")
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

            return Ok(await Mediator.Send(new GetReplicasResultadosReglaValByEstatus
            {
                EstatusId = estatusId,
                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));

        }

        [HttpPost]
        public IActionResult extraerMuestreosAprobados(List<ReplicasResultadosReglasValidacionDto> resultados)
        {
            List<ReplicasResultadosRegValidacionExcel> aprobados = _mapper.Map<List<ReplicasResultadosRegValidacionExcel>>(resultados);

            aprobados.ForEach(resumen =>
            {
                resumen.AceptaRechazo = (resumen.AceptaRechazo == "True") ? "SI" : "NO";
                resumen.CorrectoResultadoReglaValidacion = (resumen.CorrectoResultadoReglaValidacion == "True") ? "SI" : "NO";
                resumen.MismoResultado = (resumen.MismoResultado == "True") ? "SI" : "NO";
                resumen.EsDatoCorrectoSrenameca = (resumen.EsDatoCorrectoSrenameca == "True") ? "SI" : "NO" ?? string.Empty;
                resumen.ApruebaResultadoReplica = (resumen.ApruebaResultadoReplica == "True") ? "SI" : "NO" ?? string.Empty;
            });

            var plantilla = new Plantilla(_configuration, _env);
            string templatePath = plantilla.ObtenerRutaPlantilla("ReplicasResultadosExcel");
            var fileInfo = plantilla.GenerarArchivoTemporal(templatePath, out string temporalFilePath);
            ExcelService.ExportToExcel(aprobados, fileInfo, true);
            var bytes = plantilla.GenerarArchivoDescarga(temporalFilePath, out var contentType);
            return File(bytes, contentType, Path.GetFileName(temporalFilePath));
        }

        [HttpGet("GetDistinctValuesFromColumn")]
        public IActionResult Get(string column, string? filter = "")
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new GetReplicasResultadosReglaValByEstatus
            { Filter = filters }).Result.Data;

            return Ok(new Response<object>(AuxQuery.GetDistinctValuesFromColumn(column, data)));
        }
    }
}
