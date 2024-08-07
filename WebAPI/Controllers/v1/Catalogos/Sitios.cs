using Application.DTOs;
using Application.Features.Catalogos.Sitios.Queries;
using Application.Features.Sitios.Queries;
using Application.Features.Sitios.Commands.CreateSitioCommand;
using Application.Features.Sitios.Commands.DeleteSitioCommand;
using Application.Features.Sitios.Commands.UpdateSitioCommand;
using Application.Features.Sitios.Queries.GetAllSitios;
using Application.Features.Sitios.Queries.GetSitioById;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Shared;
using Application.Features.Catalogos.ParametrosGrupo.Commands;
using Domain.Entities;
using Application.DTOs.Catalogos;
using Domain.Settings;
using Shared.Utilities.Services;
using Application.Models;
using Application.Features.Catalogos.Sitios.Commands;

namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class Sitios : BaseApiController
    {
        //GET: api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetSitioByIdQuery { Id = id }));
        }

        //GET: api/<controller> Obtiene  los sitios mediante paginación
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

            return Ok(await Mediator.Send(new GetAllSitiosPaginadosQuery            {
               
                Page = page,
                PageSize = pageSize,
                Filter = filters,
                OrderBy = orderBy
            }));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateSitioCommand sitio)
        {
            return Ok(await Mediator.Send(new CreateSitioCommand
            {
                ClaveSitio = sitio.ClaveSitio,
                NombreSitio = sitio.NombreSitio,
                CuencaDireccionesLocalesId = sitio.CuencaDireccionesLocalesId,
                EstadoId = sitio.EstadoId,
                MunicipioId = sitio.MunicipioId,
                CuerpoTipoSubtipoAguaId = sitio.CuerpoTipoSubtipoAguaId,
                Latitud = sitio.Latitud,
                Longitud = sitio.Longitud,
                Observaciones = sitio.Observaciones,
                AcuiferoId = sitio.AcuiferoId,
                
            }));
        }

       
        [HttpPut]
        public async Task<ActionResult> Put(UpdateSitioCommand command)
        {
            return Ok(await Mediator.Send(new UpdateSitioCommand
            {
                Id = command.Id,
                ClaveSitio = command.ClaveSitio,
                NombreSitio = command.NombreSitio,
                CuencaDireccionesLocalesId = command.CuencaDireccionesLocalesId,
                EstadoId = command.EstadoId,
                MunicipioId = command.MunicipioId,
                CuerpoTipoSubtipoAguaId = command.CuerpoTipoSubtipoAguaId,
                Latitud = command.Latitud,
                Longitud = command.Longitud,
                Observaciones = command.Observaciones,
                AcuiferoId = command.AcuiferoId,
            }));
        }

        //Delete api/<controller>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteSitioCommand { Id = id }));
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

        [HttpPost("CargaSitios")]
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

            ExcelService.Mappings = ExcelSitiosSettings.KeyValues;

            var registros = ExcelService.Import<SitiosExcel>(fileInfo, "Sitios");
            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new CargaSitiosCommand { Sitios = registros, Actualizar = actualizar }));
        }


    }
}
