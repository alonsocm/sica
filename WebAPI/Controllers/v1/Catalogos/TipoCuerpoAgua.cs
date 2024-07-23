using Application.Features.Catalogos.TiposCuerpoAgua.Commands;
using Application.Features.Catalogos.TiposCuerpoAgua.Queries;
using Application.Features.TiposCuerpoAgua.Commands.AddTipoCuerpoAguaCommand;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await Mediator.Send(new GetTipoCuerpoAguaIdQuery { Id = id })); ;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await Mediator.Send(new DeleteTipoCuerpoAguaCommand { Id = id })); ;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AddTipoCuerpoAguaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateTipoCuerpoAguaCommand tipoCuerpoAgua)
        {
            return Ok(await Mediator.Send(new UpdateTipoCuerpoAguaCommand
            {
                Id = tipoCuerpoAgua.Id,
                Descripcion = tipoCuerpoAgua.Descripcion,
                TipoHomologadoId = tipoCuerpoAgua.TipoHomologadoId,
                Activo = tipoCuerpoAgua.Activo,
                Frecuencia = tipoCuerpoAgua.Frecuencia,
                EvidenciaEsperada = tipoCuerpoAgua.EvidenciaEsperada,
                TiempoMinimoMuestreo = tipoCuerpoAgua.TiempoMinimoMuestreo,
            }));
        }
    }
}
