using Application.DTOs.Catalogos;
using Application.Features.Catalogos.TiposCuerpoAgua.Commands;
using Application.Features.Catalogos.TiposCuerpoAgua.Queries;
using Application.Wrappers;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Services;
using WebAPI.Shared;


namespace WebAPI.Controllers.v1.Catalogos
{
    [ApiVersion("1.0")]
    public class TipoCuerpoAgua : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetTipoCuerpoAguaQuery()));
        }
        [HttpGet("TipoCuerpoAguaP")]
        public async Task<IActionResult> Get(int page, int pageSize, string? filter)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            return Ok(await Mediator.Send(new GetTipoCuerpoAguaPQuery { Page = page, PageSize = pageSize, Filter = filters }));
        }
        [HttpGet("GetDistinctFromColumn")]
        public IActionResult GetDistinctFromColumn(string column, string? filter)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filter))
            {
                filters = QueryParam.GetFilters(filter);
            }

            var data = Mediator.Send(new GetTipoCuerpoAguaPQuery { Filter = filters }).Result.Data;

            return Ok(new Response<object>(AuxQuery.GetDistinctValuesFromColumn(column, data)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await Mediator.Send(new GetTipoCuerpoAguaIdQuery { Id = id })); ;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTipoCuerpoAguaCommand { Id = id })); ;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AddTipoCuerpoAguaCommand command)
        {
            return Ok(await Mediator.Send(new AddTipoCuerpoAguaCommand
            {
                Id = command.Id,
                Descripcion = command.Descripcion,
                TipoHomologadoId = command.TipoHomologadoId,
                Activo = command.Activo,
                Frecuencia = command.Frecuencia,
                EvidenciasEsperadas = command.EvidenciasEsperadas,
                TiempoMinimoMuestreo = command.TiempoMinimoMuestreo
            }));
        }
        [HttpPut()]
        public async Task<IActionResult> Put(UpdateTipoCuerpoAguaCommand tipoCuerpoAgua)
        {
            return Ok(await Mediator.Send(tipoCuerpoAgua));
        }

        [HttpPost("CargaMasiva")]
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

            ExcelService.Mappings = ExcelTipoCuerpoAguaSettings.KeyValues;

            var registros = ExcelService.Import<ExcelTipocuerpoAguaDTO>(fileInfo, "Hoja1");
            System.IO.File.Delete(filePath);

            return Ok(await Mediator.Send(new AddTipoCuerpoAguaExcelCommand { TipoCuerpoAgua = registros, Actualizar = actualizar }));
        }
    }
}
