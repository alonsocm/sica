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

        //PUT api/<controller>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(int id, UpdateSitioCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
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

       
    }
}
